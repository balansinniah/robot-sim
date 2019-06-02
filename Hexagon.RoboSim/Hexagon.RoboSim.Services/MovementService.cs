using Hexagon.RoboSim.Movement.Engine.Abstractions;
using Hexagon.RoboSim.Services.Abstractions;

namespace Hexagon.RoboSim.Services
{
    public class MovementService<T,U> : IMovementService<T,U> 
    {
        private readonly IRobo<T, U> _robot;

        public MovementService(IRobo<T,U> robot)
        {
            _robot = robot;
        }

        public void Initialize(string command)
        {
            _robot.Initialize(command);
        }

        public string ProcessCommand(string command)
        {
           return _robot.ProcessAction(command);
        }
    }
}
