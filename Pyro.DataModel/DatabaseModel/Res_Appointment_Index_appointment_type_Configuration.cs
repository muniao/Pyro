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
  public class Res_Appointment_Index_appointment_type_Configuration : EntityTypeConfiguration<Res_Appointment_Index_appointment_type>
  {

    public Res_Appointment_Index_appointment_type_Configuration()
    {
      HasKey(x => x.Res_Appointment_Index_appointment_typeID).Property(x => x.Res_Appointment_Index_appointment_typeID).IsRequired();
      Property(x => x.Code).IsRequired();
      Property(x => x.System).IsOptional();
      HasRequired(x => x.Res_Appointment).WithMany(x => x.appointment_type_List).WillCascadeOnDelete(true);
    }
  }
}