﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Questionnaire_Index_context
  {
    public int Res_Questionnaire_Index_contextID {get; set;}
    public string Code {get; set;}
    public string System {get; set;}
    public virtual Res_Questionnaire Res_Questionnaire { get; set; }
   
    public Res_Questionnaire_Index_context()
    {
    }
  }
}

