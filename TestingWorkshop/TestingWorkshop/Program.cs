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

        public Solution(IHoursProcessor processor)
        {
            _processor = processor;
        }

        public IEnumerable<TimeNoModel> GetAllHours(List<int> digits)
        {
            return GenerateNumbers(digits).Where(h => validateHour(h)).ToList();
        }

        public List<TimeNoModel> GetUniqueCombinations(List<int> allDigits, List<TimeNoModel> allCurrentCombinations)
        {
            var excludedDigits = new List<int>();

            foreach (var usedCombination in allCurrentCombinations)
            {
                excludedDigits.Add(usedCombination.first);
                excludedDigits.Add(usedCombination.second);
            }

            var combinations = GenerateNumbersExcept(allDigits, excludedDigits);

            return combinations;
        }

        public List<TimeNoModel> GenerateNumbersExcept(IEnumerable<int> digits, IEnumerable<int> except)
        {
            var digitsFiltered = digits.ToList();

            except.ToList().ForEach(toRemove =>
            {
                digitsFiltered = digitsFiltered.GroupBy(s => s)
                    .SelectMany(g => g.Key.Equals(toRemove) ? g.Skip(1) : g).ToList();
            });

            return GenerateNumbers(digitsFiltered);
        }

        public List<TimeNoModel> GenerateNumbers(IEnumerable<int> digits)
        {
            if (digits.Count() <= 1)
            {
                throw new ArgumentException("There should be two or more digits");
            }

            var result = new List<TimeNoModel>();

            var testDigits = digits.ToList();

            foreach (var firstDigit in testDigits)
            {
                var digitsForNext = testDigits.ToList();

                int firstNo = firstDigit;
                digitsForNext.Remove(firstNo);

                foreach (var secondNo in digitsForNext)
                {
                    var seconds = new TimeNoModel() { first = firstNo, second = secondNo };
                    result.Add(seconds);
                }
            }

            return result;
        }

        public List<Hour24Model> GetAllPossibleHours(List<int> digits)
        {
            var correctHour = new List<int>();

            var hours = GetAllHours(digits).Select(gh => new Hour24Model { hour = gh }).ToList();

            var allHours = new List<Hour24Model>();

            foreach (var hour in hours)
            {
                var allPossibleMinutes = GetUniqueCombinations(digits, new List<TimeNoModel> { hour.hour })
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
                var allPossibleMinutes = GetUniqueCombinations(digits, new List<TimeNoModel> { hour.hour, hour.minutes })
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
            var proc = new Solution(processor);

            var hour = proc.solution(1, 8, 3, 2, 6, 4);
            

            Console.WriteLine(hour);

            Console.ReadKey();
        }
    }
}
