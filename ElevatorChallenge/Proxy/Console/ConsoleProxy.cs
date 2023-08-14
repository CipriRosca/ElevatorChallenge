using System;

namespace ElevatorChallenge.Proxy.Console
{
    //Proxy created for unit testing purpose
    public class ConsoleProxy : IConsoleProxy
    {
        public void WriteLine(string message)
        {
            System.Console.WriteLine(message);
        }

        public void WriteLine()
        {
            System.Console.WriteLine();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return System.Console.ReadKey();
        }
    }
}
