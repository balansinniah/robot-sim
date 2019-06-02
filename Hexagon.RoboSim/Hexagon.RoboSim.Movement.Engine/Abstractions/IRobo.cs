using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Models.Abstractions;

namespace Hexagon.RoboSim.Movement.Engine.Abstractions
{
    public  interface IRobo<T,U>
   {
        Cordinate<T> Position { get; set; }
        U Step { get; set; }
        IDirection<T, U> Direction { get; set; }
        IMoveArea<T> MoveArea { get; set; }
        void Initialize(string command);
        string ProcessAction(string command);
   }
}
