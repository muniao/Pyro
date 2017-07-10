﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Pyro.Common.Interfaces.Dto;
using Pyro.Common.Interfaces.UriSupport;
using Pyro.Common.Interfaces.Service;
using Pyro.Common.Interfaces.Repositories;
using Pyro.Common.BusinessEntities.Service;
using Pyro.Common.Interfaces.ITools;
using Pyro.Common.Enum;
using Pyro.Common.Tools;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Pyro.Common.BusinessEntities.Dto;
using System.Net;
using Pyro.Common.Interfaces.Dto.Headers;
using Pyro.Common.CompositionRoot;

namespace Pyro.Engine.Services
{
  public class ResourceServices : ResourceServicesBase, IResourceServices
  {
    private readonly IRepositorySwitcher IRepositorySwitcher;
    private readonly ICommonFactory ICommonFactory;
    //Constructor for dependency injection
    public ResourceServices(IUnitOfWork IUnitOfWork, IRepositorySwitcher IRepositorySwitcher, ICommonFactory ICommonFactory)
      : base(IUnitOfWork)
    {
      this.IRepositorySwitcher = IRepositorySwitcher;
      this.ICommonFactory = ICommonFactory;
    }

    public DbContextTransaction BeginTransaction()
    {
      return _UnitOfWork.BeginTransaction();
    }

    public void SetCurrentResourceType(FHIRAllTypes ResourceType)
    {
      _CurrentResourceType = ResourceType;
      _ResourceRepository = IRepositorySwitcher.GetRepository(_CurrentResourceType);
    }

    public void SetCurrentResourceType(ResourceType ResourceType)
    {
      SetCurrentResourceType(ResourceType.SearchParameter.GetLiteral());
    }

    public void SetCurrentResourceType(string ResourceName)
    {
      Type ResourceType = ModelInfo.GetTypeForFhirType(ResourceName);
      if (ResourceType != null && ModelInfo.IsKnownResource(ResourceType))
      {
        this.SetCurrentResourceType((FHIRAllTypes)ModelInfo.FhirTypeNameToFhirType(ResourceName));
      }
      else
      {
        string ErrorMessage = $"The Resource name given '{ResourceName}' is not a Resource supported by the .net FHIR API Version: {ModelInfo.Version}.";
        var OpOutCome = Common.Tools.FhirOperationOutcomeSupport.Create(OperationOutcome.IssueSeverity.Fatal, OperationOutcome.IssueType.Invalid, ErrorMessage);
        OpOutCome.Issue[0].Details = new CodeableConcept("http://hl7.org/fhir/operation-outcome", "MSG_UNKNOWN_TYPE", String.Format("Resource Type '{0}' not recognised", ResourceName));
        throw new DtoPyroException(HttpStatusCode.BadRequest, OpOutCome, ErrorMessage);
      }
    }

    //GET Read   
    // Get: URL/Fhir/Patient/1
    public virtual IResourceServiceOutcome GetRead(
      string ResourceId,
      IDtoRequestUri RequestUri,
      IDtoSearchParameterGeneric SearchParameterGeneric,
      IDtoRequestHeaders RequestHeaders)
    {
      if (string.IsNullOrWhiteSpace(ResourceId))
        throw new NullReferenceException("ResourceId can not be null or empty string.");
      if (RequestUri == null)
        throw new NullReferenceException("DtoFhirRequestUri can not be null.");
      if (SearchParameterGeneric == null)
        throw new NullReferenceException("SearchParameterGeneric can not be null.");
      if (RequestHeaders == null)
        throw new NullReferenceException("RequestHeaders can not be null.");

      IResourceServiceOutcome oServiceOperationOutcome = Common.CommonFactory.GetResourceServiceOutcome();
      oServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Read;

      // GET by FhirId
      // GET URL/FhirApi/Patient/5    
      ISearchParameterService SearchService = ICommonFactory.CreateSearchParameterService();
      ISearchParametersServiceOutcome SearchParametersServiceOutcome = SearchService.ProcessBaseSearchParameters(SearchParameterGeneric);

      if (SearchParametersServiceOutcome.FhirOperationOutcome != null)
      {
        oServiceOperationOutcome.ResourceResult = SearchParametersServiceOutcome.FhirOperationOutcome;
        oServiceOperationOutcome.HttpStatusCode = SearchParametersServiceOutcome.HttpStatusCode;
        oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcome.SearchParameters.Format;
        return oServiceOperationOutcome;
      }
      oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcome.SearchParameters.Format;

      GetResourceInstance(
        ResourceId,
        RequestUri,
        oServiceOperationOutcome,
        RequestHeaders);

      oServiceOperationOutcome.SuccessfulTransaction = true;
      return oServiceOperationOutcome;
    }

