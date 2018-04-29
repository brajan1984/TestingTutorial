using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorkshop.Services
{
    public interface IUniqueNumberGenerator
    {
        List<int> GenerateUniqueNumbersExcluding(IEnumerable<int> digits, IEnumerable<int> exclude);
        List<int> GenerateUniqueNumbers(IEnumerable<int> digits);
    }
}
