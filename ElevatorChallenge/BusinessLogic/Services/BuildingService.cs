using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ElevatorChallenge.Domain.Models;
using ElevatorChallenge.Domain.Factories.ElevatorFactory;
using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;

namespace ElevatorChallenge.BusinessLogic.Services
{
    public class BuildingService : IBuildingService
    {
        public event Action<string> OnElevatorNotFound;

        public IBuilding BuildingInstance { get; private set; }

        public BuildingService()
        { }

        public void Initialize(int totalFloors, int totalElevators, IElevatorFactory elevatorFactory)
        {
            var elevators = Enumerable.Range(1, totalElevators)
                                      .Select(i => elevatorFactory.CreateElevator(i))
                                      .ToList();

            BuildingInstance = new Building(totalFloors, elevators);

            BuildingInstance.OnElevatorNotFoundEvent += message => OnElevatorNotFound?.Invoke(message);
        }

        public IElevator CallElevator(int floor, int passengersWaiting, IElevatorStrategy elevatorStrategy)
            => BuildingInstance.CallElevator(floor, passengersWaiting, elevatorStrategy);

        public IEnumerable<Elevator> GetElevators()
            => BuildingInstance.Elevators;
    }
}