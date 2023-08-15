using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using ElevatorChallenge.Domain.Models;
using ElevatorChallenge.Proxy.Console;
using System;

namespace ElevatorChallenge.BusinessLogic.Services
{
    public class ConsolePresentationService : IConsolePresentationService
    {
        private readonly IElevatorStatusService _elevatorStatusService;
        private readonly IConsoleProxy _console;

        public ConsolePresentationService(IElevatorStatusService elevatorStatusService, IConsoleProxy console)
        {
            _elevatorStatusService = elevatorStatusService;
            _console = console;
        }
        public void DisplayElevatorStatuses()
        {
            DisplayMessage("\nElevator Statuses:");
            
            foreach (var status in _elevatorStatusService.GetElevatorStatuses())
            {
                DisplayMessage(status);
            }
        }

        public int GetUserChoice(string prompt, int min, int max)
        {
            while (true)
            {
                DisplayMessage(prompt);

                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }

                DisplayMessage($"Please enter a valid number.");
            }
        }

        public void DisplayMessage(string message) => _console.WriteLine(message);
        public void HandleElevatorEvents(Elevator elevator) => elevator.MovedToFloor += Elevator_MovedToFloor;
        private void Elevator_MovedToFloor(Elevator sender, int floor) => DisplayMessage($"Elevator {sender.Id} is now on floor {floor}.");
        private void Elevator_PassengersBoarded(Elevator sender, int numberOfPassengers) => DisplayMessage($"{numberOfPassengers} passengers boarded on Elevator {sender.Id}.");
        public void DisplaySeparator() => DisplayMessage(new string('-', 100));
        public void DisplayHeader(string headerText) => DisplayMessage($"===== {headerText} =====");

        public void NewLine(int numberOfLines = 1)
        {
            for (int i = 0; i < numberOfLines; i++)
            {
                _console.WriteLine();
            }
        }

        public void Pause()
        {
            DisplayMessage("Press any key to continue...");
            _console.ReadKey();
        }
    }
}
