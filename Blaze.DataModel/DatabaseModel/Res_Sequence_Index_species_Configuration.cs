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

namespace Blaze.DataModel.DatabaseModel
{
  public class Res_Sequence_Index_species_Configuration : EntityTypeConfiguration<Res_Sequence_Index_species>
  {

    public Res_Sequence_Index_species_Configuration()
    {
      HasKey(x => x.Res_Sequence_Index_speciesID).Property(x => x.Res_Sequence_Index_speciesID).IsRequired();
      Property(x => x.Code).IsRequired();
      Property(x => x.System).IsOptional();
      HasRequired(x => x.Res_Sequence).WithMany(x => x.species_List).WillCascadeOnDelete(true);
    }
  }
}