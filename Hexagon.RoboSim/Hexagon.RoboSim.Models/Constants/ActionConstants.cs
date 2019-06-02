namespace Hexagon.RoboSim.Models.Constants
{
    public  class ActionConstants
    {
        public const string Left    = "LEFT";
        public const string Right   = "RIGHT";
        public const string Move    = "MOVE";
        public const string Place   = "PLACE";
        public const string Report  = "REPORT";

        public static readonly string[] MoveActions = { Left, Right, Move, Place};
        public static readonly string[] ReportActions = { Report };
    }
}
