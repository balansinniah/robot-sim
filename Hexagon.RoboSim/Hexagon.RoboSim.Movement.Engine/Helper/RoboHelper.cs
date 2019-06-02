using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Models.Abstractions;
using Hexagon.RoboSim.Models.Constants;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hexagon.RoboSim.Movement.Engine.Helper
{
    public static class RoboHelper
    {
        public static bool IsValidMoveAction(string action)
        {
            return ActionConstants.MoveActions.Contains(action.ToUpper());
        }
        public static bool IsValidReportAction(string action)
        {
            return ActionConstants.ReportActions.Contains(action.ToUpper());
        }
        public static bool IsValidDirection(string direction)
        {
            return DirectionConstants.ValidDirections.Contains(direction.ToUpper());
        }

        public static bool IsPlaceExist(string[] commands)
        {
            return commands.Select(s => s.ToUpperInvariant()).ToArray().Contains(ActionConstants.Place);
        }
        public static RoboPlacementCommandSegment GetCommandSegments(string command)
        {
            var regex = new Regex(RegexConstants.InitialPlacemenRegex);
            var match = regex.Match(command);
            if (!match.Success)
            {
                return null;
            }

            return new RoboPlacementCommandSegment
            {
                Action      = match.Groups[1].Value,
                Direction   = match.Groups[7].Value,
                X           = match.Groups[3].Value,
                Y           = match.Groups[5].Value
            };
        }

        public static void IsCommandLineArgumentsValid(RoboPlacementCommandSegment commandSegments)
        {
            if (commandSegments == null) throw new ArgumentException($"Invalid command line arguments");

            if (commandSegments.Action.ToUpper() != ActionConstants.Place) throw new ArgumentException($"PLACE command missing");

           if (!IsValidDirection(commandSegments.Direction)) throw new ArgumentException($"Invalid direction");
        }

        public static void IsCordinateValid<T>(RoboPlacementCommandSegment commandSegments, IMoveArea<T> moveArea)
        {
            var coordX = (T)Convert.ChangeType(commandSegments.X, typeof(T));
            var coordY = (T)Convert.ChangeType(commandSegments.Y, typeof(T));

            if ((dynamic)coordX < 0) throw new ArgumentException($"Invalid X Position");

            if ((dynamic)coordY < 0) throw new ArgumentException($"Invalid Y Position");

            if((dynamic)coordX > (dynamic)moveArea.Width) throw new ArgumentException($"Invalid X Position");

            if ((dynamic)coordY > (dynamic)moveArea.Height) throw new ArgumentException($"Invalid Y Position");
        }
    }
}
