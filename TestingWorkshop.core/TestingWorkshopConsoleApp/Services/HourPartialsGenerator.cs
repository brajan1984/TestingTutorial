using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Extensions;
using TestingWorkshop.Models;

namespace TestingWorkshop.Services
{
    public class HourPartialsGenerator : IHourPartialsGenerator
    {
        private readonly IUniqueNumberGenerator _generator;

        public HourPartialsGenerator(IUniqueNumberGenerator generator)
        {
            _generator = generator;
        }

        public IEnumerable<Hour24Model> FillAllHours(IEnumerable<int> digits)
        {
            return _generator.GenerateUniqueNumbers(digits)
                .Select(n => {
                    int firstDigit = n / 10;
                    int secondDigit = n - firstDigit * 10;

                    return new TimeNoModel { first = firstDigit, second = secondDigit };
                })
                .Where(h => ValidateHour(h))
                .Select(gh => new Hour24Model { hour = gh })
                .ToList();
        }

        public IEnumerable<Hour24Model> FillAllMinutes(IEnumerable<int> digits, IEnumerable<Hour24Model> modelsWithHour)
        {
            return FillAllHourPartials(digits, modelsWithHour, (m, v) => m.minutes = v);
        }

        private IEnumerable<Hour24Model> FillAllHourPartials(IEnumerable<int> digits, IEnumerable<Hour24Model> modelsWithHour, Action<Hour24Model, TimeNoModel> modelModificator)
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
                    .Where(v => ValidateMinSec(v))
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

        public IEnumerable<Hour24Model> FillAllSeconds(IEnumerable<int> digits, IEnumerable<Hour24Model> modelsWitHourAndSeconds)
        {
            return FillAllHourPartials(digits, modelsWitHourAndSeconds, (m, v) => m.seconds = v);
        }

        private static bool ValidateHour(TimeNoModel hour)
        {
            return hour.fullNo >= 0 && hour.fullNo <= 24;
        }

        private static bool ValidateMinSec(TimeNoModel hour)
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
