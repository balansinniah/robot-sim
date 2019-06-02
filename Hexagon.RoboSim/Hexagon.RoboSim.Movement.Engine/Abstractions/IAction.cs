using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Models.Abstractions;

namespace Hexagon.RoboSim.Movement.Engine.Abstractions
{
    public interface IAction<T, U>
    {
        Cordinate<T> ComputeNewCordinate(Cordinate<T> cordinate, U step, IMoveArea<T> moveArea, IDirection<T, U> direction);
    }
}
