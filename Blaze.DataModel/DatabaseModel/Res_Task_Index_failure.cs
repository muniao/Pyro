﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Task_Index_failure
  {
    public int Res_Task_Index_failureID {get; set;}
    public string Code {get; set;}
    public string System {get; set;}
    public virtual Res_Task Res_Task { get; set; }
   
    public Res_Task_Index_failure()
    {
    }
  }
}

