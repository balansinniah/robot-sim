using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Models.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Actions;
using Hexagon.RoboSim.Movement.Engine.Factories;
using Hexagon.RoboSim.Movement.Engine.Helper;
using System;

namespace Hexagon.RoboSim.Movement.Engine
{
    public class Robo<T,U> : IRobo<T,U>
    {
        public Robo(IMoveArea<T> moveArea)
        {
            MoveArea = moveArea;
        }
        public Cordinate<T> Position { get; set ; }
        public U Step { get ; set ; }

        public IDirection<T,U> Direction { get; set; }
        public IMoveArea<T> MoveArea { get; set; }


        public void Initialize(string command)
        {
            ProcessInitialPlacement(command);
        }

        public string ProcessAction(string command)
        {
            var commandSegments = RoboHelper.GetCommandSegments(command);

            //check
            if(commandSegments != null)
            {
                //this will throw exception if the commads are invalid
                //This is done this way so that the detail error messages are captured for ease of user
                RoboHelper.IsCommandLineArgumentsValid(commandSegments);

                //Check if the original cordinate is valid
                RoboHelper.IsCordinateValid<T>(commandSegments, MoveArea);

                //set the new position
                Position = new Cordinate<T>
                {
                    X = (T)Convert.ChangeType(commandSegments.X, typeof(T)),
                    Y = (T)Convert.ChangeType(commandSegments.Y, typeof(T))
                };
                //set the new direction
                Direction = DirectionFactory<T, U>.Create(commandSegments.Direction);
            }
            else
            {
                if(RoboHelper.IsValidReportAction(command))
                {
                    return new Report<T, U>().GetOutput(Position, Direction);
                }

                if(RoboHelper.IsPlaceExist(new string[] { command})) throw new ArgumentException($"PLACE command missing arguments");

                if (!RoboHelper.IsValidMoveAction(command)) throw new ArgumentException($"Invalid command line arguments");

                if (Direction == null) throw new InvalidOperationException("Robo unable to perform this command without initial direction");

                if (Position == null) throw new InvalidOperationException("Robo unable to perform this command without initial position");

                //create the action
                var action      = ActionFactory<T, U>.Create(command);
                //get the new direction
                Direction = Direction.NextFacing(action);
                //calculate the new position
                Position        = action.ComputeNewCordinate(Position, Step, MoveArea, Direction);
            }

            return string.Empty;
        }

        private void ProcessInitialPlacement(string command)
        {
            var commandSegments = RoboHelper.GetCommandSegments(command);
            //this will throw exception if the commads are invalid
            //This is done this way so that the detail error messages are captured for ease of user
            RoboHelper.IsCommandLineArgumentsValid(commandSegments);

            //Check if the original cordinate is valid
            RoboHelper.IsCordinateValid<T>(commandSegments, MoveArea);

            //set the initial position
            Position = new Cordinate<T>
            {
                X = (T)Convert.ChangeType(commandSegments.X, typeof(T)),
                Y = (T)Convert.ChangeType(commandSegments.Y, typeof(T))
            };
            //set the initial direction
            Direction = DirectionFactory<T,U>.Create(commandSegments.Direction);
        }
    }
}