    // GET by Search
    // GET: URL//FhirApi/Patient?family=Smith&given=John            
    public virtual IResourceServiceOutcome GetSearch(
      IDtoRequestUri RequestUri,
      IDtoSearchParameterGeneric SearchParameterGeneric)
    {
      if (RequestUri == null)
        throw new NullReferenceException("FhirRequestUri can not be null.");

      if (SearchParameterGeneric == null)
        throw new NullReferenceException("SearchParameterGeneric can not be null.");

      IResourceServiceOutcome oServiceOperationOutcome = Common.CommonFactory.GetResourceServiceOutcome();
      oServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Read;

      // GET by Search
      // GET: URL//FhirApi/Patient?family=Smith&given=John           
      ISearchParameterService SearchService = ICommonFactory.CreateSearchParameterService();
      ISearchParametersServiceOutcome SearchParametersServiceOutcome = SearchService.ProcessSearchParameters(SearchParameterGeneric, SearchParameterService.SearchParameterServiceType.Base | SearchParameterService.SearchParameterServiceType.Bundle | SearchParameterService.SearchParameterServiceType.Resource, _CurrentResourceType, null);
      if (SearchParametersServiceOutcome.FhirOperationOutcome != null)
      {
        oServiceOperationOutcome.ResourceResult = SearchParametersServiceOutcome.FhirOperationOutcome;
        oServiceOperationOutcome.HttpStatusCode = SearchParametersServiceOutcome.HttpStatusCode;
        oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcome.SearchParameters.Format;
        return oServiceOperationOutcome;
      }

      GetResourcesBySearch(
        RequestUri,
        SearchParametersServiceOutcome,
        oServiceOperationOutcome);

      oServiceOperationOutcome.SuccessfulTransaction = true;
      return oServiceOperationOutcome;
    }

    // GET All history for Id
    // GET URL/FhirApi/Patient/5/_history
    //Read all history
    public virtual IResourceServiceOutcome GetHistory(
      string ResourceId,
      string VersionId,
      IDtoRequestUri RequestUri,
      IDtoSearchParameterGeneric SearchParameterGeneric)
    {
      if (string.IsNullOrEmpty(ResourceId))
        throw new NullReferenceException("ResourceId can not be null or empty string.");
      //VersionId can be null
      if (RequestUri == null)
        throw new NullReferenceException("DtoFhirRequestUri can not be null.");
      if (SearchParameterGeneric == null)
        throw new NullReferenceException("SearchParameterGeneric can not be null.");

      IResourceServiceOutcome oServiceOperationOutcome = Common.CommonFactory.GetResourceServiceOutcome();
      oServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Read;

      if (string.IsNullOrWhiteSpace(VersionId))
      {
        // GET All history for Id
        // GET URL/FhirApi/Patient/5/_history
        //Read all history

        ISearchParameterService SearchService = ICommonFactory.CreateSearchParameterService();
        ISearchParametersServiceOutcome SearchParametersServiceOutcome = SearchService.ProcessBundleSearchParameters(SearchParameterGeneric);

        oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcome.SearchParameters.Format;

        if (SearchParametersServiceOutcome.FhirOperationOutcome != null)
        {
          oServiceOperationOutcome.ResourceResult = SearchParametersServiceOutcome.FhirOperationOutcome;
          oServiceOperationOutcome.HttpStatusCode = SearchParametersServiceOutcome.HttpStatusCode;
          oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcome.SearchParameters.Format;
          return oServiceOperationOutcome;
        }

        GetResourceHistoryInFull(ResourceId,
          RequestUri,
          SearchParametersServiceOutcome,
          oServiceOperationOutcome);

        oServiceOperationOutcome.SuccessfulTransaction = true;
        return oServiceOperationOutcome;
      }
      else
      {
        // GET by FhirId and FhirVId
        // GET URL/FhirApi/Patient/5/_history/2   
        ISearchParameterService SearchService = ICommonFactory.CreateSearchParameterService();
        ISearchParametersServiceOutcome SearchParametersServiceOutcome = SearchService.ProcessBaseSearchParameters(SearchParameterGeneric);

        if (SearchParametersServiceOutcome.FhirOperationOutcome != null)
        {
          oServiceOperationOutcome.ResourceResult = SearchParametersServiceOutcome.FhirOperationOutcome;
          oServiceOperationOutcome.HttpStatusCode = SearchParametersServiceOutcome.HttpStatusCode;
          oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcome.SearchParameters.Format;
          return oServiceOperationOutcome;
        }
        oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcome.SearchParameters.Format;

        GetResourceHistoryInstance(
          ResourceId,
          VersionId,
          RequestUri,
          oServiceOperationOutcome);

        oServiceOperationOutcome.SuccessfulTransaction = true;
        return oServiceOperationOutcome;
      }
    }

