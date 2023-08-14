using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;
using ElevatorChallenge.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorChallenge.BusinessLogic.Strategies.Concrete
{
    public class OccupancyStrategy : IElevatorStrategy
    {
        public IElevator ChooseElevator(List<Elevator> elevators, int floor, int passengersWaiting)
        {
            var sortedElevators = elevators.OrderBy(elevator => elevator.CurrentPeopleCount);

            return sortedElevators.FirstOrDefault(e => e.CanBoardPassengers(passengersWaiting));
        }
    }
}
