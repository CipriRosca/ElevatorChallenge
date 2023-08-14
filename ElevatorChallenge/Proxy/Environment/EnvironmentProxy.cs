namespace ElevatorChallenge.Proxy.Environment
{
    //Proxy created for unit testing purpose
    public class EnvironmentProxy : IEnvironmentProxy
    {
        public void Exit()
        {
            System.Environment.Exit(0);
        }
    }
}
