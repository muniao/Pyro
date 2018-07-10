﻿using Hl7.Fhir.Model;
using Pyro.Common.PyroHealthInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.Common.Enum;
using Pyro.Common.Attributes;

namespace Pyro.Common.PyroHealthInformation
{
  public class PyroServerCodeSystem
  {
    public enum Codes
    {
      [EnumLiteral("ActiveCompartment")]
      ActiveCompartment,
      [EnumLiteral("CompartmentDefinition")]
      CompartmentDefinition,
      [EnumLiteral("HiServiceCallAudit")]
      HiServiceCallAudit,
      [EnumLiteral("ServerInstance")]
      ServerInstance,
      [EnumLiteral("Protected")]
      Protected,
    }
    public static readonly string System = "https://pyrohealth.net/fhir/CodeSystem/pyrofhirserver";
    public static Coding Protected
    {
      get
      {
        return new Coding() { Code = Codes.Protected.GetPyroLiteral(), Display = "Protected Resource", System = PyroServerCodeSystem.System };
      }
    }

    public static CodeSystem GetCodeSystem()
    {
      var CodeSystemUpdateDate = new DateTimeOffset(2018, 07, 06, 10, 00, 00, new TimeSpan(8, 0, 0));
      var CodeSys = new CodeSystem();
      CodeSys.Id = "pyrofhirserver";
      CodeSys.Meta = new Meta();
      //When the CodeSystem was last editied key to driving the update in prod servers
      CodeSys.Meta.LastUpdated = CodeSystemUpdateDate;
      CodeSys.Meta.Tag = new List<Coding>();
      //Protected Resource
      CodeSys.Meta.Tag.Add(PyroServerCodeSystem.Protected);
      CodeSys.Url = PyroServerCodeSystem.System;
      CodeSys.Version = "1.00";
      CodeSys.Name = "PyroFHIRServerCodeSystem";
      CodeSys.Title = "The Pyro Server CodeSystem";
      CodeSys.Status = PublicationStatus.Active;
      CodeSys.Experimental = false;
      CodeSys.DateElement = new FhirDateTime(CodeSystemUpdateDate);
      CodeSys.Publisher = "Pyrohealth.net";
      var AngusContactDetail = Common.PyroHealthInformation.PyroHealthContactDetailAngusMillar.GetContactDetail();
      CodeSys.Contact = new List<ContactDetail>() { AngusContactDetail };
      CodeSys.Description = new Markdown("List of codes used throughout the Pyro FHIR Server to identity concepts key to the operation of the server.");
      CodeSys.CaseSensitive = true;
      CodeSys.Compositional = false;
      CodeSys.Count = CodeSys.Concept.Count;
      CodeSys.Content = CodeSystem.CodeSystemContentMode.Complete;
      CodeSys.Concept = new List<CodeSystem.ConceptDefinitionComponent>()
      {
        new CodeSystem.ConceptDefinitionComponent()
        {
           Code = Codes.ActiveCompartment.GetPyroLiteral(),
           Display = "Active Compartment",
           Definition = "Used to indicate that a CompartmentDefinition Resource is used as an active Compartment in the FHIR server.",
        },
        new CodeSystem.ConceptDefinitionComponent()
        {
           Code = Codes.CompartmentDefinition.GetPyroLiteral(),
           Display = "CompartmentDefinition",
           Definition = "A FHIR CompartmentDefinition resource definied for use in a Pyro FHIR server",
        },
        new CodeSystem.ConceptDefinitionComponent()
        {
           Code = Codes.HiServiceCallAudit.GetPyroLiteral(),
           Display = "HI Service Call Audit",
           Definition = "An Audit event in response to a web service call made to the Australian Healthcare Identifer Service (HI Service) by a Pyro FHIR server",
        },
        new CodeSystem.ConceptDefinitionComponent()
        {
           Code = Codes.ServerInstance.GetPyroLiteral(),
           Display = "Server Instance",
           Definition = "An instance of a Pyro FHIR server",
        },
        new CodeSystem.ConceptDefinitionComponent()
        {
           Code = Codes.Protected.GetPyroLiteral(),
           Display = "Protected Resource",
           Definition = "Protected entities and resource can not be updated or deleted",
        },
      };
      return CodeSys;
    }
  }


}