    // Add (POST)
    // POST: URL/FhirApi/Patient
    public virtual IResourceServiceOutcome Post(
      Resource Resource,
      IDtoRequestUri RequestUri,
      IDtoSearchParameterGeneric SearchParameterGeneric,
      IDtoRequestHeaders RequestHeaders,
      string ForceId = "")
    {
      if (RequestUri == null)
        throw new NullReferenceException("DtoFhirRequestUri can not be null.");
      if (SearchParameterGeneric == null)
        throw new NullReferenceException("SearchParameterGeneric can not be null.");
      //RequestHeaders can been null
      if (!string.IsNullOrWhiteSpace(ForceId))
      {
        //The id must be a valid GUID and is later validated as one.
        Guid TempGuid;
        if (!Guid.TryParseExact(ForceId, "D", out TempGuid))
        {
          throw new FormatException($"The 'ForceId' used by the server in a POST (add) request must be a valid GUID. Value given was: {ForceId}");
        }
      }

      IResourceServiceOutcome oServiceOperationOutcome = Common.CommonFactory.GetResourceServiceOutcome();

      ISearchParameterService SearchService = ICommonFactory.CreateSearchParameterService();
      ISearchParametersServiceOutcome SearchParametersServiceOutcomeBase = SearchService.ProcessBaseSearchParameters(SearchParameterGeneric);

      if (SearchParametersServiceOutcomeBase.FhirOperationOutcome != null)
      {
        oServiceOperationOutcome.ResourceResult = SearchParametersServiceOutcomeBase.FhirOperationOutcome;
        oServiceOperationOutcome.HttpStatusCode = SearchParametersServiceOutcomeBase.HttpStatusCode;
        oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcomeBase.SearchParameters.Format;
        return oServiceOperationOutcome;
      }

      if ((RequestHeaders != null) && (!string.IsNullOrWhiteSpace(RequestHeaders.IfNoneExist)))
      {
        IDtoSearchParameterGeneric SearchParameterGenericIfNoneExist = ICommonFactory.CreateDtoSearchParameterGeneric().Parse(RequestHeaders.IfNoneExist);
        ISearchParameterService SearchServiceIfNoneExist = ICommonFactory.CreateSearchParameterService();
        ISearchParametersServiceOutcome SearchParametersServiceOutcomeIfNoneExist = SearchService.ProcessSearchParameters(SearchParameterGenericIfNoneExist, SearchParameterService.SearchParameterServiceType.Bundle | SearchParameterService.SearchParameterServiceType.Resource, _CurrentResourceType, null);
        if (SearchParametersServiceOutcomeIfNoneExist.FhirOperationOutcome != null)
        {
          oServiceOperationOutcome.ResourceResult = SearchParametersServiceOutcomeIfNoneExist.FhirOperationOutcome;
          oServiceOperationOutcome.HttpStatusCode = SearchParametersServiceOutcomeIfNoneExist.HttpStatusCode;
          oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcomeIfNoneExist.SearchParameters.Format;
          return oServiceOperationOutcome;
        }

        IDatabaseOperationOutcome DatabaseOperationOutcomeIfNoneExist = _ResourceRepository.GetResourceBySearch(SearchParametersServiceOutcomeIfNoneExist.SearchParameters, false);
        if (DatabaseOperationOutcomeIfNoneExist.SearchTotal == 1)
        {
          //From FHIR Specification: One Match: The server ignore the post and returns 200 OK          
          oServiceOperationOutcome.ResourceResult = null;
          oServiceOperationOutcome.FhirResourceId = DatabaseOperationOutcomeIfNoneExist.ReturnedResourceList[0].FhirId;
          oServiceOperationOutcome.LastModified = DatabaseOperationOutcomeIfNoneExist.ReturnedResourceList[0].Received;
          oServiceOperationOutcome.IsDeleted = DatabaseOperationOutcomeIfNoneExist.ReturnedResourceList[0].IsDeleted;
          oServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Create;
          oServiceOperationOutcome.ResourceVersionNumber = DatabaseOperationOutcomeIfNoneExist.ReturnedResourceList[0].Version;
          oServiceOperationOutcome.RequestUri = RequestUri.FhirRequestUri;
          oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcomeBase.SearchParameters.Format;
          oServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.OK;
          oServiceOperationOutcome.SuccessfulTransaction = true;
          return oServiceOperationOutcome;
        }
        else if (DatabaseOperationOutcomeIfNoneExist.SearchTotal > 1)
        {
          //From FHIR Specification: Multiple matches: The server returns a 412 Precondition Failed error 
          //                         indicating the client's criteria were not selective enough
          oServiceOperationOutcome.ResourceResult = null;
          oServiceOperationOutcome.FhirResourceId = string.Empty;
          oServiceOperationOutcome.LastModified = null;
          oServiceOperationOutcome.IsDeleted = null;
          oServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Create;
          oServiceOperationOutcome.ResourceVersionNumber = string.Empty;
          oServiceOperationOutcome.RequestUri = RequestUri.FhirRequestUri;
          oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcomeBase.SearchParameters.Format;
          oServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.PreconditionFailed;
          return oServiceOperationOutcome;
        }
      }

      if (!string.IsNullOrWhiteSpace(Resource.Id))
      {
        string Message = string.Format("The create (POST) interaction creates a new resource in a server-assigned location. If the client wishes to have control over the id of a newly submitted resource, it should use the update interaction instead. The Resource provide was found to contain the id: {0}", Resource.Id);
        oServiceOperationOutcome.ResourceResult = FhirOperationOutcomeSupport.Create(OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Required, Message, new List<string>() { "Resource.Id" });
        oServiceOperationOutcome.HttpStatusCode = HttpStatusCode.BadRequest;
        oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcomeBase.SearchParameters.Format;
        return oServiceOperationOutcome;
      }
      //BatchTransaction Operation need to pre assign the id GUID inorder to update referances in the batch
      //That id is then passed into 'ForceId' and is then used for the POST (add), must always be a GUID
      if (!string.IsNullOrWhiteSpace(ForceId))
      {
        Resource.Id = ForceId;
      }
      //All good commit the resource.
      oServiceOperationOutcome = SetResource(Resource, RequestUri, RestEnum.CrudOperationType.Create);
      oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcomeBase.SearchParameters.Format;
      oServiceOperationOutcome.SuccessfulTransaction = true;
      return oServiceOperationOutcome;
    }

