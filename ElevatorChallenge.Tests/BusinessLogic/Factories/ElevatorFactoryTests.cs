using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using ElevatorChallenge.Domain.Factories.ElevatorFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElevatorChallenge.Tests.BusinessLogic.Factories
{
    [TestClass]
    public class ElevatorFactoryTests
    {
        private ElevatorFactory _factory;
        private Mock<IConsolePresentationService> _mockConsoleService;

        [TestInitialize]
        public void SetUp()
        {
            _mockConsoleService = new Mock<IConsolePresentationService>();
            _factory = new ElevatorFactory(_mockConsoleService.Object);
        }

        [TestMethod]
        public void CreateElevator_ShouldReturnElevatorWithCorrectId()
        {
            // Act
            var elevator = _factory.CreateElevator(5);

            // Assert
            Assert.AreEqual(5, elevator.Id);
        }

        [TestMethod]
        public void CreateElevator_ShouldSetUpElevatorEvents()
        {
            // Act
            var elevator = _factory.CreateElevator(3);

            // Assert
            _mockConsoleService.Verify(service => service.HandleElevatorEvents(elevator), Times.Once);
        }
    }
}
