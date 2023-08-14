using ElevatorChallenge.Domain.Models;

namespace ElevatorChallenge.Domain.Factories.ElevatorFactory
{
    public interface IElevatorFactory
    {
        Elevator CreateElevator(int id);
    }
}
