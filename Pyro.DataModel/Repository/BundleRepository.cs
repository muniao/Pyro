﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pyro.DataModel.DatabaseModel;
using Pyro.DataModel.DatabaseModel.Base;
using Pyro.DataModel.Support;
using Pyro.DataModel.IndexSetter;
using Pyro.Common.Interfaces.Repositories;
using Pyro.Common.Interfaces.UriSupport;
using Pyro.Common.BusinessEntities.Dto;
using Hl7.Fhir.Model;

//This file was auto generated by _GenericCodeDataTypeEnumsGenerator.tt and should not be modified manually. 
//Generation time-stamp: : 11/11/2016 6:16:22 PM.

namespace Pyro.DataModel.Repository
{
  public partial class BundleRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Bundle, new() 
    where ResourceHistoryType :Res_Bundle_History, new()
  {
    public BundleRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { this.RepositoryResourceType = FHIRAllTypes.Bundle; }

    protected override void GetResourceHistoryEntityList(LinqKit.ExpressionStarter<ResourceType> Predicate, int StartRecord, List<DtoResource> DtoResourceList)
    {
      var HistoryEntityList = DbGetAll<ResourceType>(Predicate).SelectMany(y => y.Res_Bundle_History_List)
        .OrderByDescending(x => x.lastUpdated)
        .Skip(StartRecord)
        .Take(_NumberOfRecordsPerPage)
        .ToList();

      if (HistoryEntityList != null)
        HistoryEntityList.ForEach(x => DtoResourceList.Add(IndexSettingSupport.SetDtoResource(x, this.RepositoryResourceType, false)));
    }

    protected override int GetResourceHistoryEntityCount(LinqKit.ExpressionStarter<ResourceType> Predicate)
    {
      return DbGetAll<ResourceType>(Predicate).SelectMany(y => y.Res_Bundle_History_List).Count();      
    }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Bundle_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.composition_VersionId = null;      
      ResourceEntity.composition_FhirId = null;      
      ResourceEntity.composition_Type = null;      
      ResourceEntity.composition_Url = null;      
      ResourceEntity.composition_ServiceRootURL_StoreID = null;      
      ResourceEntity.message_VersionId = null;      
      ResourceEntity.message_FhirId = null;      
      ResourceEntity.message_Type = null;      
      ResourceEntity.message_Url = null;      
      ResourceEntity.message_ServiceRootURL_StoreID = null;      
      ResourceEntity.type_Code = null;      
      ResourceEntity.type_System = null;      
 
      
      _Context.Res_Bundle_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Bundle_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Bundle_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Bundle;
      var ResourseEntity = ResourceEntity as ResourceType;

          if (ResourceTyped.Entry != null)
      {
        if (ResourceTyped.Entry[0] != null)
        {
          var item1 = ResourceTyped.Entry[0];
          if (ResourceTyped.Type == Bundle.BundleType.Document)
          {
            if (item1.FullUrl != null)
            {
              if (item1.FullUrlElement is Hl7.Fhir.Model.FhirUri)
              {
                var Index = new ReferenceIndex();
                Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.FullUrlElement, Index, FhirRequestUri, this) as ReferenceIndex;
                if (Index != null)
                {
                  ResourseEntity.composition_Type = Index.Type;
                  ResourseEntity.composition_FhirId = Index.FhirId;
                  if (Index.Url != null)
                  {
                    ResourseEntity.composition_Url = Index.Url;
                  }
                  else
                  {
                    ResourseEntity.composition_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
                  }
                }
              }
            }
          }
        }
      }

      if (ResourceTyped.Entry != null)
      {
        if (ResourceTyped.Entry[0] != null)
        {
          var item1 = ResourceTyped.Entry[0];
          if (ResourceTyped.Type == Bundle.BundleType.Message)
          {
            if (item1.FullUrl != null)
            {
              if (item1.FullUrlElement is Hl7.Fhir.Model.FhirUri)
              {
                var Index = new ReferenceIndex();
                Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.FullUrlElement, Index, FhirRequestUri, this) as ReferenceIndex;
                if (Index != null)
                {
                  ResourseEntity.message_Type = Index.Type;
                  ResourseEntity.message_FhirId = Index.FhirId;
                  if (Index.Url != null)
                  {
                    ResourseEntity.message_Url = Index.Url;
                  }
                  else
                  {
                    ResourseEntity.message_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
                  }
                }
              }
            }
          }
        }
      }

      if (ResourceTyped.Type != null)
      {
        if (ResourceTyped.TypeElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Bundle.BundleType>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.TypeElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.type_Code = Index.Code;
            ResourseEntity.type_System = Index.System;
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
              var Index = new Res_Bundle_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Bundle_Index__profile;
              ResourseEntity._profile_List.Add(Index);
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
              var Index = new Res_Bundle_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Bundle_Index__security;
              ResourseEntity._security_List.Add(Index);
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
              var Index = new Res_Bundle_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Bundle_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

