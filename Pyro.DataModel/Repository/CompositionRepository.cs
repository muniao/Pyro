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
  public partial class CompositionRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Composition, new() 
    where ResourceHistoryType :Res_Composition_History, new()
  {
    public CompositionRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Composition_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.attester_List);
      IncludeList.Add(x => x.author_List);
      IncludeList.Add(x => x.class_List);
      IncludeList.Add(x => x.context_List);
      IncludeList.Add(x => x.entry_List);
      IncludeList.Add(x => x.period_List);
      IncludeList.Add(x => x.section_List);
      IncludeList.Add(x => x.type_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.confidentiality_Code = null;      
      ResourceEntity.confidentiality_System = null;      
      ResourceEntity.date_DateTimeOffset = null;      
      ResourceEntity.encounter_VersionId = null;      
      ResourceEntity.encounter_FhirId = null;      
      ResourceEntity.encounter_Type = null;      
      ResourceEntity.encounter_Url = null;      
      ResourceEntity.encounter_ServiceRootURL_StoreID = null;      
      ResourceEntity.identifier_Code = null;      
      ResourceEntity.identifier_System = null;      
      ResourceEntity.patient_VersionId = null;      
      ResourceEntity.patient_FhirId = null;      
      ResourceEntity.patient_Type = null;      
      ResourceEntity.patient_Url = null;      
      ResourceEntity.patient_ServiceRootURL_StoreID = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.subject_VersionId = null;      
      ResourceEntity.subject_FhirId = null;      
      ResourceEntity.subject_Type = null;      
      ResourceEntity.subject_Url = null;      
      ResourceEntity.subject_ServiceRootURL_StoreID = null;      
      ResourceEntity.title_String = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Composition_Index_attester.RemoveRange(ResourceEntity.attester_List);            
      _Context.Res_Composition_Index_author.RemoveRange(ResourceEntity.author_List);            
      _Context.Res_Composition_Index_class.RemoveRange(ResourceEntity.class_List);            
      _Context.Res_Composition_Index_context.RemoveRange(ResourceEntity.context_List);            
      _Context.Res_Composition_Index_entry.RemoveRange(ResourceEntity.entry_List);            
      _Context.Res_Composition_Index_period.RemoveRange(ResourceEntity.period_List);            
      _Context.Res_Composition_Index_section.RemoveRange(ResourceEntity.section_List);            
      _Context.Res_Composition_Index_type.RemoveRange(ResourceEntity.type_List);            
      _Context.Res_Composition_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Composition_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Composition_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Composition;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Confidentiality != null)
      {
        if (ResourceTyped.ConfidentialityElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Composition.ConfidentialityClassification>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.ConfidentialityElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.confidentiality_Code = Index.Code;
            ResourseEntity.confidentiality_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Date != null)
      {
        if (ResourceTyped.DateElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.DateElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.date_DateTimeOffset = Index.DateTimeOffset;
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

      if (ResourceTyped.Identifier != null)
      {
        if (ResourceTyped.Identifier is Hl7.Fhir.Model.Identifier)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Identifier, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.identifier_Code = Index.Code;
            ResourseEntity.identifier_System = Index.System;
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

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Composition.CompositionStatus>)
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

      if (ResourceTyped.Title != null)
      {
        if (ResourceTyped.TitleElement is Hl7.Fhir.Model.FhirString)
        {
          var Index = new StringIndex();
          Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.TitleElement, Index) as StringIndex;
          if (Index != null)
          {
            ResourseEntity.title_String = Index.String;
          }
        }
      }

      foreach (var item1 in ResourceTyped.Attester)
      {
        if (item1.Party != null)
        {
          if (item1.Party is ResourceReference)
          {
            var Index = new Res_Composition_Index_attester();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Party, Index, FhirRequestUri, this) as Res_Composition_Index_attester;
            if (Index != null)
            {
              ResourseEntity.attester_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Author != null)
      {
        foreach (var item in ResourceTyped.Author)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_Composition_Index_author();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Composition_Index_author;
            if (Index != null)
            {
              ResourseEntity.author_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Class != null)
      {
        foreach (var item3 in ResourceTyped.Class.Coding)
        {
          var Index = new Res_Composition_Index_class();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Composition_Index_class;
          ResourseEntity.class_List.Add(Index);
        }
      }

      foreach (var item1 in ResourceTyped.Event)
      {
        if (item1.Code != null)
        {
          foreach (var item4 in item1.Code)
          {
            if (item4 != null)
            {
              foreach (var item5 in item4.Coding)
              {
                var Index = new Res_Composition_Index_context();
                Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item5, Index) as Res_Composition_Index_context;
                ResourseEntity.context_List.Add(Index);
              }
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Section)
      {
        if (item1.Entry != null)
        {
          foreach (var item in item1.Entry)
          {
            if (item is ResourceReference)
            {
              var Index = new Res_Composition_Index_entry();
              Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Composition_Index_entry;
              if (Index != null)
              {
                ResourseEntity.entry_List.Add(Index);
              }
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Event)
      {
        if (item1.Period != null)
        {
          if (item1.Period is Period)
          {
            var Index = new Res_Composition_Index_period();
            Index = IndexSetterFactory.Create(typeof(DateTimePeriodIndex)).Set(item1.Period, Index) as Res_Composition_Index_period;
            ResourseEntity.period_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Section)
      {
        if (item1.Code != null)
        {
          foreach (var item4 in item1.Code.Coding)
          {
            var Index = new Res_Composition_Index_section();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Composition_Index_section;
            ResourseEntity.section_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Type != null)
      {
        foreach (var item3 in ResourceTyped.Type.Coding)
        {
          var Index = new Res_Composition_Index_type();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Composition_Index_type;
          ResourseEntity.type_List.Add(Index);
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
              var Index = new Res_Composition_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Composition_Index__profile;
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
              var Index = new Res_Composition_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Composition_Index__security;
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
              var Index = new Res_Composition_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Composition_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

