using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Movement.Engine.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Actions;

namespace Hexagon.RoboSim.Movement.Engine.Directions
{
    public class West<T,U> : IDirection<T,U>
    {
        public IDirection<T,U> NextFacing(IAction<T,U> action)
        {
            if (action.GetType() == typeof(Left<T,U>))
            {
                return new South<T,U>();
            }
            else if (action.GetType() == typeof(Right<T,U>))
            {
                return new North<T,U>();
            }

            return this;
        }
        public Cordinate<T> Move(Cordinate<T> current, U step, T xLimit, T yLimit)
        {
            if ((dynamic)current.X <= 0) return current;

            return new Cordinate<T>
            {
                X = (dynamic)current.X - (dynamic)step,
                Y = current.Y
            };
        }
    }
}
