﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using Pyro.Common.Tools.FhirNarrative;
using Pyro.Common.Exceptions;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using System.Net.Http.Extensions.Compression.Core.Compressors;

namespace Pyro.WebApi
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      //Enable CORS
      config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

      // Web API configuration and services
      // Configure Web API to use only bearer token authentication.
      config.SuppressDefaultHostAuthentication();
      config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

      //Add the Fhir Media Formatters to the Web API Pipeline
      //The order matters here, and remember the requester asks for the format they want in the request header with:
      //Content-Type: application/fhir+xml
      //Accept: application/fhir+xml      
      config.Formatters.Clear();
      config.Formatters.Add(new Pyro.Common.Formatters.FhirJsonMediaTypeFormatter());
      config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
      config.Formatters.Add(new Pyro.Common.Formatters.FhirXmlMediaTypeFormatter());
      config.Formatters.Add(new System.Net.Http.Formatting.XmlMediaTypeFormatter());
      config.Formatters.Add(new System.Net.Http.Formatting.FormUrlEncodedMediaTypeFormatter());

      //Add Exception Handler
      //var IFhirExceptionFilter = (IFhirExceptionFilter)config.DependencyResolver.GetService(typeof(IFhirExceptionFilter));
      //config.Filters.Add(IFhirExceptionFilter);


      config.MapHttpAttributeRoutes();

      //You need to add the compression handler as the last applied message handler on outgoing requests, and the first one on incoming requests.
      config.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));



      //    config.Routes.MapHttpRoute(
      //        name: "DefaultApi",
      //        routeTemplate: "api/{controller}/{id}",
      //        defaults: new { id = RouteParameter.Optional }
      //    );
      //}
    }
  }
}
