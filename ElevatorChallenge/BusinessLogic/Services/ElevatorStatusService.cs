using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using System.Collections.Generic;

namespace ElevatorChallenge.BusinessLogic.Services
{
    public class ElevatorStatusService : IElevatorStatusService
    {
        private readonly IBuildingService _buildingService;

        public ElevatorStatusService(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        public IEnumerable<string> GetElevatorStatuses()
        {
            var statuses = new List<string>();
            var elevators = _buildingService.GetElevators();

            foreach (var elevator in elevators)
            {
                statuses.Add($"Elevator {elevator.Id} - Floor: {elevator.CurrentFloor} - Direction: {elevator.Direction} - Passengers: {elevator.CurrentPeopleCount}");
            }

            return statuses;
        }
    }
}
