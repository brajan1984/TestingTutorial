using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Models;

namespace xTestingWorkshopTests.Comparer
{
    public class TimeNoModelComparer : IEqualityComparer<TimeNoModel>
    {
        public bool Equals(TimeNoModel x, TimeNoModel y)
        {
            return x.first == y.first && x.second == y.second;
        }

        public int GetHashCode(TimeNoModel obj)
        {
            return 13 * obj.first.GetHashCode() + 7 * obj.second.GetHashCode();
        }
    }
}
