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

        private IEnumerable<TimeNoModel> GetAllHours(List<int> digits)
        {
            var allHours = new List<TimeNoModel>();

            for (int first = 0; first < 6; first++)
            {
                var testDigits = digits.ToList();

                int firstNo = testDigits[first];
                testDigits.Remove(firstNo);

                foreach (var secondNo in testDigits)
                {
                    var hour = new TimeNoModel() { first = firstNo, second = secondNo };

                    if (validateHour(hour))
                    {
                        allHours.Add(hour);
                    }
                }
            }

            return allHours;
        }

        private Dictionary<List<TimeNoModel>, List<TimeNoModel>> GetUniqueCombinations(List<int> allDigits, List<List<TimeNoModel>> allCurrentCombinations)
        {
            var result = new Dictionary<List<TimeNoModel>, List<TimeNoModel>>();
            
            foreach (var usedCombination in allCurrentCombinations)
            {
                var possibleDigits = allDigits.ToList();

                foreach (var numberInCombination in usedCombination)
                {
                    possibleDigits.Remove(numberInCombination.first);
                    possibleDigits.Remove(numberInCombination.second);

                    var combinations = GenerateNumbers(possibleDigits);
                }
            }

            return result;
        }

        public List<TimeNoModel> GenerateNumbers(List<int> digits)
        {
            if (digits.Count <= 1)
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

        /*private void GenerateHourPartials(List<int> digits, List<List<TimeNoModel>> allExcludedCombinations, Func<TimeNoModel, bool> validator)
        {
            foreach (var combination in allExcludedCombinations)
            {
                var testDigits = digits.ToList();
                testDigits.Remove(hour.hour.first);
                testDigits.Remove(hour.hour.second);
                testDigits.Remove(hour.minutes.first);
                testDigits.Remove(hour.minutes.second);

                for (int first = 0; first < testDigits.Count(); first++)
                {
                    var digitsForNext = testDigits.ToList();

                    int firstNo = digitsForNext[first];
                    digitsForNext.Remove(firstNo);

                    foreach (var secondNo in digitsForNext)
                    {

                        var seconds = new TimeNoModel() { first = firstNo, second = secondNo };

                        if (validateMinSec(seconds))
                        {

                            var hourcpy = new Hour24Model
                            {
                                hour = hour.hour,
                                minutes = hour.minutes,
                                seconds = hour.seconds
                            };
                            hourcpy.seconds = seconds;
                            correctHours.Add(hourcpy);
                        }
                    }
                }
            }
        }*/

        public List<Hour24Model> GetAllPossibleHours(List<int> digits)
        {
            var correctHour = new List<int>();

            string result = "NOT POSSIBLE";

            var hours = new List<Hour24Model>();

            for (int first = 0; first < 6; first++)
            {
                var testDigits = digits.ToList();

                int firstNo = testDigits[first];
                testDigits.Remove(firstNo);

                foreach (var secondNo in testDigits)
                {
                    var hour = new TimeNoModel() { first = firstNo, second = secondNo };

                    if (validateHour(hour))
                    {
                        hours.Add(new Hour24Model { hour = hour });
                    }
                }
            }

            var hoursWithMinutes = new List<Hour24Model>();

            foreach (var hour in hours)
            {
                var testDigits = digits.ToList();
                testDigits.Remove(hour.hour.first);
                testDigits.Remove(hour.hour.second);

                for (int first = 0; first < testDigits.Count(); first++)
                {
                    var digitsForNext = testDigits.ToList();

                    int firstNo = digitsForNext[first];
                    digitsForNext.Remove(firstNo);

                    foreach (var secondNo in digitsForNext)
                    {

                        var minute = new TimeNoModel() { first = firstNo, second = secondNo };
                        var hourcpy = new Hour24Model
                        {
                            hour = hour.hour,
                            minutes = hour.minutes,
                            seconds = hour.seconds
                        };
                        if (validateMinSec(minute))
                        {
                            hourcpy.minutes = minute;
                            hoursWithMinutes.Add(hourcpy);
                        }
                    }
                }
            }

            var correctHours = new List<Hour24Model>();

            foreach (var hour in hoursWithMinutes)
            {
                var testDigits = digits.ToList();
                testDigits.Remove(hour.hour.first);
                testDigits.Remove(hour.hour.second);
                testDigits.Remove(hour.minutes.first);
                testDigits.Remove(hour.minutes.second);

                for (int first = 0; first < testDigits.Count(); first++)
                {
                    var digitsForNext = testDigits.ToList();

                    int firstNo = digitsForNext[first];
                    digitsForNext.Remove(firstNo);

                    foreach (var secondNo in digitsForNext)
                    {

                        var seconds = new TimeNoModel() { first = firstNo, second = secondNo };

                        if (validateMinSec(seconds))
                        {

                            var hourcpy = new Hour24Model
                            {
                                hour = hour.hour,
                                minutes = hour.minutes,
                                seconds = hour.seconds
                            };
                            hourcpy.seconds = seconds;
                            correctHours.Add(hourcpy);
                        }
                    }
                }
            }

            return correctHours;
        }

        private void PrintHours(List<Hour24Model> correctHours)
        {
            var combinations = "new string[] {" + correctHours.Select(h => "\"" + h.To24HourFormatString() + "\"").Aggregate((c, n) => $"{c}, {n}") + "}";
            
            Console.Write(combinations);
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

            PrintHours(correctHours);

            string result = _processor.Process(correctHours);

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
