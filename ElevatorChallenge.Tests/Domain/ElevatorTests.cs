using ElevatorChallenge.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevatorChallenge.Tests.Domain
{
    [TestClass]
    public class ElevatorTests
    {
        private Elevator _elevator;

        [TestInitialize]
        public void Setup()
        {
            _elevator = new Elevator(1);
        }

        [TestMethod]
        public void BoardPassengers_WithValidPassengers_ShouldIncreaseCount()
        {
            int initialCount = _elevator.CurrentPeopleCount;

            _elevator.BoardPassengers(5);

            Assert.AreEqual(initialCount + 5, _elevator.CurrentPeopleCount);
        }

        [TestMethod]
        public void BoardPassengers_WithInvalidPassengers_ShouldNotIncreaseCount()
        {
            int initialCount = _elevator.CurrentPeopleCount;
            _elevator.CurrentPeopleCount = 9;

            _elevator.BoardPassengers(5);

            Assert.AreEqual(initialCount + 9, _elevator.CurrentPeopleCount);
        }

        [TestMethod]
        public void CanBoardPassengers_WithinCapacity_ShouldReturnTrue()
        {
            _elevator.CurrentPeopleCount = 5;
            bool canBoard = _elevator.CanBoardPassengers(4);

            Assert.IsTrue(canBoard);
        }

        [TestMethod]
        public void CanBoardPassengers_ExceedingCapacity_ShouldReturnFalse()
        {
            _elevator.CurrentPeopleCount = 9;
            bool canBoard = _elevator.CanBoardPassengers(5);

            Assert.IsFalse(canBoard);
        }

        [TestMethod]
        public void TriggerMoveToFloorEvent_ShouldTriggerMovedToFloorEvent()
        {
            bool eventTriggered = false;
            _elevator.MovedToFloor += (sender, floor) => eventTriggered = true;

            _elevator.TriggerMoveToFloorEvent(3);

            Assert.IsTrue(eventTriggered);
        }
    }
}
