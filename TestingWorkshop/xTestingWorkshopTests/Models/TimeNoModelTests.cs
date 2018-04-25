using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Extensions;
using TestingWorkshop.Models;
using Xunit;

namespace xTestingWorkshopTests.Models
{
    public class TimeNoModelTests
    {
        [Theory]
        [InlineData("0", 0, 0)]
        [InlineData("1", 0, 1)]
        [InlineData("2", 0, 2)]
        [InlineData("3", 0, 3)]
        [InlineData("4", 0, 4)]
        [InlineData("5", 0, 5)]
        [InlineData("6", 0, 6)]
        [InlineData("7", 0, 7)]
        [InlineData("8", 0, 8)]
        [InlineData("9", 0, 9)]
        [InlineData("08", 0, 8)]
        [InlineData("10", 1, 0)]
        [InlineData("24", 2, 4)]
        [InlineData("99", 9, 9)]
        public void ParseToTimeNoModel_ParseDigit_Succesfull(string input, int first, int second)
        {
            var result = input.ParseToTimeNoModel();

            result.first.Should().Be(first);
            result.second.Should().Be(second);
        }
    }
}
