using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;
using System;
using System.Collections.Generic;

namespace ElevatorChallenge.Domain.Models
{
    public interface IBuilding
    {
        List<Elevator> Elevators { get; }
        int TotalFloors { get; }
        event Action<string> OnElevatorNotFoundEvent;
        IElevator CallElevator(int floor, int passengersWaiting, IElevatorStrategy elevatorStrategy);
    }
}