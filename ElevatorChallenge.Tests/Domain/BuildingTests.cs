using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;
using ElevatorChallenge.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ElevatorChallenge.Tests.Domain
{
    [TestClass]
    public class BuildingTests
    {
        private Building _building;
        private List<Elevator> _elevators;

        [TestInitialize]
        public void Setup()
        {
            _elevators = new List<Elevator>
            {
                new Elevator(1),
                new Elevator(2),
                new Elevator(3)
            };

            _building = new Building(10, _elevators);
        }

        [TestMethod]
        public void CallElevator_WhenElevatorFound_ShouldReturnElevator()
        {
            var mockStrategy = new Mock<IElevatorStrategy>();
            mockStrategy.Setup(s => s.ChooseElevator(_elevators, It.IsAny<int>(), It.IsAny<int>()))
                        .Returns(_elevators[0]);

            var elevator = _building.CallElevator(5, 2, mockStrategy.Object);

            Assert.IsNotNull(elevator);
            Assert.AreEqual(1, elevator.Id);
        }

        [TestMethod]
        public void CallElevator_WhenElevatorNotFound_ShouldRaiseEvent()
        {
            bool eventRaised = false;
            _building.OnElevatorNotFoundEvent += (message) => eventRaised = true;

            var mockStrategy = new Mock<IElevatorStrategy>();
            mockStrategy.Setup(s => s.ChooseElevator(_elevators, It.IsAny<int>(), It.IsAny<int>()))
                        .Returns((Elevator)null);

            _building.CallElevator(5, 2, mockStrategy.Object);

            Assert.IsTrue(eventRaised);
        }
    }
}
