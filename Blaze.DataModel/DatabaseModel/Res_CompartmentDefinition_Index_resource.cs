﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_CompartmentDefinition_Index_resource
  {
    public int Res_CompartmentDefinition_Index_resourceID {get; set;}
    public string Code {get; set;}
    public string System {get; set;}
    public virtual Res_CompartmentDefinition Res_CompartmentDefinition { get; set; }
   
    public Res_CompartmentDefinition_Index_resource()
    {
    }
  }
}

