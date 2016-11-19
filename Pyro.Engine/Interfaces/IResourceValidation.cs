﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;

namespace Pyro.Engine.Interfaces
{
  public interface IResourceValidation
  {
    Common.Interfaces.Service.IResourceValidationOperationOutcome Validate(Resource Resource);
  }
}
