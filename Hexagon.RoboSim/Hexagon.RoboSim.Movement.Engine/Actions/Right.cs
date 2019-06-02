using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Models.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Abstractions;

namespace Hexagon.RoboSim.Movement.Engine.Actions
{
    public class Right<T, U> : IAction<T, U>
    {
        public Cordinate<T> ComputeNewCordinate(Cordinate<T> cordinate, U step, IMoveArea<T> moveArea, IDirection<T, U> direction)
        {
            return cordinate;
        }
    }
}
