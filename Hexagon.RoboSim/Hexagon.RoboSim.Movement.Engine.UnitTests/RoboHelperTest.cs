using FluentAssertions;
using Hexagon.RoboSim.Models.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Helper;
using Moq;
using System;
using Xunit;

namespace Hexagon.RoboSim.Movement.Engine.UnitTests
{
    public class RoboHelperTest
    {
        [Theory]
        [InlineData("MOVE", true)]
        [InlineData("LEFT", true)]
        [InlineData("RIGHT", true)]
        [InlineData("PLACE", true)]
        [InlineData("REPORT", false)]
        [InlineData("JUMP", false)]
        [InlineData("RUN", false)]
        public void RoboHelper_IsValidMoveAction_Should_Return_Correct_Response(string action, bool response)
        {
            var result = RoboHelper.IsValidMoveAction(action);

            result.Should().Be(response);
        }
        [Theory]
        [InlineData("REPORT", true)]
        [InlineData("JUMP", false)]
        [InlineData("MOVE", false)]
        public void RoboHelper_IsValidReportAction_Should_Return_Correct_Response(string action, bool response)
        {
            var result = RoboHelper.IsValidReportAction(action);

            result.Should().Be(response);
        }
        [Theory]
        [InlineData("NORTH", true)]
        [InlineData("SOUTH", true)]
        [InlineData("EAST", true)]
        [InlineData("WEST", true)]
        [InlineData("NORTHWEST", true)]
        [InlineData("NORTHEAST", true)]
        [InlineData("SOUTHWEST", true)]
        [InlineData("SOUTHEAST", true)]
        [InlineData("SOUTHNORTH", false)]
        [InlineData("NORTHSOUHT", false)]
        public void RoboHelper_IsValidDirection_Should_Return_Correct_Response(string direction, bool response)
        {
            var result = RoboHelper.IsValidDirection(direction);

            result.Should().Be(response);
        }
        [Theory]
        [InlineData(true, "MOVE","PLACE", "CHECK")]
        [InlineData(false, "MOVE", "PLCE", "CHECK")]
        [InlineData(true, "MOVE", "PLACE", "PLACE")]
        [InlineData(true, "MOVE", "palce", "place")]
        public void RoboHelper_IsPlaceExist_Should_Return_Correct_Response(bool response, params string[] commands)
        {
            var result = RoboHelper.IsPlaceExist(commands);

            result.Should().Be(response);
        }
        [Theory]
        [InlineData("PLACE 8,9,NORTH","PLACE", "8", "9", "NORTH")]
        [InlineData("PLACE 80,90,NORTH", "PLACE", "80", "90", "NORTH")]
        [InlineData("PLACE -1,-1,NORTH", "PLACE", "-1", "-1", "NORTH")]
        [InlineData("PLACE 0,0,NORTHWEST", "PLACE", "0", "0", "NORTHWEST")]
        public void RoboHelper_GetCommandSegments_Should_Return_Correct_Result(string command, string action, string x, string y, string direction)
        {
            var result = RoboHelper.GetCommandSegments(command);
            result.X.Should().Be(x);
            result.Y.Should().Be(y);
            result.Direction.Should().Be(direction);
            result.Action.Should().Be(action);
        }
        [Theory]
        [InlineData("", "Invalid command line arguments")]
        [InlineData("MOVE 8,9,NORTH", "PLACE command missing")]
        [InlineData("PLACE 8,9,MOVE", "Invalid direction")]
        public void RoboHelper_IsCommandLineArgumentsValid_Should_Throw_Correct_Exeption(string command, string exceptionMessage)
        {
            var commandSegments = RoboHelper.GetCommandSegments(command);
            Action act          = () => RoboHelper.IsCommandLineArgumentsValid(commandSegments);
            act.Should().Throw<ArgumentException>().WithMessage(exceptionMessage);

        }
        [Theory]
        [InlineData("PLACE 11,9,NORTHEAST", "Invalid X Position", 10, 10)]
        [InlineData("PLACE 8,11,NORTH", "Invalid Y Position", 10, 10)]
        [InlineData("PLACE -1,9,NORTHEAST", "Invalid X Position", 10, 10)]
        [InlineData("PLACE 8,-1,NORTH", "Invalid Y Position", 10, 10)]
        public void RoboHelper_IsCordinateValid_Should_Throw_Correct_Exeption(string command, string exceptionMessage, int height, int width)
        {
            var commandSegments = RoboHelper.GetCommandSegments(command);

            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(width);
            moveAreaMock.SetupGet(s => s.Height).Returns(height);

            Action act = () => RoboHelper.IsCordinateValid<int>(commandSegments, moveAreaMock.Object);
            act.Should().Throw<ArgumentException>().WithMessage(exceptionMessage);

        }
    }
}
