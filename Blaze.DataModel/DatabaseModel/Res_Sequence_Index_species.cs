﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Sequence_Index_species
  {
    public int Res_Sequence_Index_speciesID {get; set;}
    public string Code {get; set;}
    public string System {get; set;}
    public virtual Res_Sequence Res_Sequence { get; set; }
   
    public Res_Sequence_Index_species()
    {
    }
  }
}

