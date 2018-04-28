using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Models;
using TestingWorkshop.Services;
using Xunit;

namespace xTestingWorkshopTests
{
    public class HourGeneratorTests
    {
        Mock<IUniqueNumberGenerator> _numberGeneratorMock = new Mock<IUniqueNumberGenerator>();
        HourGenerator _hourGeneratorImpl;

        public HourGeneratorTests()
        {
            _hourGeneratorImpl = new HourGenerator(_numberGeneratorMock.Object);
        }
        
        [Theory]
        [InlineData(new int[] { 1, 8, 3, 2 }, 18, new int[] { 1, 8 }, new int[] { 32, 23 }, new int[] { 32, 23 })]
        [InlineData(new int[] { 1, 8, 9, 2 }, 18, new int[] { 1, 8 }, new int[] { 92, 29 }, new int[] { 29 })]
        [InlineData(new int[] { 1, 8, 9, 9 }, 18, new int[] { 1, 8 }, new int[] { 99, 99 }, new int[] { })]
        public void FillAllHourPartials_GetsAStandardSetAndFillMinutes_ReturnsHoursWithMinutes(int[] digits, int hour, int[] hourDigits, int[] allPossibleMinutes, int[] expectedResult)
        {
            var hourModel = new List<Hour24Model> { new Hour24Model { hour = GenerateTimeNoModel(hour) } };
            var expectedHourWithMinutes = expectedResult.Select(m => new Hour24Model { hour = GenerateTimeNoModel(hour), minutes = GenerateTimeNoModel(m) });

            _numberGeneratorMock
                .Setup(m => m.GenerateUniqueNumbersExcluding(digits, hourDigits))
                .Returns(allPossibleMinutes.ToList());

            var result = _hourGeneratorImpl.FillAllHourPartials(digits, hourModel, (m, v) => m.minutes = v);

            result
                .Should()
                .BeEquivalentTo(expectedHourWithMinutes);
        }

        [Theory]
        [InlineData(new int[] { 1, 8, 3, 2, 6, 4 }, 18, 32, new int[] { 1, 8, 3, 2 }, new int[] { 46 }, new int[] { 46 })]
        [InlineData(new int[] { 4, 4, 3, 9, 6, 2 }, 23, 46, new int[] { 2, 3, 4, 6 }, new int[] { 49, 94 }, new int[] { 49 })]
        public void FillAllHourPartials_GetsAStandardSetAndFillSeconds_ReturnsHoursWithMinutesAndSeconds(int[] digits, int hour, int minutes, int[] usedDigits, int[] allPossibleSeconds, int[] expectedResult)
        {
            var hourModel = new List<Hour24Model> { new Hour24Model { hour = GenerateTimeNoModel(hour), minutes = GenerateTimeNoModel(minutes) } };
            var expectedHour = expectedResult.Select(s => new Hour24Model { hour = GenerateTimeNoModel(hour), minutes = GenerateTimeNoModel(minutes), seconds = GenerateTimeNoModel(s) });

            _numberGeneratorMock
                .Setup(m => m.GenerateUniqueNumbersExcluding(digits, usedDigits))
                .Returns(allPossibleSeconds.ToList());

            var result = _hourGeneratorImpl.FillAllHourPartials(digits, hourModel, (m, v) => m.seconds = v);

            result
                .Should()
                .BeEquivalentTo(expectedHour);
        }

        [Theory]
        [InlineData(new int[] { 1, 8, 3, 2, 6, 4 }, new int[] { 18, 13, 12, 16, 14, 21, 23, 24, 81, 83, 82, 86, 84, 31 }, new int[] { 18, 13, 12, 16, 14, 21, 23, 24 })]
        public void FillAllHours_HoursAreFilteredByProperRange_CorrectHoursCollection(int[] inputData, int[] allCombinations, int[] expectedResultData)
        {
            var allCombinationsCollection = allCombinations.ToList();
            var inputDataCollection = inputData.ToList();

            _numberGeneratorMock
                .Setup(g => g.GenerateUniqueNumbers(inputDataCollection))
                .Returns(allCombinationsCollection);

            var hours = _hourGeneratorImpl.FillAllHours(inputDataCollection);

            var expectedResult = new List<Hour24Model>();

            for (int i = 0; i < expectedResultData.Length; i++)
            {
                expectedResult.Add(new Hour24Model { hour = GenerateTimeNoModel(expectedResultData[i]) });
            }

            var testResult = _hourGeneratorImpl.FillAllHours(inputData.ToList());

            testResult.Should().BeEquivalentTo(expectedResult);

            _numberGeneratorMock.Verify(m => m.GenerateUniqueNumbers(It.Is<List<int>>(p => p == inputDataCollection)), Times.Once);
        }

        private TimeNoModel GenerateTimeNoModel(int no)
        {
            var first = no / 10;
            return new TimeNoModel { first = first, second = no - first * 10 };
        }
    }
}
