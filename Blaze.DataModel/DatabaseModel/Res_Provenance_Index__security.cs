﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Provenance_Index__security : TokenIndex
  {
    public int Res_Provenance_Index__securityID {get; set;}
    public virtual Res_Provenance Res_Provenance { get; set; }
   
    public Res_Provenance_Index__security()
    {
    }
  }
}
