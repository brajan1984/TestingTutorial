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
        private readonly IHourGenerator _generator;

        public Solution(IHoursProcessor processor, IHourGenerator generator)
        {
            _processor = processor;
            _generator = generator;
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


        public List<Hour24Model> GetAllPossibleHours(List<int> digits)
        {
            var correctHour = new List<int>();

            var hours = _generator.FillAllHours(digits);

            var allHours = _generator.FillAllMinutes(digits, hours);

            var fullHours = _generator.FillAllSeconds(digits, allHours);

            return fullHours.ToList();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var processor = new HourProcessor();
            var generator = new TwoDigitsUniqueNumberGenerator();
            var hourGenerator = new HourGenerator(generator);

            var proc = new Solution(processor, hourGenerator);

            var hour = proc.solution(1, 8, 3, 2, 6, 4);
            

            Console.WriteLine(hour);

            Console.ReadKey();
        }
    }
}
