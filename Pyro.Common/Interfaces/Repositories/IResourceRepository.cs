﻿using System.Collections.Generic;
using Pyro.Common.BusinessEntities.Dto;
using Pyro.Common.BusinessEntities.Search;
using Pyro.Common.Interfaces.UriSupport;
using Pyro.Common.Interfaces.Service;
using Hl7.Fhir.Model;

namespace Pyro.Common.Interfaces.Repositories
{
  public interface IResourceRepository
  {
    IDatabaseOperationOutcome GetResourceBySearch(DtoSearchParameters DtoSearchParameters, bool WithXml = false);
    IDatabaseOperationOutcome AddResource(Resource Resource, IDtoFhirRequestUri FhirRequestUri);
    IDatabaseOperationOutcome UpdateResource(string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri);
    IDatabaseOperationOutcome UpdateResouceAsDeleted(string FhirResourceId, string ResourceVersion);
    IDatabaseOperationOutcome UpdateResouceListAsDeleted(ICollection<DtoResource> ResourceCollection);
    IDatabaseOperationOutcome GetResourceHistoryByFhirID(string FhirResourceId, DtoSearchParameters DtoSearchParameters);
    IDatabaseOperationOutcome GetResourceByFhirIDAndVersionNumber(string FhirResourceId, string ResourceVersionNumber);
    IDatabaseOperationOutcome GetResourceByFhirID(string FhirResourceId, bool WithXml = false);
  }
}
