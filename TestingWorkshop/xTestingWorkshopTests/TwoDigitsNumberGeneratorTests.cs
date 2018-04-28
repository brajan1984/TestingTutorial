using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop.Services;
using Xunit;

namespace xTestingWorkshopTests
{
    public class TwoDigitsNumberGeneratorTests
    {
        TwoDigitsUniqueNumberGenerator _generatorImpl = new TwoDigitsUniqueNumberGenerator();

        [Theory]
        [InlineData(new int[] { 1, 2 }, new int[] { 12, 21 })]
        [InlineData(new int[] { 4, 4 }, new int[] { 44, 44 })]
        [InlineData(new int[] { 5, 6, 7 }, new int[] { 56, 57, 65, 67, 75, 76 })]
        [InlineData(new int[] { 1, 8, 3, 2, 6, 4 }, new int[] { 18, 13, 12, 16, 14, 81, 83, 82, 86, 84, 31, 38, 32, 36, 34, 21, 28, 23, 26, 24, 61, 68, 63, 62, 64, 41, 48, 43, 42, 46 })]
        public void GenerateUniqueNumbers_GiveSomePossibleDigits_CorrectCombinationsReturned(int[] digits, int[] combinationsReturned)
        {
            var testResult = _generatorImpl.GenerateUniqueNumbers(digits.ToList());

            testResult
                .Should()
                .BeEquivalentTo(combinationsReturned.ToList());
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        public void GenerateUniqueNumbers_GiveNotEnoughDigits_ShouldThrowException(int[] digits)
        {
            Action testAction = () => _generatorImpl.GenerateUniqueNumbers(digits.ToList());

            testAction
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("There should be two or more digits");
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 23 })]
        [InlineData(new int[] { 54, 66 })]
        public void GenerateUniqueNumbers_GiveNumber_ShouldThrowException(int[] digits)
        {
            Action testAction = () => _generatorImpl.GenerateUniqueNumbers(digits.ToList());

            testAction
                .Should()
                .Throw<ArgumentOutOfRangeException>();
        }
        
        [Theory]
        [InlineData(new int[] { 1, 8, 3, 2, 6, 4 }, new int[] { 2, 6 }, new int[] { 18, 13, 14, 81, 83, 84, 31, 38, 34, 41, 48, 43 })]
        [InlineData(new int[] { 1, 1, 2 }, new int[] { 1 }, new int[] { 12, 21 })]
        public void GenerateUniqueNumbersExcluding_FilterCollection_CorrectFilteredCollection(int[] inputData, int[] excludeData, int[] expectedResult)
        {
            var testResult = _generatorImpl.GenerateUniqueNumbersExcluding(inputData.ToList(), excludeData.ToList());

            testResult
                .Should()
                .BeEquivalentTo(expectedResult.ToList());
        }

        [Theory]
        [InlineData(new int[] { 0, 1 }, new int[] { 1 }, new int[] {  })]
        public void GenerateUniqueNumbersExcluding_FilterToSmallCollection_ThorwsException(int[] inputData, int[] excludeData, int[] expectedResult)
        {
            Action testAction = () => _generatorImpl.GenerateUniqueNumbersExcluding(inputData.ToList(), excludeData.ToList());

            testAction
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("There should be two or more digits");
        }
    }
}
