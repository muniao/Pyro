﻿using System;
using System.Collections.Generic;
using Pyro.Common.Interfaces;
using Pyro.Common.Interfaces.Services;
using Pyro.Common.Interfaces.Repositories;
using Pyro.Common.BusinessEntities.Search.Validation;
using Pyro.Common.Enum;
using Hl7.Fhir.Model;

namespace Pyro.Engine.Services
{
  public abstract class BaseResourceServices : CommonServices, IBaseResourceServices, ICommonServices, IBaseServices
  {
    protected IResourceRepository _ResourceRepository = null;

    //Constructor for dependency injection
    public BaseResourceServices(IUnitOfWork IUnitOfWork)
      : base(IUnitOfWork) { }

    protected FHIRAllTypes _CurrentResourceType;

    public FHIRAllTypes CurrentResourceType
    {
      get
      {
        return _CurrentResourceType;
      }
    }

    public FHIRAllTypes SetCurrentResourceType
    {
      set
      {
        _CurrentResourceType = value;
      }
    }

    //GET    
    public virtual IServiceOperationOutcome Get(IServiceRequest PyroServiceRequest)
    {
      IServiceOperationOutcome oPyroServiceOperationOutcome = Common.CommonFactory.GetPyroServiceOperationOutcome();
      oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Read;

      switch (PyroServiceRequest.ServiceRequestType)
      {
        case ServiceEnums.ServiceRequestType.History:
          {
            if (string.IsNullOrWhiteSpace(PyroServiceRequest.VersionId))
            {
              // GET All history for Id
              // GET URL/FhirApi/Patient/5/_history
              //Read all history
              SearchParametersValidationOperationOutcome oSearchParametersValidationOperationOutcome = SearchParameterValidator.ValidateHistoryParameters(new Common.BusinessEntities.Search.DtoSearchParameterGeneric(PyroServiceRequest.SearchParams));
              if (oSearchParametersValidationOperationOutcome.FhirOperationOutcome != null)
              {
                oPyroServiceOperationOutcome.SearchValidationOperationOutcome = oSearchParametersValidationOperationOutcome;
                return oPyroServiceOperationOutcome;
              }

              oSearchParametersValidationOperationOutcome.SearchParameters.PrimaryRootUrlStore = PyroServiceRequest.FhirRequestUri.PrimaryRootUrlStore;
              IDatabaseOperationOutcome DatabaseOperationOutcome = _ResourceRepository.GetResourceHistoryByFhirID(PyroServiceRequest.ResourceId, oSearchParametersValidationOperationOutcome.SearchParameters);

              oPyroServiceOperationOutcome.ResourceResult = Support.FhirBundleSupport.CreateBundle(DatabaseOperationOutcome.ReturnedResourceList,
                                                                                                   Bundle.BundleType.History,
                                                                                                   PyroServiceRequest.FhirRequestUri,
                                                                                                   DatabaseOperationOutcome.SearchTotal,
                                                                                                   DatabaseOperationOutcome.PagesTotal,
                                                                                                   DatabaseOperationOutcome.PageRequested);
              oPyroServiceOperationOutcome.FhirResourceId = string.Empty;
              oPyroServiceOperationOutcome.LastModified = null;
              oPyroServiceOperationOutcome.IsDeleted = null;
              oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Read;
              oPyroServiceOperationOutcome.ResourceVersionNumber = string.Empty;
              oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
              oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
              oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.OK;

              return oPyroServiceOperationOutcome;
            }
            else

            {
              // GET by FhirId and FhirVId
              // GET URL/FhirApi/Patient/5/_history/2    
              IDatabaseOperationOutcome DatabaseOperationOutcome = _ResourceRepository.GetResourceByFhirIDAndVersionNumber(PyroServiceRequest.ResourceId, PyroServiceRequest.VersionId);
              if (DatabaseOperationOutcome.ReturnedResourceList != null && DatabaseOperationOutcome.ReturnedResourceList.Count == 1)
              {
                oPyroServiceOperationOutcome.ResourceResult = Support.FhirResourceSerializationSupport.Serialize(DatabaseOperationOutcome.ReturnedResourceList[0].Xml);
                oPyroServiceOperationOutcome.FhirResourceId = DatabaseOperationOutcome.ReturnedResourceList[0].FhirId;
                oPyroServiceOperationOutcome.LastModified = DatabaseOperationOutcome.ReturnedResourceList[0].Received;
                oPyroServiceOperationOutcome.IsDeleted = DatabaseOperationOutcome.ReturnedResourceList[0].IsDeleted;
                oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Read;
                oPyroServiceOperationOutcome.ResourceVersionNumber = DatabaseOperationOutcome.ReturnedResourceList[0].Version;
                oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
                oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
                oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.OK;
              }
              else
              {
                oPyroServiceOperationOutcome.ResourceResult = null;
                oPyroServiceOperationOutcome.FhirResourceId = string.Empty;
                oPyroServiceOperationOutcome.LastModified = null;
                oPyroServiceOperationOutcome.IsDeleted = null;
                oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Read;
                oPyroServiceOperationOutcome.ResourceVersionNumber = string.Empty;
                oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
                oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
                oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
              }
              return oPyroServiceOperationOutcome;
            }
          }
        case ServiceEnums.ServiceRequestType.Read:
          {
            // GET by FhirId
            // GET URL/FhirApi/Patient/5    
            IDatabaseOperationOutcome DatabaseOperationOutcome = _ResourceRepository.GetResourceByFhirID(PyroServiceRequest.ResourceId, true);
            if (DatabaseOperationOutcome.ReturnedResourceList.Count == 1 && !DatabaseOperationOutcome.ReturnedResourceList[0].IsDeleted)
            {
              oPyroServiceOperationOutcome.ResourceResult = Support.FhirResourceSerializationSupport.Serialize(DatabaseOperationOutcome.ReturnedResourceList[0].Xml);
              oPyroServiceOperationOutcome.FhirResourceId = DatabaseOperationOutcome.ReturnedResourceList[0].FhirId;
              oPyroServiceOperationOutcome.LastModified = DatabaseOperationOutcome.ReturnedResourceList[0].Received;
              oPyroServiceOperationOutcome.IsDeleted = DatabaseOperationOutcome.ReturnedResourceList[0].IsDeleted;
              oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Read;
              oPyroServiceOperationOutcome.ResourceVersionNumber = DatabaseOperationOutcome.ReturnedResourceList[0].Version;
              oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
              oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
              oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            else if (DatabaseOperationOutcome.ReturnedResourceList.Count == 1 && DatabaseOperationOutcome.ReturnedResourceList[0].IsDeleted)
            {
              oPyroServiceOperationOutcome.ResourceResult = null;
              oPyroServiceOperationOutcome.FhirResourceId = DatabaseOperationOutcome.ReturnedResourceList[0].FhirId;
              oPyroServiceOperationOutcome.LastModified = DatabaseOperationOutcome.ReturnedResourceList[0].Received;
              oPyroServiceOperationOutcome.IsDeleted = DatabaseOperationOutcome.ReturnedResourceList[0].IsDeleted;
              oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Read;
              oPyroServiceOperationOutcome.ResourceVersionNumber = DatabaseOperationOutcome.ReturnedResourceList[0].Version;
              oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
              oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
              oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.Gone;
            }
            else
            {
              oPyroServiceOperationOutcome.ResourceResult = null;
              oPyroServiceOperationOutcome.FhirResourceId = null;
              oPyroServiceOperationOutcome.LastModified = null;
              oPyroServiceOperationOutcome.IsDeleted = null;
              oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Read;
              oPyroServiceOperationOutcome.ResourceVersionNumber = null;
              oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
              oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
              oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return oPyroServiceOperationOutcome;
          }
        case ServiceEnums.ServiceRequestType.Search:
          {
            // GET by Search
            // GET: URL//FhirApi/Patient?family=Smith&given=John
            SearchParametersValidationOperationOutcome oSearchParametersValidationOperationOutcome = SearchParameterValidator.Validate(_CurrentResourceType, new Common.BusinessEntities.Search.DtoSearchParameterGeneric(PyroServiceRequest.SearchParams));
            if (oSearchParametersValidationOperationOutcome.FhirOperationOutcome != null)
            {
              oPyroServiceOperationOutcome.SearchValidationOperationOutcome = oSearchParametersValidationOperationOutcome;
              return oPyroServiceOperationOutcome;
            }
            oSearchParametersValidationOperationOutcome.SearchParameters.PrimaryRootUrlStore = PyroServiceRequest.FhirRequestUri.PrimaryRootUrlStore;
            IDatabaseOperationOutcome DatabaseOperationOutcome = _ResourceRepository.GetResourceBySearch(oSearchParametersValidationOperationOutcome.SearchParameters);

            oPyroServiceOperationOutcome.ResourceResult = Support.FhirBundleSupport.CreateBundle(DatabaseOperationOutcome.ReturnedResourceList,
                                                                                                   Bundle.BundleType.Searchset,
                                                                                                   PyroServiceRequest.FhirRequestUri,
                                                                                                   DatabaseOperationOutcome.SearchTotal,
                                                                                                   DatabaseOperationOutcome.PagesTotal,
                                                                                                   DatabaseOperationOutcome.PageRequested);
            oPyroServiceOperationOutcome.FhirResourceId = string.Empty;
            oPyroServiceOperationOutcome.LastModified = null;
            oPyroServiceOperationOutcome.IsDeleted = null;
            oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Read;
            oPyroServiceOperationOutcome.ResourceVersionNumber = string.Empty;
            oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
            oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
            oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.OK;
            return oPyroServiceOperationOutcome;
          }
        default:
          throw new System.ComponentModel.InvalidEnumArgumentException(PyroServiceRequest.ServiceRequestType.ToString(), (int)PyroServiceRequest.ServiceRequestType, typeof(ServiceEnums.ServiceRequestType));
      }
    }

    // Add
    // POST: URL/FhirApi/Patient
    public virtual IServiceOperationOutcome Post(IServiceRequest PyroServiceRequest)
    {
      IServiceOperationOutcome oPyroServiceOperationOutcome = Common.CommonFactory.GetPyroServiceOperationOutcome();
      oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Create;

      if (!string.IsNullOrWhiteSpace(PyroServiceRequest.Resource.Id))
      {
        var oIssueComponent = new OperationOutcome.IssueComponent();
        oIssueComponent.Severity = OperationOutcome.IssueSeverity.Error;
        oIssueComponent.Code = OperationOutcome.IssueType.Required;
        oIssueComponent.Details = new CodeableConcept("http://hl7.org/fhir/operation-outcome", "MSG_INVALID_ID", String.Format("Id not accepted, type"));
        oIssueComponent.Details.Text = String.Format("The create (POST) interaction creates a new resource in a server-assigned location. If the client wishes to have control over the id of a newly submitted resource, it should use the update interaction instead. The Resource provide was found to contain the id: {0}", PyroServiceRequest.Resource.Id);
        oIssueComponent.Diagnostics = oIssueComponent.Details.Text;
        var oOperationOutcome = new OperationOutcome();
        oOperationOutcome.Issue = new List<OperationOutcome.IssueComponent>() { oIssueComponent };
        oPyroServiceOperationOutcome.ResourceValidationOperationOutcome = new Validation.ResourceValidationOperationOutcome();
        oPyroServiceOperationOutcome.ResourceValidationOperationOutcome.FhirOperationOutcome = oOperationOutcome;
        oPyroServiceOperationOutcome.ResourceValidationOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
        return oPyroServiceOperationOutcome;
      }
      //Assign GUID as FHIR id;
      PyroServiceRequest.Resource.Id = Guid.NewGuid().ToString();

      //Validation of resource        
      Interfaces.IResourceValidation Validation = Pyro.Engine.Validation.ResourceValidationFactory.GetValidationInstance(CurrentResourceType);
      IResourceValidationOperationOutcome oResourceValidationOperationOutcome = Validation.Validate(PyroServiceRequest.Resource);
      if (oResourceValidationOperationOutcome.HasError)
      {
        oPyroServiceOperationOutcome.ResourceValidationOperationOutcome = oResourceValidationOperationOutcome;
        return oPyroServiceOperationOutcome;
      }

      if (PyroServiceRequest.Resource.Meta == null)
        PyroServiceRequest.Resource.Meta = new Meta();
      string ResourceVersionNumber = Common.Tools.ResourceVersionNumber.FirstVersion();
      PyroServiceRequest.Resource.Meta.VersionId = ResourceVersionNumber;
      PyroServiceRequest.Resource.Meta.LastUpdated = DateTimeOffset.Now;

      IDatabaseOperationOutcome DatabaseOperationOutcome = _ResourceRepository.AddResource(PyroServiceRequest.Resource, PyroServiceRequest.FhirRequestUri);
      if (DatabaseOperationOutcome.ReturnedResourceList.Count == 1)
      {
        oPyroServiceOperationOutcome.ResourceResult = Support.FhirResourceSerializationSupport.Serialize(DatabaseOperationOutcome.ReturnedResourceList[0].Xml);
        oPyroServiceOperationOutcome.FhirResourceId = DatabaseOperationOutcome.ReturnedResourceList[0].FhirId;
        oPyroServiceOperationOutcome.LastModified = DatabaseOperationOutcome.ReturnedResourceList[0].Received;
        oPyroServiceOperationOutcome.IsDeleted = DatabaseOperationOutcome.ReturnedResourceList[0].IsDeleted;
        oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Create;
        oPyroServiceOperationOutcome.ResourceVersionNumber = DatabaseOperationOutcome.ReturnedResourceList[0].Version;
        oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
        oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
        oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.Created;
      }
      else
      {
        oPyroServiceOperationOutcome.ResourceResult = null;
        oPyroServiceOperationOutcome.FhirResourceId = string.Empty;
        oPyroServiceOperationOutcome.LastModified = null;
        oPyroServiceOperationOutcome.IsDeleted = null;
        oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Create;
        oPyroServiceOperationOutcome.ResourceVersionNumber = string.Empty;
        oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
        oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
        oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
      }
      return oPyroServiceOperationOutcome;
    }

    //Update
    // PUT: URL/FhirApi/Patient/5
    public virtual IServiceOperationOutcome Put(IServiceRequest PyroServiceRequest)
    {
      IServiceOperationOutcome oPyroServiceOperationOutcome = Common.CommonFactory.GetPyroServiceOperationOutcome();
      oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Update;
      if (string.IsNullOrWhiteSpace(PyroServiceRequest.Resource.Id) || PyroServiceRequest.Resource.Id != PyroServiceRequest.ResourceId)
      {
        var oIssueComponent = new OperationOutcome.IssueComponent();
        oIssueComponent.Severity = OperationOutcome.IssueSeverity.Error;
        oIssueComponent.Code = OperationOutcome.IssueType.Required;
        oIssueComponent.Details = new CodeableConcept("http://hl7.org/fhir/operation-outcome", "MSG_INVALID_ID", String.Format("Id not accepted, type"));
        oIssueComponent.Details.Text = String.Format("The Resource SHALL contain an id element that has an identical value to the [id] in the URL. The id in the resource was: '{0}' and the [id] in the URL was: '{1}'", PyroServiceRequest.Resource.Id, PyroServiceRequest.ResourceId);
        oIssueComponent.Diagnostics = oIssueComponent.Details.Text;
        var oOperationOutcome = new OperationOutcome();
        oOperationOutcome.Issue = new List<OperationOutcome.IssueComponent>() { oIssueComponent };
        oPyroServiceOperationOutcome.ResourceValidationOperationOutcome = new Validation.ResourceValidationOperationOutcome();
        oPyroServiceOperationOutcome.ResourceValidationOperationOutcome.FhirOperationOutcome = oOperationOutcome;
        oPyroServiceOperationOutcome.ResourceValidationOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
        return oPyroServiceOperationOutcome;
      }

      Interfaces.IResourceValidation Validation = Pyro.Engine.Validation.ResourceValidationFactory.GetValidationInstance(CurrentResourceType);
      IResourceValidationOperationOutcome oResourceValidationOperationOutcome = Validation.Validate(PyroServiceRequest.Resource);
      if (oResourceValidationOperationOutcome.HasError)
      {
        oPyroServiceOperationOutcome.ResourceValidationOperationOutcome = oResourceValidationOperationOutcome;
        return oPyroServiceOperationOutcome;
      }

      //Create Resource's Meta element if not found and update its last updated property to now
      if (PyroServiceRequest.Resource.Meta == null)
        PyroServiceRequest.Resource.Meta = new Meta();
      PyroServiceRequest.Resource.Meta.LastUpdated = DateTimeOffset.Now;

      //Check db for existence of this Resource 
      IDatabaseOperationOutcome DatabaseOperationOutcomeGet = _ResourceRepository.GetResourceByFhirID(PyroServiceRequest.ResourceId);
      if (DatabaseOperationOutcomeGet.ReturnedResourceList.Count != 0)
      {
        //The resource has been found so update its version number based on the older resource              
        PyroServiceRequest.Resource.Meta.VersionId = Common.Tools.ResourceVersionNumber.Increment(DatabaseOperationOutcomeGet.ReturnedResourceList[0].Version);
        IDatabaseOperationOutcome DatabaseOperationOutcomeUpdate = _ResourceRepository.UpdateResource(PyroServiceRequest.Resource.Meta.VersionId, PyroServiceRequest.Resource, PyroServiceRequest.FhirRequestUri);
        if (DatabaseOperationOutcomeUpdate.ReturnedResourceList != null && DatabaseOperationOutcomeUpdate.ReturnedResourceList.Count == 1)
        {
          oPyroServiceOperationOutcome.ResourceResult = Support.FhirResourceSerializationSupport.Serialize(DatabaseOperationOutcomeUpdate.ReturnedResourceList[0].Xml);
          oPyroServiceOperationOutcome.FhirResourceId = DatabaseOperationOutcomeUpdate.ReturnedResourceList[0].FhirId;
          oPyroServiceOperationOutcome.LastModified = DatabaseOperationOutcomeUpdate.ReturnedResourceList[0].Received;
          oPyroServiceOperationOutcome.IsDeleted = DatabaseOperationOutcomeUpdate.ReturnedResourceList[0].IsDeleted;
          oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Update;
          oPyroServiceOperationOutcome.ResourceVersionNumber = DatabaseOperationOutcomeUpdate.ReturnedResourceList[0].Version;
          oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
          oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
          oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.OK;
        }
        else
        {
          oPyroServiceOperationOutcome.ResourceResult = null;
          oPyroServiceOperationOutcome.FhirResourceId = string.Empty;
          oPyroServiceOperationOutcome.LastModified = null;
          oPyroServiceOperationOutcome.IsDeleted = null;
          oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Update;
          oPyroServiceOperationOutcome.ResourceVersionNumber = string.Empty;
          oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
          oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
          oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
        }
      }
      else
      {
        //This is a new resource so update its version number as 1
        PyroServiceRequest.Resource.Meta.VersionId = Common.Tools.ResourceVersionNumber.FirstVersion();
        IDatabaseOperationOutcome DatabaseOperationOutcomeAdd = _ResourceRepository.AddResource(PyroServiceRequest.Resource, PyroServiceRequest.FhirRequestUri);
        if (DatabaseOperationOutcomeAdd.ReturnedResourceList != null && DatabaseOperationOutcomeAdd.ReturnedResourceList.Count == 1)
        {
          oPyroServiceOperationOutcome.ResourceResult = Support.FhirResourceSerializationSupport.Serialize(DatabaseOperationOutcomeAdd.ReturnedResourceList[0].Xml);
          oPyroServiceOperationOutcome.FhirResourceId = DatabaseOperationOutcomeAdd.ReturnedResourceList[0].FhirId;
          oPyroServiceOperationOutcome.LastModified = DatabaseOperationOutcomeAdd.ReturnedResourceList[0].Received;
          oPyroServiceOperationOutcome.IsDeleted = DatabaseOperationOutcomeAdd.ReturnedResourceList[0].IsDeleted;
          oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Create;
          oPyroServiceOperationOutcome.ResourceVersionNumber = DatabaseOperationOutcomeAdd.ReturnedResourceList[0].Version;
          oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
          oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
          oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.Created;
        }
        else
        {
          oPyroServiceOperationOutcome.ResourceResult = null;
          oPyroServiceOperationOutcome.FhirResourceId = string.Empty;
          oPyroServiceOperationOutcome.LastModified = null;
          oPyroServiceOperationOutcome.IsDeleted = null;
          oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Create;
          oPyroServiceOperationOutcome.ResourceVersionNumber = string.Empty;
          oPyroServiceOperationOutcome.RequestUri = PyroServiceRequest.FhirRequestUri.FhirUri.Uri;
          oPyroServiceOperationOutcome.ServiceRootUri = PyroServiceRequest.FhirRequestUri.FhirUri.ServiceRootUrl;
          oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
        }
      }
      return oPyroServiceOperationOutcome;
    }

    //Delete
    // DELETE: URL/FhirApi/Patient/5
    public virtual IServiceOperationOutcome Delete(string FhirResourceId)
    {
      IServiceOperationOutcome oPyroServiceOperationOutcome = Common.CommonFactory.GetPyroServiceOperationOutcome();
      IDatabaseOperationOutcome DatabaseOperationOutcome = _ResourceRepository.GetResourceByFhirID(FhirResourceId);
      if (DatabaseOperationOutcome.ReturnedResourceList.Count == 1)
      {
        //Resource exists so..
        if (!DatabaseOperationOutcome.ReturnedResourceList[0].IsDeleted)
        {
          string NewResourceVersionNumber = Common.Tools.ResourceVersionNumber.Increment(DatabaseOperationOutcome.ReturnedResourceList[0].Version);
          _ResourceRepository.UpdateResouceAsDeleted(FhirResourceId, NewResourceVersionNumber);

          oPyroServiceOperationOutcome.ResourceResult = null;
          oPyroServiceOperationOutcome.FhirResourceId = FhirResourceId;
          oPyroServiceOperationOutcome.LastModified = null;
          oPyroServiceOperationOutcome.IsDeleted = null;
          oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Delete;
          oPyroServiceOperationOutcome.ResourceVersionNumber = NewResourceVersionNumber;
          oPyroServiceOperationOutcome.RequestUri = null;
          oPyroServiceOperationOutcome.ServiceRootUri = null;
          oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
        }
        else
        {
          oPyroServiceOperationOutcome.ResourceResult = null;
          oPyroServiceOperationOutcome.FhirResourceId = FhirResourceId;
          oPyroServiceOperationOutcome.LastModified = DatabaseOperationOutcome.ReturnedResourceList[0].Received;
          oPyroServiceOperationOutcome.IsDeleted = DatabaseOperationOutcome.ReturnedResourceList[0].IsDeleted;
          oPyroServiceOperationOutcome.OperationType = RestEnum.CrudOperationType.Delete;
          oPyroServiceOperationOutcome.ResourceVersionNumber = DatabaseOperationOutcome.ReturnedResourceList[0].Version;
          oPyroServiceOperationOutcome.RequestUri = null;
          oPyroServiceOperationOutcome.ServiceRootUri = null;
          oPyroServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
        }
      }
      return oPyroServiceOperationOutcome;
    }

  }
}
