using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Movement.Engine.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Actions;

namespace Hexagon.RoboSim.Movement.Engine.Directions
{
    public class NorthEast<T, U> : IDirection<T, U>
    {
        public Cordinate<T> Move(Cordinate<T> current, U step, T xLimit, T yLimit)
        {
            if ((dynamic)current.X >= xLimit && (dynamic)current.Y >= yLimit) return current;

            if ((dynamic)current.X >= xLimit && (dynamic)current.Y < yLimit) return current;

            if ((dynamic)current.X < xLimit && (dynamic)current.Y >= yLimit) return current;

            return new Cordinate<T>
            {
                X = (dynamic)current.X + (dynamic)step,
                Y = (dynamic)current.Y + (dynamic)step
            };
        }
        public IDirection<T, U> NextFacing(IAction<T, U> action)
        {
            if (action.GetType() == typeof(Left<T, U>))
            {
                return new NorthWest<T, U>();
            }
            else if (action.GetType() == typeof(Right<T, U>))
            {
                return new SouthEast<T, U>();
            }

            return this;
        }
    }
}
