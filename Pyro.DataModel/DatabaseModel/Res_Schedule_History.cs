﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_Schedule_History : ResourceIndexBase
  {
    public int Res_Schedule_HistoryID {get; set;}
    public virtual Res_Schedule Res_Schedule { get; set; }
   
    public Res_Schedule_History()
    {
    }
  }
}
