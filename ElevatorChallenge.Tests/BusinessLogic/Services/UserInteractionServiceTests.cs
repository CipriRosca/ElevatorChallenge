using ElevatorChallenge.BusinessLogic.Services;
using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using ElevatorChallenge.BusinessLogic.Strategies.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElevatorChallenge.Tests.BusinessLogic.Services
{
    [TestClass]
    public class UserInteractionServiceTests
    {
        private Mock<IConsolePresentationService> _mockConsoleService;
        private UserInteractionService _userService;

        [TestInitialize]
        public void SetUp()
        {
            _mockConsoleService = new Mock<IConsolePresentationService>();
            _userService = new UserInteractionService(_mockConsoleService.Object);
        }

        [TestMethod]
        public void GetUserInput_ValidInput_ReturnsInput()
        {
            // Arrange
            _mockConsoleService.Setup(s => s.GetUserChoice(It.IsAny<string>(), 1, It.IsAny<int>())).Returns(5);

            // Act
            var result = _userService.GetUserInput("ABCD:", 1, It.IsAny<int>());

            // Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void ChooseElevatorStrategy_Choice1_ReturnsNearestElevatorStrategy()
        {
            // Arrange
            _mockConsoleService.SetupSequence(s => s.GetUserChoice(It.IsAny<string>(), 1, It.IsAny<int>()))
                .Returns(1);

            // Act
            var strategy = _userService.ChooseElevatorStrategy();

            // Assert
            Assert.IsInstanceOfType(strategy, typeof(ProximityStrategy));
        }

        [TestMethod]
        public void ChooseElevatorStrategy_Choice2_ReturnsLeastOccupancyStrategy()
        {
            // Arrange
            _mockConsoleService.SetupSequence(s => s.GetUserChoice(It.IsAny<string>(), 1, It.IsAny<int>()))
                .Returns(2);

            // Act
            var strategy = _userService.ChooseElevatorStrategy();

            // Assert
            Assert.IsInstanceOfType(strategy, typeof(OccupancyStrategy));
        }
    }
}
