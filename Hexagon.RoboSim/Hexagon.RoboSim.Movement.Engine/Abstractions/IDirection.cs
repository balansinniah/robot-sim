using Hexagon.RoboSim.Models;

namespace Hexagon.RoboSim.Movement.Engine.Abstractions
{
    public interface IDirection<T,U>
    {
        IDirection<T,U> NextFacing(IAction<T,U> action);
        Cordinate<T> Move(Cordinate<T> current, U step, T xLimit, T yLimit);
    }
}
