﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
//using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Pyro.WebApi.Providers;
using Pyro.WebApi.Models;
using IdentityServer3.AccessTokenValidation;

namespace Pyro.WebApi
{
  public partial class Startup
  {    
    // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
    public void ConfigureAuth(IAppBuilder app)
    {
      // Enable the application to use a cookie to store information for the signed in user
      // and to use a cookie to temporarily store information about a user logging in with a third party login provider

      //app.UseCookieAuthentication(new CookieAuthenticationOptions());
      //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
      if (Pyro.Common.Global.WebConfigProperties.FHIRApiAuthentication())
      {
        //Forces this AuthorizationAttribute on all controlers
        HttpConfiguration.Filters.Add(new Pyro.WebApi.Authorization.SwitchableAuthorizationAttribute());

        //Connects to the external Authorization service
        string AuthorityUrl = Pyro.Common.Global.WebConfigProperties.AuthenticationServerUrl();
        try
        {
          app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
          {
            ClientId = "PyroFhirApi",
            ClientSecret = "prometheus.apiResource",
            Authority = AuthorityUrl,
            ValidationMode = ValidationMode.Local,
            RequiredScopes = Pyro.Smart.Scopes.ScopeStringGenerator.GetAllUserAndPatientScopes()
          });
        }
        catch (Exception Exec)
        {
          Common.Logging.Logger.Log.Fatal(Exec, $"The Pyro FHIR server is unable to connect to the Token Authentication service at: {AuthorityUrl}");
        }
      }

    }
  }
}
