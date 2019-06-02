namespace Hexagon.RoboSim.Services.Abstractions
{
    public interface IMovementService<T, U>
    {
        void Initialize(string command);
        string ProcessCommand(string command);
    }
}
