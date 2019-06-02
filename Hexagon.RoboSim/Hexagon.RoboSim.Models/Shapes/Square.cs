using Hexagon.RoboSim.Models.Abstractions;

namespace Hexagon.RoboSim.Models.Shapes
{
    public class Square<T> : IMoveArea<T>
    {
        public T Height { get; set; }
        public T Width { get; set; }
    }
}
