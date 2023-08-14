using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;
using ElevatorChallenge.BusinessLogic.Strategies.Concrete;

namespace ElevatorChallenge.BusinessLogic.Factories.ElevatorStrategyFactory
{
    public class ElevatorStrategyFactory
    {
        public static IElevatorStrategy CreateStrategy(int choice)
        {
            return choice switch
            {
                1 => new ProximityStrategy(),
                2 => new OccupancyStrategy(),
                _ => new ProximityStrategy(), 
            };
        }
    }
}
