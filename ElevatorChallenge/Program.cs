using ElevatorChallenge.BusinessLogic.Services;
using ElevatorChallenge.BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using ElevatorChallenge.Domain.Factories.ElevatorFactory;
using ElevatorChallenge.BusinessLogic.Facade;
using ElevatorChallenge.Proxy.Console;
using ElevatorChallenge.Proxy.Environment;

namespace ElevatorChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            var elevatorSimulator = serviceProvider.GetService<IElevatorSimulatorFacade>();

            RunSimulator(elevatorSimulator);
        }

        private static void RunSimulator(IElevatorSimulatorFacade elevatorSimulator)
        {
            while (true)
            {
                try
                {
                    elevatorSimulator.RunSimulation(initializeFirstTimeSetup: true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                    .AddSingleton<IConsoleProxy, ConsoleProxy>()
                    .AddSingleton<IConsolePresentationService, ConsolePresentationService>()
                    .AddSingleton<IEnvironmentProxy, EnvironmentProxy>()
                    .AddSingleton<IElevatorFactory, ElevatorFactory>()
                    .AddScoped<IElevatorStatusService, ElevatorStatusService>()
                    .AddScoped<IBuildingService, BuildingService>()
                    .AddScoped<IElevatorSimulatorFacade, ElevatorSimulatorFacade>()
                    .AddScoped<IUserInteractionService, UserInteractionService>()
                    .BuildServiceProvider();
        }
    }
}
