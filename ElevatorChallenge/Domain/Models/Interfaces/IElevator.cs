using System;

namespace ElevatorChallenge.Domain.Models
{
    public interface IElevator
    {
        int Id { get; }
        int CurrentFloor { get; set; }
        int CurrentPeopleCount { get; set; }
        ElevatorDirection Direction { get; set; }

        event Action<Elevator, int> MovedToFloor;

        void BoardPassengers(int numberOfPassengers);
        bool CanBoardPassengers(int numberOfPassengers);
        void DeboardAllPassengers();
        void MoveToFloor(int targetFloor);
        void TriggerMoveToFloorEvent(int floor);
    }
}