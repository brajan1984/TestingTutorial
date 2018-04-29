using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Extensions;
using TestingWorkshop.Models;

namespace TestingWorkshop.Services
{
    public class Solution
    {
        private readonly IHoursProcessor _processor;
        private readonly IFullHoursGenerator _generator;

        public Solution(IHoursProcessor processor, IFullHoursGenerator generator)
        {
            _processor = processor;
            _generator = generator;
        }

        public string Execute(int A, int B, int C, int D, int E, int F)
        {
            var digits = new List<int>();
            digits.Add(A);
            digits.Add(B);
            digits.Add(C);
            digits.Add(D);
            digits.Add(E);
            digits.Add(F);

            List<Hour24Model> correctHours = _generator.GetAllPossibleHours(digits).ToList();

            return _processor.Process(correctHours);
        }
    }
}