    //Update (PUT)
    // PUT: URL/FhirApi/Patient/5
    public virtual IResourceServiceOutcome Put(
      string ResourceId,
      Resource Resource,
      IDtoRequestUri RequestUri,
      IDtoSearchParameterGeneric SearchParameterGeneric,
      IDtoRequestHeaders RequestHeaders)
    {
      if (string.IsNullOrWhiteSpace(ResourceId))
        throw new NullReferenceException("ResourceId can not be null or empty.");
      if (Resource == null)
        throw new NullReferenceException("Resource can not be null.");
      if (RequestUri == null)
        throw new NullReferenceException("DtoFhirRequestUri can not be null.");
      if (SearchParameterGeneric == null)
        throw new NullReferenceException("SearchParameterGeneric can not be null.");
      if (RequestHeaders == null)
        throw new NullReferenceException("RequestHeaders can not be null.");

      IResourceServiceOutcome oServiceOperationOutcome = Common.CommonFactory.GetResourceServiceOutcome();

      ISearchParameterService SearchService = ICommonFactory.CreateSearchParameterService();
      ISearchParametersServiceOutcome SearchParametersServiceOutcome = SearchService.ProcessBaseSearchParameters(SearchParameterGeneric);
      if (SearchParametersServiceOutcome.FhirOperationOutcome != null)
      {
        oServiceOperationOutcome.ResourceResult = SearchParametersServiceOutcome.FhirOperationOutcome;
        oServiceOperationOutcome.HttpStatusCode = SearchParametersServiceOutcome.HttpStatusCode;
        oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcome.SearchParameters.Format;
        return oServiceOperationOutcome;
      }

      if (string.IsNullOrWhiteSpace(Resource.Id) || Resource.Id != ResourceId)
      {
        string Message = String.Format("The Resource SHALL contain an id element that has an identical value to the [id] in the URL. The id in the resource was: '{0}' and the [id] in the URL was: '{1}'", Resource.Id, ResourceId);
        oServiceOperationOutcome.ResourceResult = FhirOperationOutcomeSupport.Create(OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Required, Message);
        oServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
        return oServiceOperationOutcome;
      }

      //Create Resource's Meta element if not found and update its last updated property to now
      if (Resource.Meta == null)
      {
        Resource.Meta = new Meta();
      }

      //Check db for existence of this Resource 
      IDatabaseOperationOutcome DatabaseOperationOutcomeGet = _ResourceRepository.GetResourceByFhirID(ResourceId);

      if (DatabaseOperationOutcomeGet.ReturnedResourceList != null && DatabaseOperationOutcomeGet.ReturnedResourceList.Count == 1)
      {
        if (!string.IsNullOrWhiteSpace(RequestHeaders.IfMatch) &&
          (RequestHeaders.IfMatch != DatabaseOperationOutcomeGet.ReturnedResourceList[0].Version))
        {
          string Message = $"Version aware update conflict error. HTTP Header 'If-Match' used. The version intended to be updated was: '{RequestHeaders.IfMatch}' the current version found on the server was: '{DatabaseOperationOutcomeGet.ReturnedResourceList[0].Version}'.";
          oServiceOperationOutcome.ResourceResult = FhirOperationOutcomeSupport.Create(OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Conflict, Message);
          oServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Update;
          oServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.Conflict;
          return oServiceOperationOutcome;
        }
        else
        {
          //The resource has been found so update its version number based on the older resource              
          Resource.Meta.VersionId = Common.Tools.ResourceVersionNumber.Increment(DatabaseOperationOutcomeGet.ReturnedResourceList[0].Version);

          oServiceOperationOutcome = SetResource(Resource, RequestUri, RestEnum.CrudOperationType.Update);
          oServiceOperationOutcome.SuccessfulTransaction = true;
          //If the found resource is IsDeleted = true then need to return Status = 201 (Created) after 
          //performing to update, not 201 (OK)
          if (DatabaseOperationOutcomeGet.ReturnedResourceList[0].IsDeleted)
          {
            oServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.Created;
          }
        }
      }
      else if (DatabaseOperationOutcomeGet.ReturnedResourceList != null && DatabaseOperationOutcomeGet.ReturnedResourceList.Count == 0)
      {
        if (!string.IsNullOrWhiteSpace(RequestHeaders.IfMatch))
        {
          string Message = $"Version aware update conflict. HTTP Header 'If-Match: {RequestHeaders.IfMatch}' used, and no previous resource can be found in the server.";
          oServiceOperationOutcome.ResourceResult = FhirOperationOutcomeSupport.Create(OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Conflict, Message);
          oServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.Conflict;
          return oServiceOperationOutcome;
        }
        else
        {
          //This is a new resource so update its version number as 1 and create
          Resource.Meta.VersionId = Common.Tools.ResourceVersionNumber.FirstVersion();
          oServiceOperationOutcome = SetResource(Resource, RequestUri, RestEnum.CrudOperationType.Create);
          oServiceOperationOutcome.SuccessfulTransaction = true;
        }
      }

      oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcome.SearchParameters.Format;
      return oServiceOperationOutcome;
    }

