using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Extensions;
using TestingWorkshop.Models;

namespace TestingWorkshop.Services
{
    public class HourGenerator : IHourGenerator
    {
        private readonly IUniqueNumberGenerator _generator;

        public HourGenerator(IUniqueNumberGenerator generator)
        {
            _generator = generator;
        }

        public IEnumerable<TimeNoModel> FillAllHours(List<int> digits)
        {
            return _generator.GenerateUniqueNumbers(digits)
                .Select(n => {
                    int firstDigit = n / 10;
                    int secondDigit = n - firstDigit * 10;

                    return new TimeNoModel { first = firstDigit, second = secondDigit };
                })
                .Where(h => validateHour(h))
                .ToList();
        }

        static bool validateHour(TimeNoModel hour)
        {
            return hour.fullNo >= 0 && hour.fullNo <= 24;
        }

        public IEnumerable<Hour24Model> FillAllHourPartials(IEnumerable<int> digits, IEnumerable<Hour24Model> modelsWithHour, Action<Hour24Model, TimeNoModel> modelModificator)
        {
            var allFilledModels = new List<Hour24Model>();

            foreach (var hour in modelsWithHour)
            {
                var allPossibleValues = _generator.GenerateUniqueNumbersExcluding(digits, ExplodeHourModel(hour))
                    .Select(n => {
                        int firstDigit = n / 10;
                        int secondDigit = n - firstDigit * 10;

                        return new TimeNoModel { first = firstDigit, second = secondDigit };
                    })
                    .Where(v => validateMinSec(v))
                    .ToList();

                allPossibleValues.ForEach(model =>
                {
                    var copy = hour.Copy();
                    modelModificator(copy, model);
                    allFilledModels.Add(copy);
                });
            }

            return allFilledModels;
        }
        
        static bool validateMinSec(TimeNoModel hour)
        {
            return hour.fullNo >= 0 && hour.fullNo <= 60;
        }

        private List<int> ExplodeHourModel(Hour24Model model)
        {
            List<int> exploded = new List<int>();

            if (model.hour != null)
            {
                exploded.AddRange(new int[] { model.hour.first, model.hour.second });
            }

            if (model.minutes != null)
            {
                exploded.AddRange(new int[] { model.minutes.first, model.minutes.second });
            }

            if (model.seconds != null)
            {
                exploded.AddRange(new int[] { model.seconds.first, model.seconds.second });
            }

            return exploded;
        }

    }
}
