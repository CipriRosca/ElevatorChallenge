using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using ElevatorChallenge.Domain.Models;
using ElevatorChallenge.Domain.Factories.ElevatorFactory;
using ElevatorChallenge.Domain.Enums;
using ElevatorChallenge.Common.Helpers;
using ElevatorChallenge.Proxy.Environment;

namespace ElevatorChallenge.BusinessLogic.Facade
{
    public class ElevatorSimulatorFacade : IElevatorSimulatorFacade
    {
        private readonly IUserInteractionService _userInteractionService;
        private readonly IBuildingService _buildingService;
        private readonly IConsolePresentationService _consoleService;
        private readonly IElevatorFactory _elevatorFactory;
        private readonly IEnvironmentProxy _applicationService;
        private readonly MenuHelper _menuHelper;

        public ElevatorSimulatorFacade(IUserInteractionService interactionService, IBuildingService buildingService, IConsolePresentationService consoleService, IElevatorFactory elevatorFactory, IEnvironmentProxy applicationService)
        {
            _userInteractionService = interactionService;
            _buildingService = buildingService;
            _consoleService = consoleService;
            _elevatorFactory = elevatorFactory;
            _applicationService = applicationService;

            _menuHelper = new MenuHelper(_consoleService);
        }

        private void InitializeBuilding()
        {
            _consoleService.DisplayMessage("Elevator challenge!");

            int totalFloors = _userInteractionService.GetUserInput("Enter the total number of floors in the building:", 1, int.MaxValue);
            int totalElevators = _userInteractionService.GetUserInput("Enter the number of elevators:", 1, int.MaxValue);

            _buildingService.Initialize(totalFloors, totalElevators, _elevatorFactory);
            _buildingService.OnElevatorNotFound += _consoleService.DisplayMessage;
        }

        public void RunSimulation(bool initializeFirstTimeSetup)
        {
            if (initializeFirstTimeSetup) InitializeBuilding();

            UserChoiceAction action;
            do
            {
                action = HandleUserChoice();
            } while (action != UserChoiceAction.Exit);
        }

        private UserChoiceAction HandleUserChoice()
        {
            _consoleService.NewLine(1);
            _menuHelper.ShowMenu();

            int choice = _userInteractionService.GetUserInput("Please choose a valid option:", 1, 3);

            switch (choice)
            {
                case 1:
                    CallElevatorAndBoardPassengers();
                    return UserChoiceAction.CallElevator;
                case 2:
                    _menuHelper.ShowElevatorStatuses();
                    _menuHelper.ShowMenu();
                    return UserChoiceAction.ShowStatuses;
                case 3:
                    ExitSimulation();
                    return UserChoiceAction.Exit;
                default:
                    _consoleService.DisplayMessage("Invalid choice. Please choose a valid option.");
                    return UserChoiceAction.Invalid;
            }
        }

        private void CallElevatorAndBoardPassengers()
        {
            int destinationFloor = GetUserDestinationFloor();
            if (!IsValidFloor(destinationFloor)) return;

            int passengersWaiting = _userInteractionService.GetUserInput("Enter the number of passengers waiting:", 1, int.MaxValue);
            var selectedStrategy = _userInteractionService.ChooseElevatorStrategy();

            var elevator = _buildingService.CallElevator(destinationFloor, passengersWaiting, selectedStrategy);

            if (elevator != null) MovePassengersToDestionationFloor(destinationFloor, passengersWaiting, elevator);
        }

        private void MovePassengersToDestionationFloor(int floor, int passengersWaiting, IElevator elevator)
        {
            elevator.MoveToFloor(floor);
            BoardPassengersOntoElevator(elevator, passengersWaiting);

            PostArrivalAction(elevator);
            _consoleService.Pause();
        }

        private int GetUserDestinationFloor()
        {
            var numberOfFloors = _buildingService.BuildingInstance.TotalFloors;
            return _userInteractionService.GetUserInput($"From which floor are you calling the elevator? (1 to {numberOfFloors}):", 1, numberOfFloors);
        }

        private bool IsValidFloor(int floor)
        {
            if (floor <= _buildingService?.BuildingInstance?.TotalFloors) return true;

            _consoleService.DisplayMessage($"Invalid floor. Please choose a floor between 1 and {_buildingService?.BuildingInstance?.TotalFloors}.");
            return false;
        }

        private void BoardPassengersOntoElevator(IElevator elevator, int passengers)
        {
            elevator.BoardPassengers(passengers);
            _consoleService.DisplayMessage($"{passengers} passengers boarded on Elevator {elevator.Id} at floor {elevator.CurrentFloor}. (Total number of passengers: {elevator.CurrentPeopleCount})");
        }

        private void PostArrivalAction(IElevator elevator)
        {
            bool isUserDoneWithActions = false;

            while (!isUserDoneWithActions)
            {
                _menuHelper.DisplayPostArrivalMenu();
                var choice = (ElevatorActions)_userInteractionService.GetUserInput("Enter your choice:", 1, 4);

                switch (choice)
                {
                    case ElevatorActions.DeboardPassengers:
                        DeboardAllPassengers(elevator);
                        _menuHelper.ShowMenu();
                        RunSimulation(initializeFirstTimeSetup: false);
                        isUserDoneWithActions = true;
                        break;

                    case ElevatorActions.MoveElevator:
                        MoveElevatorToDifferentFloor(elevator);
                        break;

                    case ElevatorActions.CallNewElevator:
                        CallElevatorAndBoardPassengers();
                        break;

                    case ElevatorActions.Exit:
                        ExitSimulation();
                        return;
                }
            }
        }

        private void DeboardAllPassengers(IElevator elevator)
        {
            elevator.DeboardAllPassengers();
            _consoleService.DisplayMessage("All passengers have deboarded.");
        }

        private void MoveElevatorToDifferentFloor(IElevator elevator)
        {
            var totalNumberOfFloors = _buildingService.BuildingInstance.TotalFloors;
            int targetFloor = _userInteractionService.GetUserInput($"Which floor would you like to move the elevator to? (1 to {totalNumberOfFloors}):", 1, totalNumberOfFloors);
            elevator.MoveToFloor(targetFloor);
            _consoleService.DisplayMessage($"Elevator {elevator.Id} has moved to floor {targetFloor}. (Total number of passengers: {elevator.CurrentPeopleCount})");
        }

        private void ExitSimulation()
        {
            _consoleService.DisplayMessage("Goodbye!");
            _applicationService.Exit();
        }
    }
}
