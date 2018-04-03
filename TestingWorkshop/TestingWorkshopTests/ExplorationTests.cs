using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingWorkshop;

namespace TestingWorkshopTests
{
    public class ExplorationTests
    {
        Solution _solutionImpl = new Solution();

        [Theory]
        [TestCase(1, 8, 3, 2, 6, 4, "12:36:48")]
        [TestCase(4, 4, 4, 4, 4, 4, "NOT POSSIBLE")]
        [TestCase(1, 9, 3, 5, 6, 8, "16:38:59")]
        [TestCase(4, 4, 3, 9, 6, 2, "23:46:49")]
        [TestCase(0, 2, 1, 3, 1, 2, "01:12:23")]
        [TestCase(1, 8, 0, 2, 0, 4, "00:12:48")]
        public void SolutionExplorationTest(int A, int B, int C, int D, int E, int F, string expectedValue)
        {
            var result = _solutionImpl.solution(A, B, C, D, E, F);

            result.Should().Be(expectedValue);
        }
    }
}
