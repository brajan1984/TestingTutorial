using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Models;

namespace TestingWorkshop.Services
{
    public class FullHoursGenerator : IFullHoursGenerator
    {
        private readonly IHourPartialsGenerator _generator;

        public FullHoursGenerator(IHourPartialsGenerator generator)
        {
            _generator = generator;
        }

        public IEnumerable<Hour24Model> GetAllPossibleHours(IEnumerable<int> digits)
        {
            var correctHour = new List<int>();

            var hours = _generator.FillAllHours(digits);

            var allHours = _generator.FillAllMinutes(digits, hours);

            var fullHours = _generator.FillAllSeconds(digits, allHours);

            return fullHours.ToList();
        }
    }
}
