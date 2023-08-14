using ElevatorChallenge.BusinessLogic.Services;
using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using ElevatorChallenge.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorChallenge.Tests.BusinessLogic.Services
{
    [TestClass]
    public class ElevatorStatusServiceTests
    {
        private Mock<IBuildingService> _mockBuildingService;
        private ElevatorStatusService _statusService;

        [TestInitialize]
        public void SetUp()
        {
            _mockBuildingService = new Mock<IBuildingService>();
            _statusService = new ElevatorStatusService(_mockBuildingService.Object);
        }

        [TestMethod]
        public void GetElevatorStatuses_SingleElevator_ShouldReturnCorrectStatus()
        {
            // Arrange
            var elevator = new Elevator(1)
            {
                CurrentFloor = 5,
                Direction = ElevatorDirection.Up,
                CurrentPeopleCount = 3
            };

            _mockBuildingService.Setup(s => s.GetElevators()).Returns(new List<Elevator> { elevator });

            // Act
            var statuses = _statusService.GetElevatorStatuses().ToList();

            // Assert
            Assert.AreEqual(1, statuses.Count);
            Assert.AreEqual("Elevator 1 - Floor: 5 - Direction: Up - Passengers: 3", statuses[0]);
        }

        [TestMethod]
        public void GetElevatorStatuses_MultipleElevators_ShouldReturnCorrectStatuses()
        {
            // Arrange
            var elevators = new List<Elevator>
            {
                new Elevator(1) { CurrentFloor = 2, Direction = ElevatorDirection.Up, CurrentPeopleCount = 2 },
                new Elevator(2) { CurrentFloor = 7, Direction = ElevatorDirection.Down, CurrentPeopleCount = 5 }
            };
            _mockBuildingService.Setup(s => s.GetElevators()).Returns(elevators);

            // Act
            var statuses = _statusService.GetElevatorStatuses().ToList();

            // Assert
            Assert.AreEqual(2, statuses.Count);
            Assert.AreEqual("Elevator 1 - Floor: 2 - Direction: Up - Passengers: 2", statuses[0]);
            Assert.AreEqual("Elevator 2 - Floor: 7 - Direction: Down - Passengers: 5", statuses[1]);
        }
    }
}
