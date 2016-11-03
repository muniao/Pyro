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
  public partial class AppointmentResponseRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_AppointmentResponse, new() 
    where ResourceHistoryType :Res_AppointmentResponse_History, new()
  {
    public AppointmentResponseRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_AppointmentResponse_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.actor_VersionId = null;      
      ResourceEntity.actor_FhirId = null;      
      ResourceEntity.actor_Type = null;      
      ResourceEntity.actor_Url = null;      
      ResourceEntity.actor_ServiceRootURL_StoreID = null;      
      ResourceEntity.appointment_VersionId = null;      
      ResourceEntity.appointment_FhirId = null;      
      ResourceEntity.appointment_Type = null;      
      ResourceEntity.appointment_Url = null;      
      ResourceEntity.appointment_ServiceRootURL_StoreID = null;      
      ResourceEntity.location_VersionId = null;      
      ResourceEntity.location_FhirId = null;      
      ResourceEntity.location_Type = null;      
      ResourceEntity.location_Url = null;      
      ResourceEntity.location_ServiceRootURL_StoreID = null;      
      ResourceEntity.part_status_Code = null;      
      ResourceEntity.part_status_System = null;      
      ResourceEntity.patient_VersionId = null;      
      ResourceEntity.patient_FhirId = null;      
      ResourceEntity.patient_Type = null;      
      ResourceEntity.patient_Url = null;      
      ResourceEntity.patient_ServiceRootURL_StoreID = null;      
      ResourceEntity.practitioner_VersionId = null;      
      ResourceEntity.practitioner_FhirId = null;      
      ResourceEntity.practitioner_Type = null;      
      ResourceEntity.practitioner_Url = null;      
      ResourceEntity.practitioner_ServiceRootURL_StoreID = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_AppointmentResponse_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_AppointmentResponse_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_AppointmentResponse_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_AppointmentResponse_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as AppointmentResponse;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Actor != null)
      {
        if (ResourceTyped.Actor is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Actor, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.actor_Type = Index.Type;
            ResourseEntity.actor_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.actor_Url = Index.Url;
            }
            else
            {
              ResourseEntity.actor_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Appointment != null)
      {
        if (ResourceTyped.Appointment is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Appointment, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.appointment_Type = Index.Type;
            ResourseEntity.appointment_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.appointment_Url = Index.Url;
            }
            else
            {
              ResourseEntity.appointment_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Actor != null)
      {
        if (ResourceTyped.Actor is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Actor, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.location_Type = Index.Type;
            ResourseEntity.location_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.location_Url = Index.Url;
            }
            else
            {
              ResourseEntity.location_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.ParticipantStatus != null)
      {
        if (ResourceTyped.ParticipantStatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ParticipationStatus>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.ParticipantStatusElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.part_status_Code = Index.Code;
            ResourseEntity.part_status_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Actor != null)
      {
        if (ResourceTyped.Actor is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Actor, Index, FhirRequestUri, this) as ReferenceIndex;
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

      if (ResourceTyped.Actor != null)
      {
        if (ResourceTyped.Actor is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Actor, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.practitioner_Type = Index.Type;
            ResourseEntity.practitioner_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.practitioner_Url = Index.Url;
            }
            else
            {
              ResourseEntity.practitioner_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_AppointmentResponse_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_AppointmentResponse_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
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
              var Index = new Res_AppointmentResponse_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_AppointmentResponse_Index__profile;
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
              var Index = new Res_AppointmentResponse_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_AppointmentResponse_Index__security;
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
              var Index = new Res_AppointmentResponse_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_AppointmentResponse_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

