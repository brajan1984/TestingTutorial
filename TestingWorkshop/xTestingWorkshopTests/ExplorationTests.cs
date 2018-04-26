﻿using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingWorkshop;
using TestingWorkshop.Extensions;
using TestingWorkshop.Models;
using TestingWorkshop.Services;
using Xunit;

namespace xTestingWorkshopTests
{
    public class ExplorationTests
    {
        Mock<IHoursProcessor> _processorMock = new Mock<IHoursProcessor>();
        Solution _solutionImpl = null;
        static readonly string[] _testSet1 = new string[] { "18:32:46", "18:36:24", "18:36:42", "18:34:26", "18:23:46", "18:26:34", "18:26:43", "18:24:36", "18:43:26", "18:42:36", "18:46:32", "18:46:23", "13:28:46", "13:26:48", "13:48:26", "13:46:28", "12:38:46", "12:36:48", "12:48:36", "12:46:38", "16:38:24", "16:38:42", "16:32:48", "16:34:28", "16:28:34", "16:28:43", "16:23:48", "16:24:38", "16:48:32", "16:48:23", "16:43:28", "16:42:38", "14:38:26", "14:36:28", "14:28:36", "14:26:38", "21:38:46", "21:36:48", "21:48:36", "21:46:38", "23:18:46", "23:16:48", "23:48:16", "23:46:18", "24:18:36", "24:16:38", "24:38:16", "24:36:18" };

        public ExplorationTests()
        {
            _solutionImpl = new Solution(_processorMock.Object);
        }

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

