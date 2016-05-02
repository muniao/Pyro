﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Claim
  {
    public int Res_ClaimID {get; set;}
    public string FhirId {get; set;}
    public int versionId {get; set;}
    public DateTimeOffset lastUpdated {get; set;}
    public string XmlBlob {get; set;}
    public DateTimeOffset? created_DateTimeOffset {get; set;}
    public string facilityidentifier_Code {get; set;}
    public string facilityidentifier_System {get; set;}
    public string facilityreference_FhirId {get; set;}
    public string facilityreference_Type {get; set;}
    public virtual Blaze_RootUrlStore facilityreference_Url { get; set; }
    public int? facilityreference_Url_Blaze_RootUrlStoreID { get; set; }
    public string organizationidentifier_Code {get; set;}
    public string organizationidentifier_System {get; set;}
    public string organizationreference_FhirId {get; set;}
    public string organizationreference_Type {get; set;}
    public virtual Blaze_RootUrlStore organizationreference_Url { get; set; }
    public int? organizationreference_Url_Blaze_RootUrlStoreID { get; set; }
    public string patientidentifier_Code {get; set;}
    public string patientidentifier_System {get; set;}
    public string patientreference_FhirId {get; set;}
    public string patientreference_Type {get; set;}
    public virtual Blaze_RootUrlStore patientreference_Url { get; set; }
    public int? patientreference_Url_Blaze_RootUrlStoreID { get; set; }
    public string priority_Code {get; set;}
    public string priority_System {get; set;}
    public string provideridentifier_Code {get; set;}
    public string provideridentifier_System {get; set;}
    public string providerreference_FhirId {get; set;}
    public string providerreference_Type {get; set;}
    public virtual Blaze_RootUrlStore providerreference_Url { get; set; }
    public int? providerreference_Url_Blaze_RootUrlStoreID { get; set; }
    public string targetidentifier_Code {get; set;}
    public string targetidentifier_System {get; set;}
    public string targetreference_FhirId {get; set;}
    public string targetreference_Type {get; set;}
    public virtual Blaze_RootUrlStore targetreference_Url { get; set; }
    public int? targetreference_Url_Blaze_RootUrlStoreID { get; set; }
    public string use_Code {get; set;}
    public string use_System {get; set;}
    public ICollection<Res_Claim_Index_identifier> identifier_List { get; set; }
   
    public Res_Claim()
    {
      this.identifier_List = new HashSet<Res_Claim_Index_identifier>();
    }
  }
}

