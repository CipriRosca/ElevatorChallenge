using System.Collections.Generic;

namespace ElevatorChallenge.BusinessLogic.Services.Interfaces
{
    public interface IElevatorStatusService
    {
        IEnumerable<string> GetElevatorStatuses();
    }
}
