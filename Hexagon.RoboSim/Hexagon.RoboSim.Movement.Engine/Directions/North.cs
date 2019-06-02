using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Movement.Engine.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Actions;

namespace Hexagon.RoboSim.Movement.Engine.Directions
{
    public class North<T,U> : IDirection<T,U>
    {
        public Cordinate<T> Move(Cordinate<T> current, U step, T xLimit, T yLimit)
        {
            if ((dynamic)current.Y >= (dynamic)yLimit) return current;

            return new Cordinate<T>
            {
                X = (dynamic)current.X,
                Y = (dynamic)current.Y + (dynamic)step
            };
        }

        public IDirection<T,U> NextFacing(IAction<T,U> action)
        {
            if (action.GetType() == typeof(Left<T,U>))
            {
                return new West<T,U>();
            }
            else if (action.GetType() == typeof(Right<T,U>))
            {
                return new East<T,U>();
            }

            return this;
        }
    }
}
