﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_SearchParameter_Index_component : ReferenceIndex
  {
    public int Res_SearchParameter_Index_componentID {get; set;}
    public virtual Res_SearchParameter Res_SearchParameter { get; set; }
   
    public Res_SearchParameter_Index_component()
    {
    }
  }
}
