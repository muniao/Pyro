﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Conformance_Index_securityservice
  {
    public int Res_Conformance_Index_securityserviceID {get; set;}
    public string Code {get; set;}
    public string System {get; set;}
    public virtual Res_Conformance Res_Conformance { get; set; }
   
    public Res_Conformance_Index_securityservice()
    {
    }
  }
}

