﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_ImagingStudy_Index__profile : UriIndex
  {
    public int Res_ImagingStudy_Index__profileID {get; set;}
    public virtual Res_ImagingStudy Res_ImagingStudy { get; set; }
   
    public Res_ImagingStudy_Index__profile()
    {
    }
  }
}
