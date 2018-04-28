using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Models;

namespace TestingWorkshop.Extensions
{
    public static class TimeNoModelExtensions
    {
        public static string ToDoubleDigitString(this TimeNoModel model)
        {
            return $"{model.first}{model.second}";
        }
    }
}
