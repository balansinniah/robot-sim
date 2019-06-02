﻿using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Movement.Engine.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Actions;

namespace Hexagon.RoboSim.Movement.Engine.Directions
{
    public class SouthWest<T, U> : IDirection<T, U>
    {
        public Cordinate<T> Move(Cordinate<T> current, U step, T xLimit, T yLimit)
        {
            if ((dynamic)current.X <= 0 && (dynamic)current.Y <= 0) return current;

            if ((dynamic)current.X <= 0 && (dynamic)current.Y > 0) return current;

            if ((dynamic)current.X > 0 && (dynamic)current.Y <= 0) return current;

            return new Cordinate<T>
            {
                X = (dynamic)current.X - (dynamic)step,
                Y = (dynamic)current.Y - (dynamic)step
            };
        }

        public IDirection<T, U> NextFacing(IAction<T, U> action)
        {
            if (action.GetType() == typeof(Left<T, U>))
            {
                return new SouthEast<T, U>();
            }
            else if (action.GetType() == typeof(Right<T, U>))
            {
                return new NorthWest<T, U>();
            }

            return this;
        }
    }
}
