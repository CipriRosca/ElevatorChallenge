using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;
using ElevatorChallenge.Domain.Factories.ElevatorFactory;
using ElevatorChallenge.Domain.Models;
using System;
using System.Collections.Generic;

namespace ElevatorChallenge.BusinessLogic.Services.Interfaces
{
    public interface IBuildingService
    {
        IBuilding BuildingInstance { get; }
        void Initialize(int totalFloors, int totalElevators, IElevatorFactory elevatorFactory);
        IElevator CallElevator(int floor, int passengersWaiting, IElevatorStrategy elevatorStrategy);
        IEnumerable<Elevator> GetElevators();
        
        event Action<string> OnElevatorNotFound;
    }
}
