using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using ElevatorChallenge.BusinessLogic.Factories.ElevatorStrategyFactory;
using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;

namespace ElevatorChallenge.BusinessLogic.Services
{
    public class UserInteractionService : IUserInteractionService
    {
        private readonly IConsolePresentationService _consoleService;

        public UserInteractionService(IConsolePresentationService consoleService)
        {
            _consoleService = consoleService;
        }

        public int GetUserInput(string prompt, int minValue, int max) => _consoleService.GetUserChoice(prompt, minValue, max);

        public IElevatorStrategy ChooseElevatorStrategy()
        {
            _consoleService.NewLine(1);
            _consoleService.DisplayMessage("Which elevator selection strategy would you prefer?");
            _consoleService.DisplayMessage("1. Select the nearest elevator to your location.");
            _consoleService.DisplayMessage("2. Select an elevator with the least occupancy.");

            var choice = GetUserInput("Enter your choice (1-2):", 1, 2);

            return ElevatorStrategyFactory.CreateStrategy(choice);
        }
    }
}