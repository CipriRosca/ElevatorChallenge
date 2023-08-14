using ElevatorChallenge.BusinessLogic.Services;
using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using ElevatorChallenge.Domain.Models;
using ElevatorChallenge.Proxy.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ElevatorChallenge.Tests.BusinessLogic.Services
{
    [TestClass]
    public class ConsolePresentationServiceTests
    {
        private Mock<IConsoleProxy> _mockConsole;
        private Mock<IElevatorStatusService> _mockStatusService;
        private ConsolePresentationService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockConsole = new Mock<IConsoleProxy>();
            _mockStatusService = new Mock<IElevatorStatusService>();

            _service = new ConsolePresentationService(_mockStatusService.Object, _mockConsole.Object);
        }

        [TestMethod]
        public void DisplayElevatorStatuses_ShouldPrintAllStatuses()
        {
            // Arrange
            var statuses = new List<string> { "Status1", "Status2", "Status3" };
            _mockStatusService.Setup(s => s.GetElevatorStatuses()).Returns(statuses);

            // Act
            _service.DisplayElevatorStatuses();

            // Assert
            _mockConsole.Verify(c => c.WriteLine("\nElevator Statuses:"), Times.Once);
            foreach (var status in statuses)
            {
                _mockConsole.Verify(c => c.WriteLine(status), Times.Once);
            }
        }

        [TestMethod]
        public void DisplayMessage_ShouldWriteToConsole()
        {
            // Arrange
            var message = "Test message";

            // Act
            _service.DisplayMessage(message);

            // Assert
            _mockConsole.Verify(c => c.WriteLine(message), Times.Once);
        }

        [TestMethod]
        public void HandleElevatorEvents_ElevatorMoved_ShouldDisplayCorrectMessage()
        {
            // Arrange
            var elevator = new Elevator(1);

            // Act
            _service.HandleElevatorEvents(elevator);
            elevator.TriggerMoveToFloorEvent(5); 

            // Assert
            _mockConsole.Verify(c => c.WriteLine("Elevator 1 is now on floor 5."), Times.Once);
        }

        [TestMethod]
        public void NewLine_ShouldWriteEmptyLines()
        {
            // Act
            _service.NewLine(3);

            // Assert
            _mockConsole.Verify(c => c.WriteLine(), Times.Exactly(3));
        }

        [TestMethod]
        public void DisplaySeparator_ShouldDisplayLine()
        {
            // Act
            _service.DisplaySeparator();

            // Assert
            _mockConsole.Verify(c => c.WriteLine(new string('-', 100)), Times.Once);
        }

        [TestMethod]
        public void DisplayHeader_ShouldDisplayFormattedHeader()
        {
            // Act
            _service.DisplayHeader("Test Header");

            // Assert
            _mockConsole.Verify(c => c.WriteLine("===== Test Header ====="), Times.Once);
        }

        [TestMethod]
        public void Pause_ShouldPromptForAnyKey()
        {
            // Act
            _service.Pause();

            // Assert
            _mockConsole.Verify(c => c.WriteLine("Press any key to continue..."), Times.Once);
            _mockConsole.Verify(c => c.ReadKey(), Times.Once);
        }
    }
}
