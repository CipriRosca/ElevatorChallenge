using System;

namespace ElevatorChallenge.Domain.Models
{
    public class Elevator : IElevator
    {
        private const int MAX_CAPACITY = 10;

        public int Id { get; private set; }
        public int CurrentFloor { get; set; } = 1;
        public int CurrentPeopleCount { get; set; } = 0;
        public ElevatorDirection Direction { get; set; } = ElevatorDirection.Stationary;

        public event Action<Elevator, int> MovedToFloor;

        public Elevator(int id) => Id = id;

        public void MoveToFloor(int targetFloor)
        {
            Direction = CurrentFloor < targetFloor ? ElevatorDirection.Up : ElevatorDirection.Down;

            while (CurrentFloor != targetFloor)
            {
                System.Threading.Thread.Sleep(1000);
                CurrentFloor += (Direction == ElevatorDirection.Up) ? 1 : -1;

                MovedToFloor?.Invoke(this, CurrentFloor);
            }

            Direction = ElevatorDirection.Stationary;
        }

        public void TriggerMoveToFloorEvent(int floor)
        {
            MovedToFloor?.Invoke(this, floor);
        }

        public void BoardPassengers(int numberOfPassengers)
        {
            if (CanBoardPassengers(numberOfPassengers))
            {
                CurrentPeopleCount += numberOfPassengers;
                System.Threading.Thread.Sleep(numberOfPassengers * 200);
            }
        }

        public bool CanBoardPassengers(int numberOfPassengers)
        {
            return CurrentPeopleCount + numberOfPassengers <= MAX_CAPACITY;
        }

        public void DeboardAllPassengers()
        {
            CurrentPeopleCount = 0;
        }
    }
}
