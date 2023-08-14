using System;

namespace ElevatorChallenge.Proxy.Console
{
    public interface IConsoleProxy
    {
        void WriteLine(string message);
        void WriteLine();
        ConsoleKeyInfo ReadKey();
    }
}