        [Theory]
        [InlineData(new int[] { 1, 8, 3, 2, 6, 4 }, new string[] { "18:34:26", "18:23:46", "18:32:46", "18:36:24", "18:36:42", "18:26:34", "18:26:43", "18:24:36", "18:43:26", "18:42:36", "18:46:32", "18:46:23", "13:28:46", "13:26:48", "13:48:26", "13:46:28", "12:38:46", "12:36:48", "12:48:36", "12:46:38", "16:38:24", "16:38:42", "16:32:48", "16:34:28", "16:28:34", "16:28:43", "16:23:48", "16:24:38", "16:48:32", "16:48:23", "16:43:28", "16:42:38", "14:38:26", "14:36:28", "14:28:36", "14:26:38", "21:38:46", "21:36:48", "21:48:36", "21:46:38", "23:18:46", "23:16:48", "23:48:16", "23:46:18", "24:18:36", "24:16:38", "24:38:16", "24:36:18" })]
        [InlineData(new int[] { 2, 6, 4, 1, 8, 3 }, new string[] { "18:34:26", "18:23:46", "18:32:46", "18:36:24", "18:36:42", "18:26:34", "18:26:43", "18:24:36", "18:43:26", "18:42:36", "18:46:32", "18:46:23", "13:28:46", "13:26:48", "13:48:26", "13:46:28", "12:38:46", "12:36:48", "12:48:36", "12:46:38", "16:38:24", "16:38:42", "16:32:48", "16:34:28", "16:28:34", "16:28:43", "16:23:48", "16:24:38", "16:48:32", "16:48:23", "16:43:28", "16:42:38", "14:38:26", "14:36:28", "14:28:36", "14:26:38", "21:38:46", "21:36:48", "21:48:36", "21:46:38", "23:18:46", "23:16:48", "23:48:16", "23:46:18", "24:18:36", "24:16:38", "24:38:16", "24:36:18" })]
        [InlineData(new int[] { 1, 9, 3, 5, 6, 8 }, new string[] { "19:36:58", "19:38:56", "19:56:38", "19:58:36", "16:39:58", "16:38:59", "16:59:38", "16:58:39", "18:39:56", "18:36:59", "18:59:36", "18:56:39" })]
        [InlineData(new int[] { 4, 4, 4, 4, 4, 4 }, new string[] { })]
        [InlineData(new int[] { 4, 4, 3, 9, 6, 2 }, new string[] { "24:49:36", "24:46:39", "24:39:46", "24:36:49", "24:49:36", "24:46:39", "24:39:46", "24:36:49", "23:49:46", "23:46:49", "23:49:46", "23:46:49" })]
        [InlineData(new int[] { 0, 2, 1, 3, 1, 2 }, new string[] { "02:13:12", "02:13:21", "02:11:32", "02:11:23", "02:12:31", "02:12:13", "02:31:12", "02:31:21", "02:31:12", "02:31:21", "02:32:11", "02:32:11", "02:13:12", "02:13:21", "02:11:32", "02:11:23", "02:12:31", "02:12:13", "02:21:31", "02:21:13", "02:23:11", "02:23:11", "02:21:31", "02:21:13", "01:23:12", "01:23:21", "01:21:32", "01:21:23", "01:22:31", "01:22:13", "01:32:12", "01:32:21", "01:31:22", "01:31:22", "01:32:12", "01:32:21", "01:12:32", "01:12:23", "01:13:22", "01:13:22", "01:12:32", "01:12:23", "01:23:12", "01:23:21", "01:21:32", "01:21:23", "01:22:31", "01:22:13", "03:21:12", "03:21:21", "03:21:12", "03:21:21", "03:22:11", "03:22:11", "03:12:12", "03:12:21", "03:11:22", "03:11:22", "03:12:12", "03:12:21", "03:12:12", "03:12:21", "03:11:22", "03:11:22", "03:12:12", "03:12:21", "03:21:12", "03:21:21", "03:21:12", "03:21:21", "03:22:11", "03:22:11", "01:23:12", "01:23:21", "01:21:32", "01:21:23", "01:22:31", "01:22:13", "01:32:12", "01:32:21", "01:31:22", "01:31:22", "01:32:12", "01:32:21", "01:12:32", "01:12:23", "01:13:22", "01:13:22", "01:12:32", "01:12:23", "01:23:12", "01:23:21", "01:21:32", "01:21:23", "01:22:31", "01:22:13", "02:13:12", "02:13:21", "02:11:32", "02:11:23", "02:12:31", "02:12:13", "02:31:12", "02:31:21", "02:31:12", "02:31:21", "02:32:11", "02:32:11", "02:13:12", "02:13:21", "02:11:32", "02:11:23", "02:12:31", "02:12:13", "02:21:31", "02:21:13", "02:23:11", "02:23:11", "02:21:31", "02:21:13", "20:13:12", "20:13:21", "20:11:32", "20:11:23", "20:12:31", "20:12:13", "20:31:12", "20:31:21", "20:31:12", "20:31:21", "20:32:11", "20:32:11", "20:13:12", "20:13:21", "20:11:32", "20:11:23", "20:12:31", "20:12:13", "20:21:31", "20:21:13", "20:23:11", "20:23:11", "20:21:31", "20:21:13", "21:03:12", "21:03:21", "21:01:32", "21:01:23", "21:02:31", "21:02:13", "21:30:12", "21:30:21", "21:31:02", "21:31:20", "21:32:01", "21:32:10", "21:10:32", "21:10:23", "21:13:02", "21:13:20", "21:12:03", "21:12:30", "21:20:31", "21:20:13", "21:23:01", "21:23:10", "21:21:03", "21:21:30", "23:01:12", "23:01:21", "23:01:12", "23:01:21", "23:02:11", "23:02:11", "23:10:12", "23:10:21", "23:11:02", "23:11:20", "23:12:01", "23:12:10", "23:10:12", "23:10:21", "23:11:02", "23:11:20", "23:12:01", "23:12:10", "23:20:11", "23:20:11", "23:21:01", "23:21:10", "23:21:01", "23:21:10", "21:03:12", "21:03:21", "21:01:32", "21:01:23", "21:02:31", "21:02:13", "21:30:12", "21:30:21", "21:31:02", "21:31:20", "21:32:01", "21:32:10", "21:10:32", "21:10:23", "21:13:02", "21:13:20", "21:12:03", "21:12:30", "21:20:31", "21:20:13", "21:23:01", "21:23:10", "21:21:03", "21:21:30", "22:01:31", "22:01:13", "22:03:11", "22:03:11", "22:01:31", "22:01:13", "22:10:31", "22:10:13", "22:13:01", "22:13:10", "22:11:03", "22:11:30", "22:30:11", "22:30:11", "22:31:01", "22:31:10", "22:31:01", "22:31:10", "22:10:31", "22:10:13", "22:13:01", "22:13:10", "22:11:03", "22:11:30", "10:23:12", "10:23:21", "10:21:32", "10:21:23", "10:22:31", "10:22:13", "10:32:12", "10:32:21", "10:31:22", "10:31:22", "10:32:12", "10:32:21", "10:12:32", "10:12:23", "10:13:22", "10:13:22", "10:12:32", "10:12:23", "10:23:12", "10:23:21", "10:21:32", "10:21:23", "10:22:31", "10:22:13", "12:03:12", "12:03:21", "12:01:32", "12:01:23", "12:02:31", "12:02:13", "12:30:12", "12:30:21", "12:31:02", "12:31:20", "12:32:01", "12:32:10", "12:10:32", "12:10:23", "12:13:02", "12:13:20", "12:12:03", "12:12:30", "12:20:31", "12:20:13", "12:23:01", "12:23:10", "12:21:03", "12:21:30", "13:02:12", "13:02:21", "13:01:22", "13:01:22", "13:02:12", "13:02:21", "13:20:12", "13:20:21", "13:21:02", "13:21:20", "13:22:01", "13:22:10", "13:10:22", "13:10:22", "13:12:02", "13:12:20", "13:12:02", "13:12:20", "13:20:12", "13:20:21", "13:21:02", "13:21:20", "13:22:01", "13:22:10", "11:02:32", "11:02:23", "11:03:22", "11:03:22", "11:02:32", "11:02:23", "11:20:32", "11:20:23", "11:23:02", "11:23:20", "11:22:03", "11:22:30", "11:30:22", "11:30:22", "11:32:02", "11:32:20", "11:32:02", "11:32:20", "11:20:32", "11:20:23", "11:23:02", "11:23:20", "11:22:03", "11:22:30", "12:03:12", "12:03:21", "12:01:32", "12:01:23", "12:02:31", "12:02:13", "12:30:12", "12:30:21", "12:31:02", "12:31:20", "12:32:01", "12:32:10", "12:10:32", "12:10:23", "12:13:02", "12:13:20", "12:12:03", "12:12:30", "12:20:31", "12:20:13", "12:23:01", "12:23:10", "12:21:03", "12:21:30", "10:23:12", "10:23:21", "10:21:32", "10:21:23", "10:22:31", "10:22:13", "10:32:12", "10:32:21", "10:31:22", "10:31:22", "10:32:12", "10:32:21", "10:12:32", "10:12:23", "10:13:22", "10:13:22", "10:12:32", "10:12:23", "10:23:12", "10:23:21", "10:21:32", "10:21:23", "10:22:31", "10:22:13", "12:03:12", "12:03:21", "12:01:32", "12:01:23", "12:02:31", "12:02:13", "12:30:12", "12:30:21", "12:31:02", "12:31:20", "12:32:01", "12:32:10", "12:10:32", "12:10:23", "12:13:02", "12:13:20", "12:12:03", "12:12:30", "12:20:31", "12:20:13", "12:23:01", "12:23:10", "12:21:03", "12:21:30", "13:02:12", "13:02:21", "13:01:22", "13:01:22", "13:02:12", "13:02:21", "13:20:12", "13:20:21", "13:21:02", "13:21:20", "13:22:01", "13:22:10", "13:10:22", "13:10:22", "13:12:02", "13:12:20", "13:12:02", "13:12:20", "13:20:12", "13:20:21", "13:21:02", "13:21:20", "13:22:01", "13:22:10", "11:02:32", "11:02:23", "11:03:22", "11:03:22", "11:02:32", "11:02:23", "11:20:32", "11:20:23", "11:23:02", "11:23:20", "11:22:03", "11:22:30", "11:30:22", "11:30:22", "11:32:02", "11:32:20", "11:32:02", "11:32:20", "11:20:32", "11:20:23", "11:23:02", "11:23:20", "11:22:03", "11:22:30", "12:03:12", "12:03:21", "12:01:32", "12:01:23", "12:02:31", "12:02:13", "12:30:12", "12:30:21", "12:31:02", "12:31:20", "12:32:01", "12:32:10", "12:10:32", "12:10:23", "12:13:02", "12:13:20", "12:12:03", "12:12:30", "12:20:31", "12:20:13", "12:23:01", "12:23:10", "12:21:03", "12:21:30", "20:13:12", "20:13:21", "20:11:32", "20:11:23", "20:12:31", "20:12:13", "20:31:12", "20:31:21", "20:31:12", "20:31:21", "20:32:11", "20:32:11", "20:13:12", "20:13:21", "20:11:32", "20:11:23", "20:12:31", "20:12:13", "20:21:31", "20:21:13", "20:23:11", "20:23:11", "20:21:31", "20:21:13", "21:03:12", "21:03:21", "21:01:32", "21:01:23", "21:02:31", "21:02:13", "21:30:12", "21:30:21", "21:31:02", "21:31:20", "21:32:01", "21:32:10", "21:10:32", "21:10:23", "21:13:02", "21:13:20", "21:12:03", "21:12:30", "21:20:31", "21:20:13", "21:23:01", "21:23:10", "21:21:03", "21:21:30", "23:01:12", "23:01:21", "23:01:12", "23:01:21", "23:02:11", "23:02:11", "23:10:12", "23:10:21", "23:11:02", "23:11:20", "23:12:01", "23:12:10", "23:10:12", "23:10:21", "23:11:02", "23:11:20", "23:12:01", "23:12:10", "23:20:11", "23:20:11", "23:21:01", "23:21:10", "23:21:01", "23:21:10", "21:03:12", "21:03:21", "21:01:32", "21:01:23", "21:02:31", "21:02:13", "21:30:12", "21:30:21", "21:31:02", "21:31:20", "21:32:01", "21:32:10", "21:10:32", "21:10:23", "21:13:02", "21:13:20", "21:12:03", "21:12:30", "21:20:31", "21:20:13", "21:23:01", "21:23:10", "21:21:03", "21:21:30", "22:01:31", "22:01:13", "22:03:11", "22:03:11", "22:01:31", "22:01:13", "22:10:31", "22:10:13", "22:13:01", "22:13:10", "22:11:03", "22:11:30", "22:30:11", "22:30:11", "22:31:01", "22:31:10", "22:31:01", "22:31:10", "22:10:31", "22:10:13", "22:13:01", "22:13:10", "22:11:03", "22:11:30" })]
        [InlineData(new int[] { 1, 8, 0, 2, 0, 4 }, new string[] { "18:02:04", "18:02:40", "18:00:24", "18:00:42", "18:04:20", "18:04:02", "18:20:04", "18:20:40", "18:20:04", "18:20:40", "18:24:00", "18:24:00", "18:02:04", "18:02:40", "18:00:24", "18:00:42", "18:04:20", "18:04:02", "18:40:20", "18:40:02", "18:42:00", "18:42:00", "18:40:20", "18:40:02", "10:28:04", "10:28:40", "10:20:48", "10:24:08", "10:08:24", "10:08:42", "10:02:48", "10:04:28", "10:48:20", "10:48:02", "10:42:08", "10:40:28", "12:08:04", "12:08:40", "12:00:48", "12:04:08", "12:08:04", "12:08:40", "12:00:48", "12:04:08", "12:48:00", "12:48:00", "12:40:08", "12:40:08", "10:28:04", "10:28:40", "10:20:48", "10:24:08", "10:08:24", "10:08:42", "10:02:48", "10:04:28", "10:48:20", "10:48:02", "10:42:08", "10:40:28", "14:08:20", "14:08:02", "14:02:08", "14:00:28", "14:28:00", "14:28:00", "14:20:08", "14:20:08", "14:08:20", "14:08:02", "14:02:08", "14:00:28", "01:28:04", "01:28:40", "01:20:48", "01:24:08", "01:08:24", "01:08:42", "01:02:48", "01:04:28", "01:48:20", "01:48:02", "01:42:08", "01:40:28", "08:12:04", "08:12:40", "08:10:24", "08:10:42", "08:14:20", "08:14:02", "08:21:04", "08:21:40", "08:20:14", "08:20:41", "08:24:10", "08:24:01", "08:01:24", "08:01:42", "08:02:14", "08:02:41", "08:04:12", "08:04:21", "08:41:20", "08:41:02", "08:42:10", "08:42:01", "08:40:12", "08:40:21", "02:18:04", "02:18:40", "02:10:48", "02:14:08", "02:01:48", "02:08:14", "02:08:41", "02:04:18", "02:41:08", "02:48:10", "02:48:01", "02:40:18", "00:18:24", "00:18:42", "00:12:48", "00:14:28", "00:21:48", "00:28:14", "00:28:41", "00:24:18", "00:41:28", "00:48:12", "00:48:21", "00:42:18", "04:18:20", "04:18:02", "04:12:08", "04:10:28", "04:21:08", "04:28:10", "04:28:01", "04:20:18", "04:01:28", "04:08:12", "04:08:21", "04:02:18", "21:08:04", "21:08:40", "21:00:48", "21:04:08", "21:08:04", "21:08:40", "21:00:48", "21:04:08", "21:48:00", "21:48:00", "21:40:08", "21:40:08", "20:18:04", "20:18:40", "20:10:48", "20:14:08", "20:01:48", "20:08:14", "20:08:41", "20:04:18", "20:41:08", "20:48:10", "20:48:01", "20:40:18", "20:18:04", "20:18:40", "20:10:48", "20:14:08", "20:01:48", "20:08:14", "20:08:41", "20:04:18", "20:41:08", "20:48:10", "20:48:01", "20:40:18", "24:18:00", "24:18:00", "24:10:08", "24:10:08", "24:01:08", "24:08:10", "24:08:01", "24:00:18", "24:01:08", "24:08:10", "24:08:01", "24:00:18", "01:28:04", "01:28:40", "01:20:48", "01:24:08", "01:08:24", "01:08:42", "01:02:48", "01:04:28", "01:48:20", "01:48:02", "01:42:08", "01:40:28", "08:12:04", "08:12:40", "08:10:24", "08:10:42", "08:14:20", "08:14:02", "08:21:04", "08:21:40", "08:20:14", "08:20:41", "08:24:10", "08:24:01", "08:01:24", "08:01:42", "08:02:14", "08:02:41", "08:04:12", "08:04:21", "08:41:20", "08:41:02", "08:42:10", "08:42:01", "08:40:12", "08:40:21", "02:18:04", "02:18:40", "02:10:48", "02:14:08", "02:01:48", "02:08:14", "02:08:41", "02:04:18", "02:41:08", "02:48:10", "02:48:01", "02:40:18", "00:18:24", "00:18:42", "00:12:48", "00:14:28", "00:21:48", "00:28:14", "00:28:41", "00:24:18", "00:41:28", "00:48:12", "00:48:21", "00:42:18", "04:18:20", "04:18:02", "04:12:08", "04:10:28", "04:21:08", "04:28:10", "04:28:01", "04:20:18", "04:01:28", "04:08:12", "04:08:21", "04:02:18" })]
        public void GetAllPossibleHours_GetsAStandardSet_ReturnsAllPossibleHours(int[] digits, string[] allCombinations)
        {
            var result = _solutionImpl.GetAllPossibleHours(digits.ToList());

            result.Select(h => h.To24HourFormatString())
                .Should()
                .BeEquivalentTo(allCombinations.ToList());
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, new int[] { 12, 21 })]
        [InlineData(new int[] { 4, 4 }, new int[] { 44, 44 })]
        [InlineData(new int[] { 5, 6, 7 }, new int[] { 56, 57, 65, 67, 75, 76 })]
        [InlineData(new int[] { 1, 8, 3, 2, 6, 4 }, new int[] { 18, 13, 12, 16, 14, 81, 83, 82, 86, 84, 31, 38, 32, 36, 34, 21, 28, 23, 26, 24, 61, 68, 63, 62, 64, 41, 48, 43, 42, 46 })]
        public void GenerateNumbers_GiveSomePossibleDigits_CorrectCombinationsReturned(int[] digits, int[] combinationsReturned)
        {
            var combinationsGenerated = _solutionImpl.GenerateNumbers(digits.ToList());

            var testResult = combinationsGenerated.Select(v => v.fullNo).ToList();

            testResult
                .Should()
                .BeEquivalentTo(combinationsReturned.ToList());
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        public void GenerateNumbers_GiveDigit_ShouldThrowException(int[] digits)
        {
            Action testAction = () => _solutionImpl.GenerateNumbers(digits.ToList());

            testAction
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("There should be two or more digits");
        }

    }
}
