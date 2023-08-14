using ElevatorChallenge.BusinessLogic.Strategies.Abstractions;

namespace ElevatorChallenge.BusinessLogic.Services.Interfaces
{
    public interface IUserInteractionService
    {
        int GetUserInput(string prompt, int minValue, int max);
        IElevatorStrategy ChooseElevatorStrategy();
    }
}
