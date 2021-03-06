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
using Pyro.DataLayer.DbModel.Entity;
using Pyro.Common.Database;

namespace Pyro.DataLayer.DbModel.DatabaseContextConfig
{
  public class ServiceBaseUrlContextConfig : EntityTypeConfiguration<_ServiceBaseUrl>
  {
    public ServiceBaseUrlContextConfig()
    {
      ToTable("_BaseUrl");
      HasKey(x => x.Id).Property(x => x.Id).IsRequired();
      Property(x => x.Url).IsRequired()
        .HasMaxLength(StaticDatabaseInfo.BaseDatabaseFieldLength.StringMaxLength)
        .HasColumnAnnotation(IndexAnnotation.AnnotationName,
        new IndexAnnotation(new IndexAttribute("ix_Url") { IsUnique = true }));
      Property(x => x.IsServersPrimaryUrlRoot).IsRequired();
    }
  }
}
