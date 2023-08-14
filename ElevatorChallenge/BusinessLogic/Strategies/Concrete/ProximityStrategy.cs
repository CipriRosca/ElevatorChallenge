using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;
using ElevatorChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorChallenge.BusinessLogic.Strategies.Concrete
{
    public class ProximityStrategy : IElevatorStrategy
    {
        public IElevator ChooseElevator(List<Elevator> elevators, int floor, int passengersWaiting)
        {
            var sortedElevators = elevators.OrderBy(elevator => Math.Abs(elevator.CurrentFloor - floor));

            return sortedElevators.FirstOrDefault(e => e.CanBoardPassengers(passengersWaiting));
        }
    }
}
