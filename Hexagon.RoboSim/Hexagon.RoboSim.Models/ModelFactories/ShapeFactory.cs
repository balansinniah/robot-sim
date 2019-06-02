using Hexagon.RoboSim.Models.Abstractions;
using Hexagon.RoboSim.Models.Constants;
using Hexagon.RoboSim.Models.Shapes;
using System;

namespace Hexagon.RoboSim.Models.ModelFactories
{
    public static class ShapeFactory<T>
    {
        public static IMoveArea<T> Create(string shape)
        {
            switch (shape.ToUpper())
            {
                case ShapeConstants.Rectangle:
                    return Create<Rectangle<T>>();
                case ShapeConstants.Square:
                    return Create<Square<T>>();
                default:
                    throw new ArgumentException("Shape not valid");
            }
        }
        public static IMoveArea<T> Create<P>() where P : IMoveArea<T>, new()
        {
            return new P();
        }
    }
}
