namespace Hexagon.RoboSim.Models.Abstractions
{
    public interface IMoveArea<T>
    {
        T Height { get; set; }
        T Width { get; set; }
    }
}
