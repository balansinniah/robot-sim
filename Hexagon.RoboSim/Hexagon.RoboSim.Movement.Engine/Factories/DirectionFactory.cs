using Hexagon.RoboSim.Models.Constants;
using Hexagon.RoboSim.Movement.Engine.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Directions;
using System;

namespace Hexagon.RoboSim.Movement.Engine.Factories
{
    public static class DirectionFactory<T,U>
    {
        public static IDirection<T, U> Create(string direction)
        {
            switch(direction.ToUpper())
            {
                case DirectionConstants.East:
                    return Create<East<T, U>>();
                case DirectionConstants.West:
                    return Create<West<T, U>>();
                case DirectionConstants.North:
                    return Create<North<T, U>>();
                case DirectionConstants.South:
                    return Create<South<T, U>>();
                case DirectionConstants.SouthEast:
                    return Create<SouthEast<T, U>>();
                case DirectionConstants.NorthEast:
                    return Create<NorthEast<T, U>>();
                case DirectionConstants.SouthWest:
                    return Create<SouthWest<T, U>>();
                case DirectionConstants.NorthWest:
                    return Create<NorthWest<T, U>>();
                default:
                    throw new ArgumentException("Invalid Direction to create from factory");
            }
        }
        public static IDirection<T,U> Create<P>() where P : IDirection<T,U>, new()
        {
            return new P();
        }
    }
}
