﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pyro.DataModel.DatabaseModel;
using Pyro.DataModel.DatabaseModel.Base;
using Pyro.DataModel.Support;
using Pyro.DataModel.IndexSetter;
using Pyro.Common.Interfaces.Repositories;
using Pyro.Common.Interfaces.UriSupport;
using Hl7.Fhir.Model;

//This file was auto generated by _GenericCodeDataTypeEnumsGenerator.tt and should not be modified manually. 
//Generation time-stamp: : 3/11/2016 12:11:43 PM.

namespace Pyro.DataModel.Repository
{
  public partial class CommunicationRequestRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_CommunicationRequest, new() 
    where ResourceHistoryType :Res_CommunicationRequest_History, new()
  {
    public CommunicationRequestRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_CommunicationRequest_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.category_List);
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.medium_List);
      IncludeList.Add(x => x.priority_List);
      IncludeList.Add(x => x.recipient_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.encounter_VersionId = null;      
      ResourceEntity.encounter_FhirId = null;      
      ResourceEntity.encounter_Type = null;      
      ResourceEntity.encounter_Url = null;      
      ResourceEntity.encounter_ServiceRootURL_StoreID = null;      
      ResourceEntity.patient_VersionId = null;      
      ResourceEntity.patient_FhirId = null;      
      ResourceEntity.patient_Type = null;      
      ResourceEntity.patient_Url = null;      
      ResourceEntity.patient_ServiceRootURL_StoreID = null;      
      ResourceEntity.requested_DateTimeOffset = null;      
      ResourceEntity.requester_VersionId = null;      
      ResourceEntity.requester_FhirId = null;      
      ResourceEntity.requester_Type = null;      
      ResourceEntity.requester_Url = null;      
      ResourceEntity.requester_ServiceRootURL_StoreID = null;      
      ResourceEntity.sender_VersionId = null;      
      ResourceEntity.sender_FhirId = null;      
      ResourceEntity.sender_Type = null;      
      ResourceEntity.sender_Url = null;      
      ResourceEntity.sender_ServiceRootURL_StoreID = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.subject_VersionId = null;      
      ResourceEntity.subject_FhirId = null;      
      ResourceEntity.subject_Type = null;      
      ResourceEntity.subject_Url = null;      
      ResourceEntity.subject_ServiceRootURL_StoreID = null;      
      ResourceEntity.time_DateTimeOffset = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_CommunicationRequest_Index_category.RemoveRange(ResourceEntity.category_List);            
      _Context.Res_CommunicationRequest_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_CommunicationRequest_Index_medium.RemoveRange(ResourceEntity.medium_List);            
      _Context.Res_CommunicationRequest_Index_priority.RemoveRange(ResourceEntity.priority_List);            
      _Context.Res_CommunicationRequest_Index_recipient.RemoveRange(ResourceEntity.recipient_List);            
      _Context.Res_CommunicationRequest_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_CommunicationRequest_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_CommunicationRequest_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as CommunicationRequest;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Encounter != null)
      {
        if (ResourceTyped.Encounter is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Encounter, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.encounter_Type = Index.Type;
            ResourseEntity.encounter_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.encounter_Url = Index.Url;
            }
            else
            {
              ResourseEntity.encounter_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Subject != null)
      {
        if (ResourceTyped.Subject is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Subject, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.patient_Type = Index.Type;
            ResourseEntity.patient_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.patient_Url = Index.Url;
            }
            else
            {
              ResourseEntity.patient_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.RequestedOn != null)
      {
        if (ResourceTyped.RequestedOnElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.RequestedOnElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.requested_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Requester != null)
      {
        if (ResourceTyped.Requester is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Requester, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.requester_Type = Index.Type;
            ResourseEntity.requester_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.requester_Url = Index.Url;
            }
            else
            {
              ResourseEntity.requester_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Sender != null)
      {
        if (ResourceTyped.Sender is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Sender, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.sender_Type = Index.Type;
            ResourseEntity.sender_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.sender_Url = Index.Url;
            }
            else
            {
              ResourseEntity.sender_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CommunicationRequest.CommunicationRequestStatus>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.StatusElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.status_Code = Index.Code;
            ResourseEntity.status_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Subject != null)
      {
        if (ResourceTyped.Subject is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Subject, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.subject_Type = Index.Type;
            ResourseEntity.subject_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.subject_Url = Index.Url;
            }
            else
            {
              ResourseEntity.subject_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Scheduled != null)
      {
        if (ResourceTyped.Scheduled is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.Scheduled, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.time_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Category != null)
      {
        foreach (var item3 in ResourceTyped.Category.Coding)
        {
          var Index = new Res_CommunicationRequest_Index_category();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_CommunicationRequest_Index_category;
          ResourseEntity.category_List.Add(Index);
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_CommunicationRequest_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_CommunicationRequest_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Medium != null)
      {
        foreach (var item3 in ResourceTyped.Medium)
        {
          if (item3 != null)
          {
            foreach (var item4 in item3.Coding)
            {
              var Index = new Res_CommunicationRequest_Index_medium();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_CommunicationRequest_Index_medium;
              ResourseEntity.medium_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Priority != null)
      {
        foreach (var item3 in ResourceTyped.Priority.Coding)
        {
          var Index = new Res_CommunicationRequest_Index_priority();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_CommunicationRequest_Index_priority;
          ResourseEntity.priority_List.Add(Index);
        }
      }

      if (ResourceTyped.Recipient != null)
      {
        foreach (var item in ResourceTyped.Recipient)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_CommunicationRequest_Index_recipient();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_CommunicationRequest_Index_recipient;
            if (Index != null)
            {
              ResourseEntity.recipient_List.Add(Index);
            }
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
              var Index = new Res_CommunicationRequest_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_CommunicationRequest_Index__profile;
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
              var Index = new Res_CommunicationRequest_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_CommunicationRequest_Index__security;
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
              var Index = new Res_CommunicationRequest_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_CommunicationRequest_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