    //Delete
    // DELETE: URL/FhirApi/Patient/5
    public virtual IResourceServiceOutcome Delete(
      string ResourceId,
      IDtoRequestUri RequestUri,
      IDtoSearchParameterGeneric SearchParameterGeneric)
    {
      if (string.IsNullOrWhiteSpace(ResourceId))
        throw new NullReferenceException("ResourceId can not be null or empty.");
      if (RequestUri == null)
        throw new NullReferenceException("DtoFhirRequestUri can not be null.");
      if (SearchParameterGeneric == null)
        throw new NullReferenceException("SearchParameterGeneric can not be null.");

      IResourceServiceOutcome oServiceOperationOutcome = Common.CommonFactory.GetResourceServiceOutcome();

      ISearchParameterService SearchService = ICommonFactory.CreateSearchParameterService();
      ISearchParametersServiceOutcome SearchParametersServiceOutcome = SearchService.ProcessBaseSearchParameters(SearchParameterGeneric);
      if (SearchParametersServiceOutcome.FhirOperationOutcome != null)
      {
        oServiceOperationOutcome.ResourceResult = SearchParametersServiceOutcome.FhirOperationOutcome;
        oServiceOperationOutcome.HttpStatusCode = SearchParametersServiceOutcome.HttpStatusCode;
        oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcome.SearchParameters.Format;
        return oServiceOperationOutcome;
      }

      IDatabaseOperationOutcome DatabaseOperationOutcomeGet = _ResourceRepository.GetResourceByFhirID(ResourceId, false);
      if (DatabaseOperationOutcomeGet.ReturnedResourceList.Count == 1 &&
        DatabaseOperationOutcomeGet.ReturnedResourceList[0].IsDeleted)
      {
        // The resource exists yet is already deleted, 
        //need to return the details about it but not the xml content 
        oServiceOperationOutcome.ResourceResult = null;
        oServiceOperationOutcome.FhirResourceId = ResourceId;
        oServiceOperationOutcome.LastModified = DatabaseOperationOutcomeGet.ReturnedResourceList[0].Received;
        oServiceOperationOutcome.IsDeleted = DatabaseOperationOutcomeGet.ReturnedResourceList[0].IsDeleted;
        oServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Delete;
        oServiceOperationOutcome.ResourceVersionNumber = DatabaseOperationOutcomeGet.ReturnedResourceList[0].Version;
        oServiceOperationOutcome.RequestUri = null;
        oServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
      }
      else
      {
        //There is one that is not already deleted or there is none
        //Either case calling the CommitResourceCollectionAsDeleted will return the correct result. 
        ICollection<string> ResourceIdsToBeDeleted = DatabaseOperationOutcomeGet.ReturnedResourceList.Where(x => x.IsDeleted == false).Select(x => x.FhirId).ToArray();
        oServiceOperationOutcome = SetResourceCollectionAsDeleted(ResourceIdsToBeDeleted);
      }

      oServiceOperationOutcome.FormatMimeType = SearchParametersServiceOutcome.SearchParameters.Format;
      oServiceOperationOutcome.SuccessfulTransaction = true;
      return oServiceOperationOutcome;
    }

