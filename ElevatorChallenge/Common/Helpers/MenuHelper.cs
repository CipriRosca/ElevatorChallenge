using ElevatorChallenge.BusinessLogic.Services.Interfaces;

namespace ElevatorChallenge.Common.Helpers
{
    public class MenuHelper
    {
        private readonly IConsolePresentationService _consoleService;

        public MenuHelper(IConsolePresentationService consoleService)
        {
            _consoleService = consoleService;
        }

        public void DisplayPostArrivalMenu()
        {
            _consoleService.NewLine(2);
            _consoleService.DisplayMessage("Choose an action:");
            _consoleService.DisplayMessage("1. Deboard all passengers");
            _consoleService.DisplayMessage("2. Move to a different floor");
            _consoleService.DisplayMessage("3. Call elevator");
            _consoleService.DisplayMessage("4. Show elevator statuses");
            _consoleService.DisplayMessage("5. Exit");
        }

        public void ShowElevatorStatuses()
        {
            _consoleService.DisplayHeader("Elevator Statuses");
            _consoleService.DisplayElevatorStatuses();
            _consoleService.DisplaySeparator();
        }

        public void ShowMenu()
        {
            _consoleService.DisplayMessage("\nChoose an action:");
            _consoleService.DisplayMessage("1. Call an elevator.");
            _consoleService.DisplayMessage("2. Show elevator statuses.");
            _consoleService.DisplayMessage("3. Exit Menu.");
        }
    }
}
