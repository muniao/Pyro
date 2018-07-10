﻿using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Pyro.Common.CompositionRoot;
using Pyro.Common.Interfaces.Repositories;
using Pyro.Common.Interfaces.Service;
using Pyro.Common.Logging;
using Pyro.Common.Service.ResourceService;
using Pyro.Common.Service.Trigger;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyro.Engine.Services.ResourceSeed
{
  public class ResourceSeedingService : IResourceSeedingService
  {
    private readonly IUnitOfWork IUnitOfWork;
    private readonly IResourceServices IResourceServices;
    private readonly IRequestMetaFactory IRequestMetaFactory;
    private readonly IResourceTriggerService IResourceTriggerService;
    private readonly ILog ILog;

    public ResourceSeedingService(IUnitOfWork IUnitOfWork, IResourceServices IResourceServices, IRequestMetaFactory IRequestMetaFactory, IResourceTriggerService IResourceTriggerService, ILog ILog)
    {
      this.IUnitOfWork = IUnitOfWork;
      this.IResourceServices = IResourceServices;
      this.IRequestMetaFactory = IRequestMetaFactory;
      this.IResourceTriggerService = IResourceTriggerService;
      this.ILog = ILog;
    }

    public void Process()
    {
      var MasterResourceList = ObtainMasterResoureList();      
      var ResourceToCommit = ResolveResourcesToLoad(MasterResourceList);
      CommitResourceList(ResourceToCommit);
    }

    private List<Resource> ObtainMasterResoureList()
    {
      List<Resource> MasterResourceList = new List<Resource>();
      CodeSystem PyroCodeSystem = Common.PyroHealthInformation.PyroServerCodeSystem.GetCodeSystem();
      MasterResourceList.Add(PyroCodeSystem);
      return MasterResourceList;

    }

    private List<Resource> ResolveResourcesToLoad(List<Resource> LoadList)
    {
      List<Resource> UpdateOrCreateList = new List<Resource>();
      foreach (Resource NewResource in LoadList)
      {
        string ResourceId = NewResource.Id;
        string ResourceName = NewResource.ResourceType.GetLiteral();
        var RequestMeta = IRequestMetaFactory.CreateRequestMeta().Set($"{ResourceName}/{ResourceId}");

        using (DbContextTransaction Transaction = IUnitOfWork.BeginTransaction())
        {
          try
          {
            IResourceServiceOutcome GetResourceServiceOutcome = IResourceServices.GetRead(ResourceId, RequestMeta);
            if (GetResourceServiceOutcome.HttpStatusCode == System.Net.HttpStatusCode.NotFound || GetResourceServiceOutcome.HttpStatusCode == System.Net.HttpStatusCode.Gone)
            {
              //If the resource is not found in the database then add
              UpdateOrCreateList.Add(NewResource);
            }
            else if (GetResourceServiceOutcome.HttpStatusCode == System.Net.HttpStatusCode.OK && GetResourceServiceOutcome.ResourceResult != null)
            {
              //If the resource is newer than that is the database add
              DateTimeOffset? DbResourceLastUpdated = GetResourceServiceOutcome.ResourceResult.Meta.LastUpdated;
              if (NewResource.Meta.LastUpdated.HasValue && DbResourceLastUpdated.HasValue)
              {
                if (NewResource.Meta.LastUpdated.Value > DbResourceLastUpdated.Value)
                {
                  UpdateOrCreateList.Add(NewResource);
                }
              }
            }
            Transaction.Commit();
          }
          catch (Exception Exec)
          {
            Transaction.Rollback();
            ILog.Error(Exec, $"ResourceSeeding on Startup, failed to GET {ResourceName} with id of {ResourceId}");
          }
        }
      }
      return UpdateOrCreateList;
    }

    private void CommitResourceList(List<Resource> CommitList)
    {      
      this.IResourceTriggerService.TriggersActive = false;
      using (DbContextTransaction Transaction = IUnitOfWork.BeginTransaction())
      {
        try
        {
          bool ErrorDetected = false;
          foreach(var NewResource in CommitList)
          {
            string ResourceId = NewResource.Id;
            string ResourceName = NewResource.ResourceType.GetLiteral();
            var RequestMeta = IRequestMetaFactory.CreateRequestMeta().Set($"{ResourceName}/{ResourceId}");
            IResourceServiceOutcome PutResourceServiceOutcome = IResourceServices.Put(ResourceId, NewResource, RequestMeta);
            if ((int)PutResourceServiceOutcome.HttpStatusCode > 201) //OK == 200 and Created == 201 so greater than 201 is and error, like 400.
            {
              Transaction.Rollback();
              ILog.Error($"ResourceSeeding on Startup, failed to PUT {ResourceName} with id of {ResourceId}. The entire seeding operation has been rolled back.");
              ErrorDetected = true;
              break;
            }
          }
          if (!ErrorDetected)
            Transaction.Commit();
        }
        catch (Exception Exec)
        {
          Transaction.Rollback();
          ILog.Error(Exec, $"ResourceSeeding on Startup, PUT operations failed with exception.");          
        }
        finally
        {          
          this.IResourceTriggerService.TriggersActive = true;
        }
      }
    }
    
  }
}
