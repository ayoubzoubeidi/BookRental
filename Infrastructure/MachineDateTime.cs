﻿using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;
public class MachineDateTime : IDateTime
{
    public DateTime Now => DateTime.Now;
}
