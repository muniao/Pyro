﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure.Annotations;

//This is an Auto generated file do not change it's contents!!.

namespace Pyro.DataModel.DatabaseModel
{
  public class Res_MedicationDispense_Configuration : EntityTypeConfiguration<Res_MedicationDispense>
  {

    public Res_MedicationDispense_Configuration()
    {
      HasKey(x => x.Res_MedicationDispenseID).Property(x => x.Res_MedicationDispenseID).IsRequired();
      Property(x => x.IsDeleted).IsRequired();
      Property(x => x.FhirId).IsRequired().HasMaxLength(500).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_FhirId") { IsUnique = true }));
      Property(x => x.lastUpdated).IsRequired();
      Property(x => x.versionId).IsRequired();
      Property(x => x.XmlBlob).IsRequired();
      Property(x => x.destination_VersionId).IsOptional();
      Property(x => x.destination_FhirId).IsOptional();
      Property(x => x.destination_Type).IsOptional();
      HasOptional(x => x.destination_Url);
      HasOptional<ServiceRootURL_Store>(x => x.destination_Url).WithMany().HasForeignKey(x => x.destination_ServiceRootURL_StoreID);
      Property(x => x.dispenser_VersionId).IsOptional();
      Property(x => x.dispenser_FhirId).IsOptional();
      Property(x => x.dispenser_Type).IsOptional();
      HasOptional(x => x.dispenser_Url);
      HasOptional<ServiceRootURL_Store>(x => x.dispenser_Url).WithMany().HasForeignKey(x => x.dispenser_ServiceRootURL_StoreID);
      Property(x => x.identifier_Code).IsOptional();
      Property(x => x.identifier_System).IsOptional();
      Property(x => x.medication_VersionId).IsOptional();
      Property(x => x.medication_FhirId).IsOptional();
      Property(x => x.medication_Type).IsOptional();
      HasOptional(x => x.medication_Url);
      HasOptional<ServiceRootURL_Store>(x => x.medication_Url).WithMany().HasForeignKey(x => x.medication_ServiceRootURL_StoreID);
      Property(x => x.patient_VersionId).IsOptional();
      Property(x => x.patient_FhirId).IsOptional();
      Property(x => x.patient_Type).IsOptional();
      HasOptional(x => x.patient_Url);
      HasOptional<ServiceRootURL_Store>(x => x.patient_Url).WithMany().HasForeignKey(x => x.patient_ServiceRootURL_StoreID);
      Property(x => x.status_Code).IsOptional();
      Property(x => x.status_System).IsOptional();
      Property(x => x.whenhandedover_DateTimeOffset).IsOptional();
      Property(x => x.whenprepared_DateTimeOffset).IsOptional();
    }
  }
}