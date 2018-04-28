using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Extensions;
using TestingWorkshop.Models;
using TestingWorkshop.Services;

namespace TestingWorkshop
{
    

    public class Solution
    {
        private readonly IHoursProcessor _processor;
        private readonly IUniqueNumberGenerator _generator;

        public Solution(IHoursProcessor processor, IUniqueNumberGenerator generator)
        {
            _processor = processor;
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

        private List<int> ExplodeHourModel(Hour24Model model)
        {
            List<int> exploded = new List<int>();

            if(model.hour != null)
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

        public List<Hour24Model> GetAllPossibleHours(List<int> digits)
        {
            var correctHour = new List<int>();

            var hours = FillAllHours(digits).Select(gh => new Hour24Model { hour = gh });

            var allHours = FillAllHourPartials(digits, hours, (m, v) => m.minutes = v);

            var fullHours = FillAllHourPartials(digits, allHours, (m, v) => m.seconds = v);

            return fullHours.ToList();
        }

        public string solution(int A, int B, int C, int D, int E, int F)
        {
            var digits = new List<int>();
            digits.Add(A);
            digits.Add(B);
            digits.Add(C);
            digits.Add(D);
            digits.Add(E);
            digits.Add(F);

            List<Hour24Model> correctHours = GetAllPossibleHours(digits);

            var printResult = correctHours.ParseHoursCollection();
            Console.WriteLine(printResult);

            string result = "NOT POSSIBLE";

            result = _processor.Process(correctHours);

            return result;
        }

        static bool validateHour(TimeNoModel hour)
        {
            return hour.fullNo >= 0 && hour.fullNo <= 24;
        }

        static bool validateMinSec(TimeNoModel hour)
        {
            return hour.fullNo >= 0 && hour.fullNo <= 60;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var processor = new HourProcessor();
            var generator = new TwoDigitsUniqueNumberGenerator();
            var proc = new Solution(processor, generator);

            var hour = proc.solution(1, 8, 3, 2, 6, 4);
            

            Console.WriteLine(hour);

            Console.ReadKey();
        }
    }
}
