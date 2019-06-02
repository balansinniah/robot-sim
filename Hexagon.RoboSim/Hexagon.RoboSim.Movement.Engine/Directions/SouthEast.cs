using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Movement.Engine.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Actions;

namespace Hexagon.RoboSim.Movement.Engine.Directions
{
    public class SouthEast<T, U> : IDirection<T, U>
    {
        public Cordinate<T> Move(Cordinate<T> current, U step, T xLimit, T yLimit)
        {
            if ((dynamic)current.Y <= 0 && (dynamic)current.X >= xLimit) return current;

            if ((dynamic)current.Y <= 0 && (dynamic)current.X < xLimit) return current;

            if ((dynamic)current.Y > 0 && (dynamic)current.X >= xLimit) return current;

            return new Cordinate<T>
            {
                X = (dynamic)current.X + (dynamic)step,
                Y = (dynamic)current.Y - (dynamic)step
            };
        }

        public IDirection<T, U> NextFacing(IAction<T, U> action)
        {
            if (action.GetType() == typeof(Left<T, U>))
            {
                return new NorthEast<T, U>();
            }
            else if (action.GetType() == typeof(Right<T, U>))
            {
                return new SouthWest<T, U>();
            }

            return this;
        }
    }
}