    //ConditionalUpdate (PUT)
    //DELETE: URL/FhirApi/Patient?identifier=12345&family=millar&given=angus 
    public virtual IResourceServiceOutcome ConditionalPut(
      Resource Resource,
      IDtoRequestUri FhirRequestUri,
      IDtoSearchParameterGeneric SearchParameterGeneric)
    {
      if (Resource == null)
        throw new NullReferenceException($"Resource can not be null.");
      if (FhirRequestUri == null)
        throw new NullReferenceException($"RequestUri can not be null.");
      if (SearchParameterGeneric == null)
        throw new NullReferenceException($"SearchParameterGenericcan not be null.");

      IResourceServiceOutcome ServiceOperationOutcomeConditionalPut = Common.CommonFactory.GetResourceServiceOutcome();
      // GET: URL//FhirApi/Patient?family=Smith&given=John                        

      ISearchParameterService SearchService = ICommonFactory.CreateSearchParameterService();
      ISearchParametersServiceOutcome SearchParametersServiceOutcomeAll = SearchService.ProcessResourceSearchParameters(SearchParameterGeneric, SearchParameterService.SearchParameterServiceType.Base | SearchParameterService.SearchParameterServiceType.Resource, _CurrentResourceType);
      if (SearchParametersServiceOutcomeAll.FhirOperationOutcome != null)
      {
        ServiceOperationOutcomeConditionalPut.ResourceResult = SearchParametersServiceOutcomeAll.FhirOperationOutcome;
        ServiceOperationOutcomeConditionalPut.HttpStatusCode = SearchParametersServiceOutcomeAll.HttpStatusCode;
        ServiceOperationOutcomeConditionalPut.FormatMimeType = SearchParametersServiceOutcomeAll.SearchParameters.Format;
        return ServiceOperationOutcomeConditionalPut;
      }
      ServiceOperationOutcomeConditionalPut.FormatMimeType = SearchParametersServiceOutcomeAll.SearchParameters.Format;

      IDatabaseOperationOutcome DatabaseOperationOutcomeSearch = _ResourceRepository.GetResourceBySearch(SearchParametersServiceOutcomeAll.SearchParameters, false);
      if (DatabaseOperationOutcomeSearch.ReturnedResourceList.Count == 0)
      {
        //No resource found so do a normal Create, first clear any Resource Id that may 
        //be in the resource
        Resource.Id = string.Empty;
        ServiceOperationOutcomeConditionalPut = this.Post(Resource, FhirRequestUri, SearchParameterGeneric, null, null);
        ServiceOperationOutcomeConditionalPut.FormatMimeType = SearchParametersServiceOutcomeAll.SearchParameters.Format;
        //Don't set to true below as the POST above will set the bool based on it's own result
        //oServiceOperationOutcome.SuccessfulTransaction = true;
        return ServiceOperationOutcomeConditionalPut;
      }
      else if (DatabaseOperationOutcomeSearch.ReturnedResourceList.Count == 1)
      {
        if (!string.IsNullOrWhiteSpace(Resource.Id))
        {
          if (DatabaseOperationOutcomeSearch.ReturnedResourceList[0].FhirId != Resource.Id)
          {
            string Message = "The single Resource located by the search has a different Resource Id to that which was found in the Resource given. Either, remove the Id from the Resource given or re-evaluate the search parameters.";
            ServiceOperationOutcomeConditionalPut.ResourceResult = FhirOperationOutcomeSupport.Create(OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.BusinessRule, Message);
            ServiceOperationOutcomeConditionalPut.HttpStatusCode = HttpStatusCode.BadRequest;
            ServiceOperationOutcomeConditionalPut.FormatMimeType = SearchParametersServiceOutcomeAll.SearchParameters.Format;
            return ServiceOperationOutcomeConditionalPut;
          }
        }
        else
        {
          Resource.Id = DatabaseOperationOutcomeSearch.ReturnedResourceList[0].FhirId;
        }

        //Create Resource's Meta element if not found and update its last updated property to now
        if (Resource.Meta == null)
          Resource.Meta = new Meta();

        //A database resource has been found so update the new resource's version number based on the older resource              
        Resource.Meta.VersionId = Common.Tools.ResourceVersionNumber.Increment(DatabaseOperationOutcomeSearch.ReturnedResourceList[0].Version);
        ServiceOperationOutcomeConditionalPut = SetResource(Resource, FhirRequestUri, RestEnum.CrudOperationType.Update);
        ServiceOperationOutcomeConditionalPut.SuccessfulTransaction = true;
        ServiceOperationOutcomeConditionalPut.FormatMimeType = SearchParametersServiceOutcomeAll.SearchParameters.Format;
        return ServiceOperationOutcomeConditionalPut;
      }
      else
      {
        //more than one returned so PreconditionFailed.        
        ServiceOperationOutcomeConditionalPut.ResourceResult = null;
        ServiceOperationOutcomeConditionalPut.FhirResourceId = null;
        ServiceOperationOutcomeConditionalPut.LastModified = null;
        ServiceOperationOutcomeConditionalPut.IsDeleted = true;
        ServiceOperationOutcomeConditionalPut.OperationType = RestEnum.CrudOperationType.Update;
        ServiceOperationOutcomeConditionalPut.ResourceVersionNumber = null;
        ServiceOperationOutcomeConditionalPut.RequestUri = null;
        ServiceOperationOutcomeConditionalPut.FormatMimeType = SearchParametersServiceOutcomeAll.SearchParameters.Format;
        ServiceOperationOutcomeConditionalPut.HttpStatusCode = System.Net.HttpStatusCode.PreconditionFailed;
        return ServiceOperationOutcomeConditionalPut;
      }
    }

