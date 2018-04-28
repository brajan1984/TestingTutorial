using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Extensions;
using TestingWorkshop.Models;
using Xunit;

namespace xTestingWorkshopTests.Extensions
{
    public class Hour24ModelExtensionsTests
    {
        [Theory]
        [InlineData("06:59:30", new int[] { 0, 6, 5, 9, 3, 0 })]
        [InlineData("00:00:00", new int[] { 0, 0, 0, 0, 0, 0 })]
        [InlineData("12:05:21", new int[] { 1, 2, 0, 5, 2, 1 })]
        [InlineData("23:59:16", new int[] { 2, 3, 5, 9, 1, 6 })]
        public void To24HourFormatString_ParseHoursToStringFormat_Success(string expectedResult, int[] test24HourData)
        {
            var testModel = new Hour24Model
            {
                hour = new TimeNoModel { first = test24HourData[0], second = test24HourData[1] },
                minutes = new TimeNoModel { first = test24HourData[2], second = test24HourData[3] },
                seconds = new TimeNoModel { first = test24HourData[4], second = test24HourData[5] }
            };

            var testResult = testModel.To24HourFormatString();

            testResult.Should().Be(expectedResult);
        }



        [Theory]
        [InlineData(new int[] { 1, 8, 3, 2, 4, 6, 2, 3, 2, 3, 4, 6 }, "new string[] {\"18:32:46\", \"23:23:46\"}")]
        public void ParseHoursCollection_PassingStandardHour_SuccessfullGeneratedArray(int[] inputData, string expectedResult)
        {
            var testModel1 = new Hour24Model
            {
                hour = new TimeNoModel { first = inputData[0], second = inputData[1] },
                minutes = new TimeNoModel { first = inputData[2], second = inputData[3] },
                seconds = new TimeNoModel { first = inputData[4], second = inputData[5] }
            };

            var testModel2 = new Hour24Model
            {
                hour = new TimeNoModel { first = inputData[6], second = inputData[7] },
                minutes = new TimeNoModel { first = inputData[8], second = inputData[9] },
                seconds = new TimeNoModel { first = inputData[10], second = inputData[11] }
            };

            var testResult = (new List<Hour24Model> { testModel1, testModel2 }).ParseHoursCollection();

            testResult.Should().Be(expectedResult);
        }
    }
}
