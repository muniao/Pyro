﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_EligibilityResponse : ResourceIndexBase
  {
    public int Res_EligibilityResponseID {get; set;}
    public DateTimeOffset? created_DateTimeOffset {get; set;}
    public string disposition_String {get; set;}
    public string organizationidentifier_Code {get; set;}
    public string organizationidentifier_System {get; set;}
    public string organizationreference_VersionId {get; set;}
    public string organizationreference_FhirId {get; set;}
    public string organizationreference_Type {get; set;}
    public virtual Blaze_RootUrlStore organizationreference_Url { get; set; }
    public int? organizationreference_Url_Blaze_RootUrlStoreID { get; set; }
    public string outcome_Code {get; set;}
    public string outcome_System {get; set;}
    public string requestidentifier_Code {get; set;}
    public string requestidentifier_System {get; set;}
    public string requestorganizationidentifier_Code {get; set;}
    public string requestorganizationidentifier_System {get; set;}
    public string requestorganizationreference_VersionId {get; set;}
    public string requestorganizationreference_FhirId {get; set;}
    public string requestorganizationreference_Type {get; set;}
    public virtual Blaze_RootUrlStore requestorganizationreference_Url { get; set; }
    public int? requestorganizationreference_Url_Blaze_RootUrlStoreID { get; set; }
    public string requestprovideridentifier_Code {get; set;}
    public string requestprovideridentifier_System {get; set;}
    public string requestproviderreference_VersionId {get; set;}
    public string requestproviderreference_FhirId {get; set;}
    public string requestproviderreference_Type {get; set;}
    public virtual Blaze_RootUrlStore requestproviderreference_Url { get; set; }
    public int? requestproviderreference_Url_Blaze_RootUrlStoreID { get; set; }
    public string requestreference_VersionId {get; set;}
    public string requestreference_FhirId {get; set;}
    public string requestreference_Type {get; set;}
    public virtual Blaze_RootUrlStore requestreference_Url { get; set; }
    public int? requestreference_Url_Blaze_RootUrlStoreID { get; set; }
    public ICollection<Res_EligibilityResponse_History> Res_EligibilityResponse_History_List { get; set; }
    public ICollection<Res_EligibilityResponse_Index_identifier> identifier_List { get; set; }
    public ICollection<Res_EligibilityResponse_Index_profile> profile_List { get; set; }
    public ICollection<Res_EligibilityResponse_Index_security> security_List { get; set; }
    public ICollection<Res_EligibilityResponse_Index_tag> tag_List { get; set; }
   
    public Res_EligibilityResponse()
    {
      this.identifier_List = new HashSet<Res_EligibilityResponse_Index_identifier>();
      this.profile_List = new HashSet<Res_EligibilityResponse_Index_profile>();
      this.security_List = new HashSet<Res_EligibilityResponse_Index_security>();
      this.tag_List = new HashSet<Res_EligibilityResponse_Index_tag>();
      this.Res_EligibilityResponse_History_List = new HashSet<Res_EligibilityResponse_History>();
    }
  }
}
