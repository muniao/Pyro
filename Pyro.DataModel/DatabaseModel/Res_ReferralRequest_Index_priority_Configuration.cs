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
  public class Res_ReferralRequest_Index_priority_Configuration : EntityTypeConfiguration<Res_ReferralRequest_Index_priority>
  {

    public Res_ReferralRequest_Index_priority_Configuration()
    {
      HasKey(x => x.Res_ReferralRequest_Index_priorityID).Property(x => x.Res_ReferralRequest_Index_priorityID).IsRequired();
      Property(x => x.Code).IsRequired();
      Property(x => x.System).IsOptional();
      HasRequired(x => x.Res_ReferralRequest).WithMany(x => x.priority_List).WillCascadeOnDelete(true);
    }
  }
}