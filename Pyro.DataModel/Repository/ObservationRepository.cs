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
  public partial class ObservationRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Observation, new() 
    where ResourceHistoryType :Res_Observation_History, new()
  {
    public ObservationRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Observation_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.category_List);
      IncludeList.Add(x => x.code_List);
      IncludeList.Add(x => x.code_List);
      IncludeList.Add(x => x.data_absent_reason_List);
      IncludeList.Add(x => x.data_absent_reason_List);
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.method_List);
      IncludeList.Add(x => x.performer_List);
      IncludeList.Add(x => x.related_target_List);
      IncludeList.Add(x => x.related_type_List);
      IncludeList.Add(x => x.value_concept_List);
      IncludeList.Add(x => x.value_concept_List);
      IncludeList.Add(x => x.value_quantity_List);
      IncludeList.Add(x => x.value_string_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.date_DateTimeOffsetLow = null;      
      ResourceEntity.date_DateTimeOffsetHigh = null;      
      ResourceEntity.date_DateTimeOffsetLow = null;      
      ResourceEntity.date_DateTimeOffsetHigh = null;      
      ResourceEntity.device_VersionId = null;      
      ResourceEntity.device_FhirId = null;      
      ResourceEntity.device_Type = null;      
      ResourceEntity.device_Url = null;      
      ResourceEntity.device_ServiceRootURL_StoreID = null;      
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
      ResourceEntity.specimen_VersionId = null;      
      ResourceEntity.specimen_FhirId = null;      
      ResourceEntity.specimen_Type = null;      
      ResourceEntity.specimen_Url = null;      
      ResourceEntity.specimen_ServiceRootURL_StoreID = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.subject_VersionId = null;      
      ResourceEntity.subject_FhirId = null;      
      ResourceEntity.subject_Type = null;      
      ResourceEntity.subject_Url = null;      
      ResourceEntity.subject_ServiceRootURL_StoreID = null;      
      ResourceEntity.value_date_DateTimeOffsetLow = null;      
      ResourceEntity.value_date_DateTimeOffsetHigh = null;      
      ResourceEntity.value_date_DateTimeOffsetLow = null;      
      ResourceEntity.value_date_DateTimeOffsetHigh = null;      
      ResourceEntity.value_quantity_Comparator = null;      
      ResourceEntity.value_quantity_Quantity = null;      
      ResourceEntity.value_quantity_System = null;      
      ResourceEntity.value_quantity_Code = null;      
      ResourceEntity.value_quantity_Unit = null;      
      ResourceEntity.value_string_String = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Observation_Index_category.RemoveRange(ResourceEntity.category_List);            
      _Context.Res_Observation_Index_code.RemoveRange(ResourceEntity.code_List);            
      _Context.Res_Observation_Index_code.RemoveRange(ResourceEntity.code_List);            
      _Context.Res_Observation_Index_data_absent_reason.RemoveRange(ResourceEntity.data_absent_reason_List);            
      _Context.Res_Observation_Index_data_absent_reason.RemoveRange(ResourceEntity.data_absent_reason_List);            
      _Context.Res_Observation_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_Observation_Index_method.RemoveRange(ResourceEntity.method_List);            
      _Context.Res_Observation_Index_performer.RemoveRange(ResourceEntity.performer_List);            
      _Context.Res_Observation_Index_related_target.RemoveRange(ResourceEntity.related_target_List);            
      _Context.Res_Observation_Index_related_type.RemoveRange(ResourceEntity.related_type_List);            
      _Context.Res_Observation_Index_value_concept.RemoveRange(ResourceEntity.value_concept_List);            
      _Context.Res_Observation_Index_value_concept.RemoveRange(ResourceEntity.value_concept_List);            
      _Context.Res_Observation_Index_value_quantity.RemoveRange(ResourceEntity.value_quantity_List);            
      _Context.Res_Observation_Index_value_string.RemoveRange(ResourceEntity.value_string_List);            
      _Context.Res_Observation_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Observation_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Observation_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Observation;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Effective != null)
      {
        if (ResourceTyped.Effective is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimePeriodIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimePeriodIndex)).Set(ResourceTyped.Effective, Index) as DateTimePeriodIndex;
          if (Index != null)
          {
            ResourseEntity.date_DateTimeOffsetLow = Index.DateTimeOffsetLow;
            ResourseEntity.date_DateTimeOffsetHigh = Index.DateTimeOffsetHigh;
          }
        }
      }

      if (ResourceTyped.Effective != null)
      {
        if (ResourceTyped.Effective is Hl7.Fhir.Model.Period)
        {
          var Index = new DateTimePeriodIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimePeriodIndex)).Set(ResourceTyped.Effective, Index) as DateTimePeriodIndex;
          if (Index != null)
          {
            ResourseEntity.date_DateTimeOffsetLow = Index.DateTimeOffsetLow;
            ResourseEntity.date_DateTimeOffsetHigh = Index.DateTimeOffsetHigh;
          }
        }
      }

      if (ResourceTyped.Device != null)
      {
        if (ResourceTyped.Device is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Device, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.device_Type = Index.Type;
            ResourseEntity.device_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.device_Url = Index.Url;
            }
            else
            {
              ResourseEntity.device_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

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

      if (ResourceTyped.Specimen != null)
      {
        if (ResourceTyped.Specimen is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Specimen, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.specimen_Type = Index.Type;
            ResourseEntity.specimen_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.specimen_Url = Index.Url;
            }
            else
            {
              ResourseEntity.specimen_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ObservationStatus>)
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

      if (ResourceTyped.Value != null)
      {
        if (ResourceTyped.Value is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimePeriodIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimePeriodIndex)).Set(ResourceTyped.Value, Index) as DateTimePeriodIndex;
          if (Index != null)
          {
            ResourseEntity.value_date_DateTimeOffsetLow = Index.DateTimeOffsetLow;
            ResourseEntity.value_date_DateTimeOffsetHigh = Index.DateTimeOffsetHigh;
          }
        }
      }

      if (ResourceTyped.Value != null)
      {
        if (ResourceTyped.Value is Hl7.Fhir.Model.Period)
        {
          var Index = new DateTimePeriodIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimePeriodIndex)).Set(ResourceTyped.Value, Index) as DateTimePeriodIndex;
          if (Index != null)
          {
            ResourseEntity.value_date_DateTimeOffsetLow = Index.DateTimeOffsetLow;
            ResourseEntity.value_date_DateTimeOffsetHigh = Index.DateTimeOffsetHigh;
          }
        }
      }

      if (ResourceTyped.Value != null)
      {
        if (ResourceTyped.Value is Hl7.Fhir.Model.Quantity)
        {
          var Index = new QuantityIndex();
          Index = IndexSetterFactory.Create(typeof(QuantityIndex)).Set(ResourceTyped.Value, Index) as QuantityIndex;
          if (Index != null)
          {
            ResourseEntity.value_quantity_Comparator = Index.Comparator;
            ResourseEntity.value_quantity_Quantity = Index.Quantity;
            ResourseEntity.value_quantity_System = Index.System;
            ResourseEntity.value_quantity_Code = Index.Code;
            ResourseEntity.value_quantity_Unit = Index.Unit;
          }
        }
      }

      if (ResourceTyped.Value != null)
      {
        if (ResourceTyped.Value is Hl7.Fhir.Model.FhirString)
        {
          var Index = new StringIndex();
          Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.Value, Index) as StringIndex;
          if (Index != null)
          {
            ResourseEntity.value_string_String = Index.String;
          }
        }
      }

      if (ResourceTyped.Category != null)
      {
        foreach (var item3 in ResourceTyped.Category)
        {
          if (item3 != null)
          {
            foreach (var item4 in item3.Coding)
            {
              var Index = new Res_Observation_Index_category();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Observation_Index_category;
              ResourseEntity.category_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Code != null)
      {
        foreach (var item3 in ResourceTyped.Code.Coding)
        {
          var Index = new Res_Observation_Index_code();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Observation_Index_code;
          ResourseEntity.code_List.Add(Index);
        }
      }

      foreach (var item1 in ResourceTyped.Component)
      {
        if (item1.Code != null)
        {
          foreach (var item4 in item1.Code.Coding)
          {
            var Index = new Res_Observation_Index_code();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Observation_Index_code;
            ResourseEntity.code_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.DataAbsentReason != null)
      {
        foreach (var item3 in ResourceTyped.DataAbsentReason.Coding)
        {
          var Index = new Res_Observation_Index_data_absent_reason();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Observation_Index_data_absent_reason;
          ResourseEntity.data_absent_reason_List.Add(Index);
        }
      }

      foreach (var item1 in ResourceTyped.Component)
      {
        if (item1.DataAbsentReason != null)
        {
          foreach (var item4 in item1.DataAbsentReason.Coding)
          {
            var Index = new Res_Observation_Index_data_absent_reason();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Observation_Index_data_absent_reason;
            ResourseEntity.data_absent_reason_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_Observation_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Observation_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Method != null)
      {
        foreach (var item3 in ResourceTyped.Method.Coding)
        {
          var Index = new Res_Observation_Index_method();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Observation_Index_method;
          ResourseEntity.method_List.Add(Index);
        }
      }

      if (ResourceTyped.Performer != null)
      {
        foreach (var item in ResourceTyped.Performer)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_Observation_Index_performer();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Observation_Index_performer;
            if (Index != null)
            {
              ResourseEntity.performer_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Related)
      {
        if (item1.Target != null)
        {
          if (item1.Target is ResourceReference)
          {
            var Index = new Res_Observation_Index_related_target();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Target, Index, FhirRequestUri, this) as Res_Observation_Index_related_target;
            if (Index != null)
            {
              ResourseEntity.related_target_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Related)
      {
        if (item1.Type != null)
        {
          if (item1.TypeElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Observation.ObservationRelationshipType>)
          {
            var Index = new Res_Observation_Index_related_type();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.TypeElement, Index) as Res_Observation_Index_related_type;
            ResourseEntity.related_type_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Value != null)
      {
        if (ResourceTyped.Value is CodeableConcept)
        {
          CodeableConcept CodeableConcept = ResourceTyped.Value as CodeableConcept;
          foreach (var item3 in CodeableConcept.Coding)
          {
            var Index = new Res_Observation_Index_value_concept();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Observation_Index_value_concept;
            ResourseEntity.value_concept_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Component)
      {
        if (item1.Value != null)
        {
          if (item1.Value is CodeableConcept)
          {
            CodeableConcept CodeableConcept = item1.Value as CodeableConcept;
            foreach (var item4 in CodeableConcept.Coding)
            {
              var Index = new Res_Observation_Index_value_concept();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Observation_Index_value_concept;
              ResourseEntity.value_concept_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Component)
      {
        if (item1.Value != null)
        {
          if (item1.Value is Hl7.Fhir.Model.Quantity)
          {
            var Index = new Res_Observation_Index_value_quantity();
            Index = IndexSetterFactory.Create(typeof(QuantityIndex)).Set(item1.Value, Index) as Res_Observation_Index_value_quantity;
            ResourseEntity.value_quantity_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Component)
      {
        if (item1.Value != null)
        {
          if (item1.Value is Hl7.Fhir.Model.FhirString)
          {
            var Index = new Res_Observation_Index_value_string();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.Value, Index) as Res_Observation_Index_value_string;
            ResourseEntity.value_string_List.Add(Index);
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
              var Index = new Res_Observation_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Observation_Index__profile;
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
              var Index = new Res_Observation_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Observation_Index__security;
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
              var Index = new Res_Observation_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Observation_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

