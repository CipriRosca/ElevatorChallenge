using ElevatorChallenge.Domain.Models;

namespace ElevatorChallenge.BusinessLogic.Services.Interfaces
{
    public interface IConsolePresentationService
    {
        int GetUserChoice(string prompt, int min, int max);
        void DisplayMessage(string message);
        void DisplayElevatorStatuses();
        public void HandleElevatorEvents(Elevator elevator);
        void DisplaySeparator();
        void NewLine(int numberOfLines = 1);
        void DisplayHeader(string headerText);
        void Pause();
    }
}