    //ConditionalDelete (Delete)
    //DELETE: URL/FhirApi/Patient?identifier=12345&family=millar&given=angus 
    public virtual IResourceServiceOutcome ConditionalDelete(
      IDtoRequestUri RequestUri,
      IDtoSearchParameterGeneric SearchParameterGeneric)
    {
      if (RequestUri == null)
        throw new NullReferenceException($"RequestUri can not be null.");
      if (SearchParameterGeneric == null)
        throw new NullReferenceException($"SearchParameterGenericcan not be null.");

      IResourceServiceOutcome ServiceOperationOutcomeConditionalDelete = Common.CommonFactory.GetResourceServiceOutcome();
      // GET: URL//FhirApi/Patient?family=Smith&given=John          

      ISearchParameterService SearchServiceBase = ICommonFactory.CreateSearchParameterService();
      ISearchParametersServiceOutcome SearchParametersServiceOutcomeBaseOnly = SearchServiceBase.ProcessBaseSearchParameters(SearchParameterGeneric);

      ISearchParameterService SearchServiceAll = ICommonFactory.CreateSearchParameterService();
      ISearchParametersServiceOutcome SearchParametersServiceOutcomeAll = SearchServiceAll.ProcessResourceSearchParameters(SearchParameterGeneric, SearchParameterService.SearchParameterServiceType.Base | SearchParameterService.SearchParameterServiceType.Resource, _CurrentResourceType);

      if (SearchParametersServiceOutcomeAll.FhirOperationOutcome != null)
      {
        ServiceOperationOutcomeConditionalDelete.ResourceResult = SearchParametersServiceOutcomeAll.FhirOperationOutcome;
        ServiceOperationOutcomeConditionalDelete.HttpStatusCode = SearchParametersServiceOutcomeAll.HttpStatusCode;
        ServiceOperationOutcomeConditionalDelete.FormatMimeType = SearchParametersServiceOutcomeBaseOnly.SearchParameters.Format;
        return ServiceOperationOutcomeConditionalDelete;
      }

      if ((SearchParametersServiceOutcomeAll.SearchParameters.SearchParametersList.Count - SearchParametersServiceOutcomeBaseOnly.SearchParameters.SearchParametersList.Count) <= 0 ||
        SearchParametersServiceOutcomeAll.SearchParameters.UnspportedSearchParameterList.Count != 0)
      {
        string Message = string.Empty;
        if ((SearchParametersServiceOutcomeAll.SearchParameters.SearchParametersList.Count - SearchParametersServiceOutcomeBaseOnly.SearchParameters.SearchParametersList.Count) <= 0)
        {
          Message = String.Format($"The Conditional Delete operation requires at least one valid Resource search parameter in order to be performed.");
        }
        else if (SearchParametersServiceOutcomeAll.SearchParameters.UnspportedSearchParameterList.Count != 0)
        {
          string UnspportedSearchParameterMessage = string.Empty;
          foreach (var item in SearchParametersServiceOutcomeAll.SearchParameters.UnspportedSearchParameterList)
          {
            UnspportedSearchParameterMessage = UnspportedSearchParameterMessage + $"Parameter: {item.RawParameter}, Reason: {item.ReasonMessage};";
          }
          Message = String.Format($"The Conditional Delete operation requires that all Search parameters are understood. The following supplied  parameters were not understood: {UnspportedSearchParameterMessage}");
        }
        ServiceOperationOutcomeConditionalDelete.ResourceResult = FhirOperationOutcomeSupport.Create(OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.BusinessRule, Message);
        ServiceOperationOutcomeConditionalDelete.HttpStatusCode = HttpStatusCode.PreconditionFailed;
        ServiceOperationOutcomeConditionalDelete.FormatMimeType = SearchParametersServiceOutcomeBaseOnly.SearchParameters.Format;
        return ServiceOperationOutcomeConditionalDelete;
      }

      IDatabaseOperationOutcome DatabaseOperationOutcomeSearch = _ResourceRepository.GetResourceBySearch(SearchParametersServiceOutcomeAll.SearchParameters, false);

      //There are zero or many or one to be deleted, note that GetResourceBySearch never returns deleted resource.
      ICollection<string> ResourceIdsToBeDeleted = DatabaseOperationOutcomeSearch.ReturnedResourceList.Select(x => x.FhirId).ToArray();
      ServiceOperationOutcomeConditionalDelete = SetResourceCollectionAsDeleted(ResourceIdsToBeDeleted);
      ServiceOperationOutcomeConditionalDelete.FormatMimeType = SearchParametersServiceOutcomeAll.SearchParameters.Format;
      ServiceOperationOutcomeConditionalDelete.SuccessfulTransaction = true;
      return ServiceOperationOutcomeConditionalDelete;
    }

