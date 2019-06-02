namespace Hexagon.RoboSim.Models.Constants
{
    public class DirectionConstants
    {
        public const string East        = "EAST";
        public const string West        = "WEST";
        public const string North       = "NORTH";
        public const string South       = "SOUTH";
        public const string SouthEast   = "SOUTHEAST";
        public const string SouthWest   = "SOUTHWEST";
        public const string NorthEast   = "NORTHEAST";
        public const string NorthWest   = "NORTHWEST";

        public static readonly string[] ValidDirections = { East, West, North, South, SouthEast, SouthWest, NorthEast, NorthWest };
    }
}
