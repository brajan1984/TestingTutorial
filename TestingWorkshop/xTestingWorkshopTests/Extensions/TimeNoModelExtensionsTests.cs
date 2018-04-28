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
    public class TimeNoModelExtensionsTests
    {
        [Theory]
        [InlineData("06", new int[] { 0, 6 })]
        [InlineData("00", new int[] { 0, 0 })]
        [InlineData("12", new int[] { 1, 2 })]
        [InlineData("23", new int[] { 2, 3 })]
        public void ToDoubleDigitString_ParseHoursToStringFormat_Success(string expectedResult, int[] testNoData)
        {
            var testNo = new TimeNoModel { first = testNoData[0], second = testNoData[1] };

            var testResult = testNo.ToDoubleDigitString();

            testResult.Should().Be(expectedResult);
        }
    }
}
