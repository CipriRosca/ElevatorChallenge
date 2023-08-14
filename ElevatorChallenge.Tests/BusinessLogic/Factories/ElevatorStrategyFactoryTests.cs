using ElevatorChallenge.BusinessLogic.Factories.ElevatorStrategyFactory;
using ElevatorChallenge.BusinessLogic.Strategies.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevatorChallenge.Tests.BusinessLogic.Factories
{
    [TestClass]
    public class ElevatorStrategyFactoryTests
    {
        [TestMethod]
        public void CreateStrategy_WithChoice1_ShouldReturnProximityStrategy()
        {
            // Act
            var strategy = ElevatorStrategyFactory.CreateStrategy(1);

            // Assert
            Assert.IsInstanceOfType(strategy, typeof(ProximityStrategy));
        }

        [TestMethod]
        public void CreateStrategy_WithChoice2_ShouldReturnOccupancyStrategy()
        {
            // Act
            var strategy = ElevatorStrategyFactory.CreateStrategy(2);

            // Assert
            Assert.IsInstanceOfType(strategy, typeof(OccupancyStrategy));
        }

        [TestMethod]
        public void CreateStrategy_WithInvalidChoice_ShouldReturnProximityStrategy()
        {
            // Act
            var strategy = ElevatorStrategyFactory.CreateStrategy(99);

            // Assert
            Assert.IsInstanceOfType(strategy, typeof(ProximityStrategy));
        }
    }
}
