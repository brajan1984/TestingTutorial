using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Models;

namespace TestingWorkshop.Services
{
    public interface IHourGenerator
    {
        IEnumerable<Hour24Model> FillAllHours(List<int> digits);
        IEnumerable<Hour24Model> FillAllHourPartials(IEnumerable<int> digits, IEnumerable<Hour24Model> modelsWithHour, Action<Hour24Model, TimeNoModel> modelModificator);
    }
}
