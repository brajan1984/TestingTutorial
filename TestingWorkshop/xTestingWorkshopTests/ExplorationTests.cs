using FluentAssertions;
using System;
using System.Linq;
using TestingWorkshop;
using Xunit;

namespace xTestingWorkshopTests
{
    public class ExplorationTests
    {
        Solution _solutionImpl = new Solution();

        [Theory]
        [InlineData(1, 8, 3, 2, 6, 4, "12:36:48")]
        [InlineData(4, 4, 4, 4, 4, 4, "NOT POSSIBLE")]
        [InlineData(1, 9, 3, 5, 6, 8, "16:38:59")]
        [InlineData(4, 4, 3, 9, 6, 2, "23:46:49")]
        [InlineData(0, 2, 1, 3, 1, 2, "01:12:23")]
        [InlineData(1, 8, 0, 2, 0, 4, "00:12:48")]
        public void SolutionExplorationTest(int A, int B, int C, int D, int E, int F, string expectedValue)
        {
            var result = _solutionImpl.solution(A, B, C, D, E, F);

            result.Should().Be(expectedValue);
        }
    }
}
