using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using ElevatorChallenge.Domain.Models;

namespace ElevatorChallenge.Domain.Factories.ElevatorFactory
{
    public class ElevatorFactory : IElevatorFactory
    {
        private readonly IConsolePresentationService _consoleService;

        public ElevatorFactory(IConsolePresentationService consoleService)
        {
            _consoleService = consoleService;
        }

        public Elevator CreateElevator(int id)
        {
            var elevator = new Elevator(id);
            _consoleService.HandleElevatorEvents(elevator);
            return elevator;
        }
    }
}
