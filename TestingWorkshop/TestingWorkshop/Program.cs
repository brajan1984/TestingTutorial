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

        public IEnumerable<TimeNoModel> GetAllHours(List<int> digits)
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

        public List<Hour24Model> GetAllPossibleHours(List<int> digits)
        {
            var correctHour = new List<int>();

            var hours = GetAllHours(digits).Select(gh => new Hour24Model { hour = gh }).ToList();

            var allHours = new List<Hour24Model>();

            foreach (var hour in hours)
            {
                var allPossibleMinutes = _generator.GenerateUniqueNumbersExcluding(digits, new List<int> { hour.hour.first, hour.hour.second })
                    .Select(n => {
                        int firstDigit = n / 10;
                        int secondDigit = n - firstDigit * 10;

                        return new TimeNoModel { first = firstDigit, second = secondDigit };
                    })
                    .Where(v => validateMinSec(v))
                    .ToList();

                allPossibleMinutes.ForEach(min =>
                {
                    var copy = hour.Copy();
                    copy.minutes = min;
                    allHours.Add(copy);
                });
            }

            var fullHours = new List<Hour24Model>();

            foreach (var hour in allHours)
            {
                var allPossibleMinutes = _generator.GenerateUniqueNumbersExcluding(digits, new List<int> { hour.hour.first, hour.hour.second, hour.minutes.first, hour.minutes.second, })
                    .Select(n => {
                        int firstDigit = n / 10;
                        int secondDigit = n - firstDigit * 10;

                        return new TimeNoModel { first = firstDigit, second = secondDigit };
                    })
                    .Where(v => validateMinSec(v))
                    .ToList();

                allPossibleMinutes.ForEach(sec =>
                {
                    var copy = hour.Copy();
                    copy.seconds = sec;
                    fullHours.Add(copy);
                });
            }

            return fullHours;
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
