using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using ElevatorChallenge.Domain.Factories.ElevatorFactory;
using ElevatorChallenge.BusinessLogic.Facade;
using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;
using ElevatorChallenge.Domain.Models;
using ElevatorChallenge.Proxy.Environment;

namespace ElevatorChallenge.Tests.BusinessLogic.Facade
{
    [TestClass]
    public class ElevatorSimulatorFacadeTests
    {
        private Mock<IUserInteractionService> _mockUserInteractionService;
        private Mock<IBuildingService> _mockBuildingService;
        private Mock<IConsolePresentationService> _mockConsoleService;
        private Mock<IElevatorFactory> _mockElevatorFactory;
        private Mock<IEnvironmentProxy> _mockApplicationService;
        private ElevatorSimulatorFacade _elevatorSimulatorFacade;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserInteractionService = new Mock<IUserInteractionService>();
            _mockBuildingService = new Mock<IBuildingService>();
            _mockConsoleService = new Mock<IConsolePresentationService>();
            _mockApplicationService = new Mock<IEnvironmentProxy>();
            _mockElevatorFactory = new Mock<IElevatorFactory>();
            _elevatorSimulatorFacade = new ElevatorSimulatorFacade(
                _mockUserInteractionService.Object,
                _mockBuildingService.Object,
                _mockConsoleService.Object,
                _mockElevatorFactory.Object,
                _mockApplicationService.Object
            );

            var mockBuilding = new Mock<IBuilding>();
            mockBuilding.Setup(b => b.TotalFloors).Returns(10);
            _mockBuildingService.Setup(bs => bs.BuildingInstance).Returns(mockBuilding.Object);
        }

        [TestMethod]
        public void RunSimulation_CallElevator_ActionTaken()
        {
            // Arrange
            _mockUserInteractionService.SetupSequence(x => x.GetUserInput(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)  // call elevator choice
                .Returns(3)  // floor number
                .Returns(2)  // passenger count
                .Returns(3); // exit loop

            var mockBuilding = new Mock<IBuilding>();
            mockBuilding.Setup(b => b.TotalFloors).Returns(10); // Or whatever value you want.
            _mockBuildingService.Setup(bs => bs.BuildingInstance).Returns(mockBuilding.Object);

            // Act
            _elevatorSimulatorFacade.RunSimulation(initializeFirstTimeSetup: false);

            // Assert
            _mockBuildingService.Verify(x => x.CallElevator(3, 2, It.IsAny<IElevatorStrategy>()), Times.Once);
        }

        [TestMethod]
        public void RunSimulation_InitializeFirstTime_SetupsBuilding()
        {
            // Arrange
            _mockUserInteractionService.Setup(x => x.GetUserInput("Enter the total number of floors in the building:", It.IsAny<int>(), It.IsAny<int>())).Returns(10);
            _mockUserInteractionService.Setup(x => x.GetUserInput("Enter the number of elevators:", It.IsAny<int>(), It.IsAny<int>())).Returns(3);
            _mockUserInteractionService.Setup(x => x.GetUserInput("Please choose a valid option:", It.IsAny<int>(), It.IsAny<int>())).Returns(3);

            // Act
            _elevatorSimulatorFacade.RunSimulation(true);

            // Assert
            _mockBuildingService.Verify(x => x.Initialize(10, 3, It.IsAny<IElevatorFactory>()), Times.Once);
        }

        [TestMethod]
        public void RunSimulation_UserChoosesExit_ExitsSimulation()
        {
            // Arrange
            _mockUserInteractionService.Setup(x => x.GetUserInput("Please choose a valid option:", It.IsAny<int>(), It.IsAny<int>())).Returns(3);
            _mockApplicationService.Setup(x => x.Exit()).Verifiable();

            // Act
            _elevatorSimulatorFacade.RunSimulation(false);

            // Assert
            _mockApplicationService.Verify(x => x.Exit(), Times.Once);
        }

        [TestMethod]
        public void RunSimulation_InvalidFloorEntry_ShowsInvalidFloorMessage()
        {
            // Arrange
            _mockUserInteractionService.SetupSequence(x => x.GetUserInput(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)  // call elevator choice
                .Returns(3); // exit loop

            var mockBuilding = new Mock<IBuilding>();
            mockBuilding.Setup(b => b.TotalFloors).Returns(10);
            _mockBuildingService.Setup(bs => bs.BuildingInstance).Returns(mockBuilding.Object);

            _mockUserInteractionService.Setup(x => x.GetUserInput($"From which floor are you calling the elevator? (1 to {mockBuilding.Object.TotalFloors}):", It.IsAny<int>(), It.IsAny<int>())).Returns(15);

            // Act
            _elevatorSimulatorFacade.RunSimulation(false);

            // Assert
            _mockConsoleService.Verify(x => x.DisplayMessage("Invalid floor. Please choose a floor between 1 and 10."), Times.Once);
        }

        [TestMethod]
        public void RunSimulation_ValidElevatorCall_PassengersBoarded()
        {
            // Arrange
            var mockElevator = new Mock<IElevator>();
            mockElevator.SetupProperty(e => e.CurrentPeopleCount);
            mockElevator.SetupProperty(e => e.CurrentFloor);
            mockElevator.Setup(e => e.BoardPassengers(It.IsAny<int>())).Callback<int>(p => mockElevator.Object.CurrentPeopleCount += p);

            _mockBuildingService.Setup(x => x.CallElevator(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IElevatorStrategy>())).Returns(mockElevator.Object);

            _mockUserInteractionService.SetupSequence(x => x.GetUserInput(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)  // Choice to call elevator
                .Returns(5)  // Floor number
                .Returns(4)  // Passengers number
                .Returns(4)  // Exist post arrival loop
                .Returns(3); // Exit main loop

            // Act
            _elevatorSimulatorFacade.RunSimulation(false);

            // Assert
            Assert.AreEqual(4, mockElevator.Object.CurrentPeopleCount);
            _mockConsoleService.Verify(x => x.DisplayMessage($"4 passengers boarded on Elevator {mockElevator.Object.Id} at floor {mockElevator.Object.CurrentFloor}. (Total number of passengers: {mockElevator.Object.CurrentPeopleCount})"), Times.Once);
        }
    }
}
