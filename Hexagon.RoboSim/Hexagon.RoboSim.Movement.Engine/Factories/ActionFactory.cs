using Hexagon.RoboSim.Models.Constants;
using Hexagon.RoboSim.Movement.Engine.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Actions;
using System;

namespace Hexagon.RoboSim.Movement.Engine.Factories
{
    public static class ActionFactory<T, U>
    {
        public static IAction<T, U> Create(string action)
        {
            switch (action.ToUpper())
            {
                case ActionConstants.Left:
                    return Create<Left<T, U>>();
                case ActionConstants.Right:
                    return Create<Right<T, U>>();
                case ActionConstants.Move:
                    return Create<Move<T, U>>();
                case ActionConstants.Place:
                    return Create<Place<T, U>>();
                default:
                    throw new ArgumentException("Invalid Action to create from factory");
            }
        }
        public static IAction<T, U> Create<P>() where P : IAction<T, U>, new()
        {
            return new P();
        }
    }
}
