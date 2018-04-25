using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Models;

namespace TestingWorkshop.Extensions
{
    public static class Hour24ModelExtensions
    {
        public static Hour24Model ParseToHour24Model(this string input)
        {
            string[] splitted = input.Split(':');

            return new Hour24Model
            {
                hour = splitted[0].ParseToTimeNoModel(),
                minutes = splitted[1].ParseToTimeNoModel(),
                seconds = splitted[2].ParseToTimeNoModel(),
            };
        }

        public static string To24HourFormatString(this Hour24Model model)
        {
            return $"{model.hour.ToDoubleDigitString()}:{model.minutes.ToDoubleDigitString()}:{model.seconds.ToDoubleDigitString()}";
        }
    }

    public static class TimeNoModelExtensions
    {
        public static TimeNoModel ParseToTimeNoModel(this string no)
        {
            var model = new TimeNoModel();

            var number = int.Parse(no);

            model.first = number / 10;
            model.second = number - model.first * 10;

            return model;
        }

        public static string ToDoubleDigitString(this TimeNoModel model)
        {
            return model.first.ToString() + model.second.ToString();
        }
    }
}
