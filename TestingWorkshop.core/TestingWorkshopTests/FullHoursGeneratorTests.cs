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
    public class FullHoursGeneratorTests
    {
        private FullHoursGenerator _generatorImpl;
        private Mock<IHourPartialsGenerator> _partialsGenerator = new Mock<IHourPartialsGenerator>();

        public FullHoursGeneratorTests()
        {
            _generatorImpl = new FullHoursGenerator(_partialsGenerator.Object);
        }

        [Fact]
        public void GetAllPossibleHours_CallWithData_AllNecessaryMethodsCalled()
        {
            //Arrange
            var allDigits = new List<int>();
            var hours = new List<Hour24Model>();
            var hoursWMinutes = new List<Hour24Model>();
            var fullHours = new List<Hour24Model>();

            var myDigits = new List<int>();

            _partialsGenerator.Setup(h => h.FillAllHours(allDigits)).Returns(hours);
            _partialsGenerator.Setup(h => h.FillAllMinutes(allDigits, hours)).Returns(hoursWMinutes);
            _partialsGenerator.Setup(h => h.FillAllSeconds(allDigits, hoursWMinutes)).Returns(hoursWMinutes);

            //Act
            var result = _generatorImpl.GetAllPossibleHours(allDigits);

            //Assert
            _partialsGenerator.Verify(h => h.FillAllHours(It.Is<List<int>>(o => o == allDigits)), Times.Once);
            _partialsGenerator.Verify(h => h.FillAllMinutes(It.Is<List<int>>(o => o == allDigits), It.Is<List<Hour24Model>>(o => o == hours)), Times.Once);
            _partialsGenerator.Verify(h => h.FillAllSeconds(It.Is<List<int>>(o => o == allDigits), It.Is<List<Hour24Model>>(o => o == hoursWMinutes)), Times.Once);

            result.Should().Equal(fullHours);
        }
    }
}
