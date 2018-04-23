﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Models;

namespace TestingWorkshop.Services
{
    public interface IHoursProcessor
    {
        string Process(List<Hour24Model> hours);
    }
}
