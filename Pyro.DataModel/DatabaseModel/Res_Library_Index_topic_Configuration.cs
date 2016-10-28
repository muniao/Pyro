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
  public class Res_Library_Index_topic_Configuration : EntityTypeConfiguration<Res_Library_Index_topic>
  {

    public Res_Library_Index_topic_Configuration()
    {
      HasKey(x => x.Res_Library_Index_topicID).Property(x => x.Res_Library_Index_topicID).IsRequired();
      Property(x => x.Code).IsRequired();
      Property(x => x.System).IsOptional();
      HasRequired(x => x.Res_Library).WithMany(x => x.topic_List).WillCascadeOnDelete(true);
    }
  }
}