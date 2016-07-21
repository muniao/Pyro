﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq.Expressions;
using Blaze.DataModel.DatabaseModel;
using Blaze.DataModel.DatabaseModel.Base;
using Blaze.DataModel.Support;
using Hl7.Fhir.Model;
using Blaze.Common.BusinessEntities;
using Blaze.Common.Interfaces;
using Blaze.Common.Interfaces.Repositories;
using Blaze.Common.Interfaces.UriSupport;
using Hl7.Fhir.Introspection;

namespace Blaze.DataModel.Repository
{
  public partial class SubscriptionRepository : CommonRepository, IResourceRepository
  {

    public SubscriptionRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    public IDatabaseOperationOutcome AddResource(Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Subscription;
      var ResourceEntity = new Res_Subscription();
      this.PopulateResourceEntity(ResourceEntity, "1", ResourceTyped, FhirRequestUri);
      this.DbAddEntity<Res_Subscription>(ResourceEntity);
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;     
      DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ResourcesMatchingSearchCount = 1;
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome UpdateResource(string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Subscription;
      var ResourceEntity = LoadCurrentResourceEntity(Resource.Id);
      var ResourceHistoryEntity = new Res_Subscription_History();  
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_Subscription_History_List.Add(ResourceHistoryEntity); 
      this.ResetResourceEntity(ResourceEntity);
      this.PopulateResourceEntity(ResourceEntity, ResourceVersion, ResourceTyped, FhirRequestUri);            
      this.Save();            
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ResourcesMatchingSearchCount = 1;
      return DatabaseOperationOutcome;
    }

    public void UpdateResouceAsDeleted(string FhirResourceId, string ResourceVersion)
    {
      var ResourceEntity = this.LoadCurrentResourceEntity(FhirResourceId);
      var ResourceHistoryEntity = new Res_Subscription_History();
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_Subscription_History_List.Add(ResourceHistoryEntity);
      this.ResetResourceEntity(ResourceEntity);
      ResourceEntity.IsDeleted = true;
      ResourceEntity.versionId = ResourceVersion;
      this.Save();      
    }

    public IDatabaseOperationOutcome GetResourceByFhirIDAndVersionNumber(string FhirResourceId, string ResourceVersionNumber)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      var ResourceHistoryEntity = DbGet<Res_Subscription_History>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
      if (ResourceHistoryEntity != null)
      {
        DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceHistoryEntity);
      }
      else
      {
        var ResourceEntity = DbGet<Res_Subscription>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
        if (ResourceEntity != null)
          DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceEntity);        
      }
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome GetResourceByFhirID(string FhirResourceId, bool WithXml = false)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      Blaze.Common.BusinessEntities.Dto.DtoResource DtoResource = null;
      if (WithXml)
      {        
        DtoResource = DbGetALL<Res_Subscription>(x => x.FhirId == FhirResourceId).Select(x => new Blaze.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated, Xml = x.XmlBlob }).SingleOrDefault();       
      }
      else
      {
        DtoResource = DbGetALL<Res_Subscription>(x => x.FhirId == FhirResourceId).Select(x => new Blaze.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated }).SingleOrDefault();        
      }
      DatabaseOperationOutcome.ResourceMatchingSearch = DtoResource;
      return DatabaseOperationOutcome;
    }

    private Res_Subscription LoadCurrentResourceEntity(string FhirId)
    {

      var IncludeList = new List<Expression<Func<Res_Subscription, object>>>();
      IncludeList.Add(x => x.contact_List);
      IncludeList.Add(x => x.tag_List);
      IncludeList.Add(x => x.profile_List);
      IncludeList.Add(x => x.security_List);
      IncludeList.Add(x => x.tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<Res_Subscription>(x => x.FhirId == FhirId, IncludeList);

      return ResourceEntity;
    }


    private void ResetResourceEntity(Res_Subscription ResourceEntity)
    {
      ResourceEntity.criteria_String = null;      
      ResourceEntity.payload_String = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.type_Code = null;      
      ResourceEntity.type_System = null;      
      ResourceEntity.url_Uri = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Subscription_Index_contact.RemoveRange(ResourceEntity.contact_List);            
      _Context.Res_Subscription_Index_tag.RemoveRange(ResourceEntity.tag_List);            
      _Context.Res_Subscription_Index_profile.RemoveRange(ResourceEntity.profile_List);            
      _Context.Res_Subscription_Index_security.RemoveRange(ResourceEntity.security_List);            
      _Context.Res_Subscription_Index_tag.RemoveRange(ResourceEntity.tag_List);            
 
    }

    private void PopulateResourceEntity(Res_Subscription ResourseEntity, string ResourceVersion, Subscription ResourceTyped, IDtoFhirRequestUri FhirRequestUri)
    {
       IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Criteria != null)
      {
        if (ResourceTyped.CriteriaElement is Hl7.Fhir.Model.FhirString)
        {
          StringIndex Index = null;
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.CriteriaElement) as StringIndex;
          if (Index != null)
          {
            ResourseEntity.criteria_String = Index.String;
          }
        }
      }

      if (ResourceTyped.Channel != null)
      {
        if (ResourceTyped.Channel.Payload != null)
        {
          if (ResourceTyped.Channel.PayloadElement is Hl7.Fhir.Model.FhirString)
          {
            StringIndex Index = null;
            Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Channel.PayloadElement) as StringIndex;
            if (Index != null)
            {
              ResourseEntity.payload_String = Index.String;
            }
          }
        }
      }

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Subscription.SubscriptionStatus>)
        {
          TokenIndex Index = null;
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.StatusElement) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.status_Code = Index.Code;
            ResourseEntity.status_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Channel != null)
      {
        if (ResourceTyped.Channel.Type != null)
        {
          if (ResourceTyped.Channel.TypeElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Subscription.SubscriptionChannelType>)
          {
            TokenIndex Index = null;
            Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Channel.TypeElement) as TokenIndex;
            if (Index != null)
            {
              ResourseEntity.type_Code = Index.Code;
              ResourseEntity.type_System = Index.System;
            }
          }
        }
      }

      if (ResourceTyped.Channel != null)
      {
        if (ResourceTyped.Channel.Endpoint != null)
        {
          if (ResourceTyped.Channel.EndpointElement is Hl7.Fhir.Model.FhirUri)
          {
            UriIndex Index = null;
            Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Channel.EndpointElement) as UriIndex;
            if (Index != null)
            {
              ResourseEntity.url_Uri = Index.Uri;
            }
          }
        }
      }

      foreach (var item2 in ResourceTyped.Contact)
      {
        if (item2 is ContactPoint)
        {
          var Index = new Res_Subscription_Index_contact();
          Index = IndexSettingSupport.SetIndex(Index, item2) as Res_Subscription_Index_contact;
          ResourseEntity.contact_List.Add(Index);
        }
      }

      if (ResourceTyped.Tag != null)
      {
        foreach (var item3 in ResourceTyped.Tag)
        {
          if (item3 is Hl7.Fhir.Model.Coding)
          {
            Res_Subscription_Index_tag Index = null;
            Index = IndexSettingSupport.SetIndex(Index, item3) as Res_Subscription_Index_tag;
            ResourseEntity.tag_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Meta != null)
      {
        if (ResourceTyped.Meta.Profile != null)
        {
          foreach (var item4 in ResourceTyped.Meta.ProfileElement)
          {
            if (item4 is Hl7.Fhir.Model.FhirUri)
            {
              Res_Subscription_Index_profile Index = null;
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_Subscription_Index_profile;
              ResourseEntity.profile_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Meta != null)
      {
        if (ResourceTyped.Meta.Security != null)
        {
          foreach (var item4 in ResourceTyped.Meta.Security)
          {
            if (item4 is Hl7.Fhir.Model.Coding)
            {
              Res_Subscription_Index_security Index = null;
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_Subscription_Index_security;
              ResourseEntity.security_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Meta != null)
      {
        if (ResourceTyped.Meta.Tag != null)
        {
          foreach (var item4 in ResourceTyped.Meta.Tag)
          {
            if (item4 is Hl7.Fhir.Model.Coding)
            {
              Res_Subscription_Index_tag Index = null;
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_Subscription_Index_tag;
              ResourseEntity.tag_List.Add(Index);
            }
          }
        }
      }


      

    }


  }
} 
