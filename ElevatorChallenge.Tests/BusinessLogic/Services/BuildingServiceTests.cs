using ElevatorChallenge.BusinessLogic.Services;
using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;
using ElevatorChallenge.Domain.Factories.ElevatorFactory;
using ElevatorChallenge.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorChallenge.Tests.BusinessLogic.Services
{
    [TestClass]
    public class BuildingServiceTests
    {
        private IBuildingService _service;
        private Mock<IElevatorFactory> _mockFactory;

        [TestInitialize]
        public void Setup()
        {
            _mockFactory = new Mock<IElevatorFactory>();
            _service = new BuildingService();
        }

        [TestMethod]
        public void Initialize_ShouldInitializeBuildingInstanceWithCorrectNumberOfFloorsAndElevators()
        {
            // Arrange
            int totalFloors = 5;
            int totalElevators = 3;
            _mockFactory.Setup(f => f.CreateElevator(It.IsAny<int>())).Returns(new Elevator(It.IsAny<int>()));

            // Act
            _service.Initialize(totalFloors, totalElevators, _mockFactory.Object);

            // Assert
            Assert.AreEqual(totalFloors, _service.BuildingInstance.TotalFloors);
            Assert.AreEqual(totalElevators, _service.BuildingInstance.Elevators.Count());
        }

        [TestMethod]
        public void CallElevator_ShouldCallElevatorUsingProvidedStrategy()
        {
            // Arrange
            int totalFloors = 5;
            int totalElevators = 3;
            int calledFloor = 3;
            int passengers = 2;
            var mockStrategy = new Mock<IElevatorStrategy>();

            _mockFactory.Setup(f => f.CreateElevator(It.IsAny<int>())).Returns(new Elevator(It.IsAny<int>()));

            _service.Initialize(totalFloors, totalElevators, _mockFactory.Object);
            mockStrategy.Setup(s => s.ChooseElevator(It.IsAny<List<Elevator>>(), calledFloor, passengers)).Returns(_service.BuildingInstance.Elevators.First());

            // Act
            var elevator = _service.CallElevator(calledFloor, passengers, mockStrategy.Object);

            // Assert
            mockStrategy.Verify(s => s.ChooseElevator(It.IsAny<List<Elevator>>(), calledFloor, passengers), Times.Once);
            Assert.IsNotNull(elevator);
        }

        [TestMethod]
        public void When_NoElevatorIsFound_NoElevatorFoundMessageShouldBeTriggered()
        {
            // Arrange
            bool eventWasRaised = false;
            _service.OnElevatorNotFound += (message) => eventWasRaised = true;

            var mockStrategy = new Mock<IElevatorStrategy>();
            mockStrategy.Setup(s => s.ChooseElevator(It.IsAny<List<Elevator>>(), It.IsAny<int>(), It.IsAny<int>())).Returns((Elevator)null);

            _mockFactory.Setup(f => f.CreateElevator(It.IsAny<int>())).Returns(new Elevator(It.IsAny<int>()));

            _service.Initialize(5, 3, _mockFactory.Object);

            // Act
            _service.CallElevator(1, 1, mockStrategy.Object);

            // Assert
            Assert.IsTrue(eventWasRaised);
        }
    }
}
