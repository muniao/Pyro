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
  public class Res_Patient_Index_phonetic_Configuration : EntityTypeConfiguration<Res_Patient_Index_phonetic>
  {

    public Res_Patient_Index_phonetic_Configuration()
    {
      HasKey(x => x.Res_Patient_Index_phoneticID).Property(x => x.Res_Patient_Index_phoneticID).IsRequired();
      Property(x => x.String).IsRequired();
      HasRequired(x => x.Res_Patient).WithMany(x => x.phonetic_List).WillCascadeOnDelete(true);
    }
  }
}