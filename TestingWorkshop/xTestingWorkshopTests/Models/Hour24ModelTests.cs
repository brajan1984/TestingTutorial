using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Extensions;
using TestingWorkshop.Models;
using xTestingWorkshopTests.Comparer;
using Xunit;

namespace xTestingWorkshopTests.Models
{
    public class Hour24ModelTests
    {
        [Theory]
        [InlineData("12:34:56", "12", "34", "56")]
        [InlineData("00:34:56", "00", "34", "56")]
        [InlineData("01:01:01", "01", "01", "01")]
        [InlineData("22:43:12", "22", "43", "12")]
        public void ParseToHour24Model_HoursParse_Successfull(string input, string hour, string minutes, string seconds)
        {
            var hourModel = hour.ParseToTimeNoModel();
            var minuteModel = minutes.ParseToTimeNoModel();
            var secondsModel = seconds.ParseToTimeNoModel();
            var comparer = new TimeNoModelComparer();

            var fullHourModel = input.ParseToHour24Model();

            comparer.Equals(hourModel, fullHourModel.hour).Should().BeTrue();
            comparer.Equals(minuteModel, fullHourModel.minutes).Should().BeTrue();
            comparer.Equals(secondsModel, fullHourModel.seconds).Should().BeTrue();
        }
    }
}
