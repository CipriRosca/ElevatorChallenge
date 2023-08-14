using ElevatorChallenge.BusinessLogic.Strategies.Concrete;
using ElevatorChallenge.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ElevatorChallenge.Tests.BusinessLogic.Strategies
{
    [TestClass]
    public class ProximityStrategyTests
    {
        private ProximityStrategy _strategy;

        [TestInitialize]
        public void SetUp()
        {
            _strategy = new ProximityStrategy();
        }

        [TestMethod]
        public void ChooseElevator_ShouldSelectClosestElevator()
        {
            // Arrange
            var elevator1 = new Elevator(1) { CurrentFloor = 3 };
            var elevator2 = new Elevator(2) { CurrentFloor = 7 };
            var elevator3 = new Elevator(3) { CurrentFloor = 10 };
            var elevators = new List<Elevator> { elevator1, elevator2, elevator3 };

            // Act
            var chosenElevator = _strategy.ChooseElevator(elevators, 2, 1);

            // Assert
            Assert.AreEqual(elevator1, chosenElevator);
        }

        [TestMethod]
        public void ChooseElevator_WhenClosestElevatorFull_ShouldReturnNextClosestElevator()
        {
            // Arrange
            var elevator1 = new Elevator(1) { CurrentFloor = 3, CurrentPeopleCount = 10 };
            var elevator2 = new Elevator(2) { CurrentFloor = 2, CurrentPeopleCount = 10 };
            var elevator3 = new Elevator(3) { CurrentFloor = 5 };
            var elevators = new List<Elevator> { elevator1, elevator2, elevator3 };

            // Act
            var chosenElevator = _strategy.ChooseElevator(elevators, 2, 1);

            // Assert
            Assert.AreEqual(elevator3, chosenElevator);
        }

        [TestMethod]
        public void ChooseElevator_WhenAllElevatorsFull_ShouldReturnNull()
        {
            // Arrange
            var elevator1 = new Elevator(1) { CurrentFloor = 3, CurrentPeopleCount = 10 };
            var elevator2 = new Elevator(2) { CurrentFloor = 2, CurrentPeopleCount = 10 };
            var elevators = new List<Elevator> { elevator1, elevator2 };

            // Act
            var chosenElevator = _strategy.ChooseElevator(elevators, 2, 1);

            // Assert
            Assert.IsNull(chosenElevator);
        }
    }
}
