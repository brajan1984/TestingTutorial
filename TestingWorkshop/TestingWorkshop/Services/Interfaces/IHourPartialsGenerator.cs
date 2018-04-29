using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Models;

namespace TestingWorkshop.Services
{
    public interface IHourPartialsGenerator
    {
        IEnumerable<Hour24Model> FillAllHours(IEnumerable<int> digits);
        IEnumerable<Hour24Model> FillAllMinutes(IEnumerable<int> digits, IEnumerable<Hour24Model> modelsWithHour);
        IEnumerable<Hour24Model> FillAllSeconds(IEnumerable<int> digits, IEnumerable<Hour24Model> modelsWithHour);
    }
}
