using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorkshop
{
    public class doubleDigit
    {
        public int first;
        public int second;

        public int fullNo
        {
            get
            {
                return int.Parse(string.Format("{0}{1}", first, second));
            }
        }
    }

    public class hour24
    {
        public doubleDigit hour;
        public doubleDigit minutes;

        public doubleDigit seconds;

        public override string ToString()
        {
            return new TimeSpan(hour.fullNo, minutes.fullNo, seconds == null ? 0 : seconds.fullNo).ToString();
        }
    }

    public class Solution
    {
        public string solution(int A, int B, int C, int D, int E, int F)
        {
            var digits = new List<int>();
            digits.Add(A);
            digits.Add(B);
            digits.Add(C);
            digits.Add(D);
            digits.Add(E);
            digits.Add(F);

            var correctHour = new List<int>();

            string result = "NOT POSSIBLE";

            var hours = new List<hour24>();

            for (int first = 0; first < 6; first++)
            {
                var testDigits = digits.ToList();

                int firstNo = testDigits[first];
                testDigits.Remove(firstNo);

                foreach (var secondNo in testDigits)
                {
                    var hour = new doubleDigit() { first = firstNo, second = secondNo };

                    if (validateHour(hour))
                    {
                        hours.Add(new hour24 { hour = hour });
                    }
                }
            }

            var hoursWithMinutes = new List<hour24>();

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

                        var minute = new doubleDigit() { first = firstNo, second = secondNo };
                        var hourcpy = new hour24
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

            var correctHours = new List<hour24>();

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

                        var seconds = new doubleDigit() { first = firstNo, second = secondNo };

                        if (validateMinSec(seconds))
                        {

                            var hourcpy = new hour24
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

            HttpClient client = new HttpClient();
            var jsonObject = JsonConvert.SerializeObject(correctHours.Select(h => h.ToString()).ToArray());
            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var response = client.PostAsync("http://localhost:5000/api/values/getmin", content).Result;

            Stream receiveStream = response.Content.ReadAsStreamAsync().Result;
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            result = readStream.ReadToEnd();

            return result;
        }

        static bool validateHour(doubleDigit hour)
        {
            return hour.fullNo >= 0 && hour.fullNo <= 24;
        }

        static bool validateMinSec(doubleDigit hour)
        {
            return hour.fullNo >= 0 && hour.fullNo <= 60;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var proc = new Solution();

            var hour = proc.solution(1, 8, 3, 2, 6, 4);

            Console.WriteLine(hour);

            Console.ReadKey();
        }
    }
}
