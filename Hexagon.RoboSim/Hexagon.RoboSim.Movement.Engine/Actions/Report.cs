using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Movement.Engine.Abstractions;

namespace Hexagon.RoboSim.Movement.Engine.Actions
{
    public class Report<T, U>
    {
        public string GetOutput(Cordinate<T> cordinate,  IDirection<T, U> direction)
        {
            string name         = direction.GetType().Name;
            int index           = name.IndexOf('`');
            var directionName   = index == -1 ? name : name.Substring(0, index);

            return $"{cordinate.X},{cordinate.Y},{directionName.ToUpper()}";
        }
    }
}
