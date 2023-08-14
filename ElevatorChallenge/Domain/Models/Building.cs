using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;
using System;
using System.Collections.Generic;

namespace ElevatorChallenge.Domain.Models
{
    public class Building : IBuilding
    {
        public List<Elevator> Elevators { get; private set; }
        public virtual int TotalFloors { get; private set; }

        public event Action<string> OnElevatorNotFoundEvent;

        public Building(int totalFloors, List<Elevator> elevators)
        {
            Elevators = elevators;
            TotalFloors = totalFloors;
        }

        public Building() { }

        public IElevator CallElevator(int floor, int passengersWaiting, IElevatorStrategy elevatorStrategy)
        {
            IElevator suitableElevator = elevatorStrategy.ChooseElevator(Elevators, floor, passengersWaiting);
            if (suitableElevator == null) OnElevatorNotFoundEvent?.Invoke("No suitable elevators were found.");

            return suitableElevator;
        }
    }
}
