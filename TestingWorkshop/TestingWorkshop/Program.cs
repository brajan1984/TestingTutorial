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
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new HourProcessor();
            var generator = new TwoDigitsUniqueNumberGenerator();
            var hourGenerator = new HourPartialsGenerator(generator);
            var fullHourGenerator = new FullHoursGenerator(hourGenerator);

            var proc = new Solution(processor, fullHourGenerator);

            var hour = proc.Execute(1, 8, 3, 2, 6, 4);
            

            Console.WriteLine(hour);

            Console.ReadKey();
        }
    }
}
