﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_Basic_Index__tag : TokenIndex
  {
    public int Res_Basic_Index__tagID {get; set;}
    public virtual Res_Basic Res_Basic { get; set; }
   
    public Res_Basic_Index__tag()
    {
    }
  }
}
