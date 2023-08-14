using ElevatorChallenge.Domain.Models;
using System.Collections.Generic;

namespace ElevatorChallenge.BusinessLogic.Strategies.Abstractions
{
    public interface IElevatorStrategy
    {
        IElevator ChooseElevator(List<Elevator> elevators, int floor, int passengers);
    }
}