    //DeleteHistoryIndexes
    //DELETE: URL/FhirApi/Patient?$ClearHistoryIndexes
    public virtual IResourceServiceOutcome DeleteHistoryIndexes(
      IDtoRequestUri RequestUri,
      IDtoSearchParameterGeneric SearchParameterGeneric)
    {
      if (RequestUri == null)
        throw new NullReferenceException($"RequestUri can not be null.");
      if (SearchParameterGeneric == null)
        throw new NullReferenceException($"SearchParameterGenericcan not be null.");

      IResourceServiceOutcome ServiceOutcome = Common.CommonFactory.GetResourceServiceOutcome();
      // GET: URL//FhirApi/Patient?family=Smith&given=John          

      ISearchParameterService SearchServiceBase = ICommonFactory.CreateSearchParameterService();
      ISearchParametersServiceOutcome SearchParametersServiceOutcomeBaseOnly = SearchServiceBase.ProcessBaseSearchParameters(SearchParameterGeneric);
      if (SearchParametersServiceOutcomeBaseOnly.FhirOperationOutcome != null)
      {
        ServiceOutcome.ResourceResult = SearchParametersServiceOutcomeBaseOnly.FhirOperationOutcome;
        ServiceOutcome.HttpStatusCode = SearchParametersServiceOutcomeBaseOnly.HttpStatusCode;
        ServiceOutcome.FormatMimeType = SearchParametersServiceOutcomeBaseOnly.SearchParameters.Format;
        return ServiceOutcome;
      }

      int NumberOfIndexRowsDeleted = _ResourceRepository.DeleteNonCurrentResourceIndexes();

      ServiceOutcome.HttpStatusCode = System.Net.HttpStatusCode.OK;
      ServiceOutcome.OperationType = RestEnum.CrudOperationType.Update;
      ServiceOutcome.SuccessfulTransaction = true;
      Parameters ParametersResult = new Parameters();
      ParametersResult.Id = this._CurrentResourceType.GetLiteral() + "_Response";
      ParametersResult.Parameter = new List<Parameters.ParameterComponent>();
      var Param = new Parameters.ParameterComponent();
      ParametersResult.Parameter.Add(Param);
      Param.Name = $"{this._CurrentResourceType.GetLiteral()}_TotalIndexesDeletedCount";
      var Count = new FhirDecimal();
      Count.Value = NumberOfIndexRowsDeleted;
      Param.Value = Count;
      ServiceOutcome.ResourceResult = ParametersResult;
      ServiceOutcome.FormatMimeType = SearchParametersServiceOutcomeBaseOnly.SearchParameters.Format;
      return ServiceOutcome;
    }

    public virtual void AddResourceIndexs(List<Common.BusinessEntities.Dto.DtoServiceSearchParameterLight> ServiceSearchParameterLightList, IDtoRequestUri FhirRequestUri)
    {
      _ResourceRepository.AddCurrentResourceIndex(ServiceSearchParameterLightList, FhirRequestUri);
    }

    public DateTimeOffset? GetLastCurrentResourceLastUpdatedValue()
    {
      return _ResourceRepository.GetLastCurrentResourceLastUpdatedValue();
    }

    public int GetTotalCurrentResourceCount()
    {
      return _ResourceRepository.GetTotalCurrentResourceCount();
    }

  }
}
