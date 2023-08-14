using ElevatorChallenge.BusinessLogic.Strategies.Concrete;
using ElevatorChallenge.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ElevatorChallenge.Tests.BusinessLogic.Strategies
{
    [TestClass]
    public class OccupancyStrategyTests
    {
        private OccupancyStrategy _strategy;

        [TestInitialize]
        public void SetUp()
        {
            _strategy = new OccupancyStrategy();
        }

        [TestMethod]
        public void ChooseElevator_ShouldSelectElevatorWithLeastOccupancy()
        {
            // Arrange
            var elevator1 = new Elevator(1) { CurrentPeopleCount = 3 };
            var elevator2 = new Elevator(2) { CurrentPeopleCount = 1 };
            var elevator3 = new Elevator(3) { CurrentPeopleCount = 5 };
            var elevators = new List<Elevator> { elevator1, elevator2, elevator3 };

            // Act
            var chosenElevator = _strategy.ChooseElevator(elevators, 1, 2);

            // Assert
            Assert.AreEqual(elevator2, chosenElevator);
        }

        [TestMethod]
        public void ChooseElevator_WhenAllElevatorsFull_ShouldReturnNull()
        {
            // Arrange
            var elevator1 = new Elevator(1) { CurrentPeopleCount = 10 };
            var elevator2 = new Elevator(2) { CurrentPeopleCount = 10 };
            var elevators = new List<Elevator> { elevator1, elevator2 };

            // Act
            var chosenElevator = _strategy.ChooseElevator(elevators, 1, 1);

            // Assert
            Assert.IsNull(chosenElevator);
        }

        [TestMethod]
        public void ChooseElevator_ShouldReturnElevatorWithEnoughSpace()
        {
            // Arrange
            var elevator1 = new Elevator(1) { CurrentPeopleCount = 5 };
            var elevator2 = new Elevator(2) { CurrentPeopleCount = 2 };
            var elevators = new List<Elevator> { elevator1, elevator2 };

            // Act
            var chosenElevator = _strategy.ChooseElevator(elevators, 1, 3);

            // Assert
            Assert.AreEqual(elevator2, chosenElevator);
        }
    }
}
