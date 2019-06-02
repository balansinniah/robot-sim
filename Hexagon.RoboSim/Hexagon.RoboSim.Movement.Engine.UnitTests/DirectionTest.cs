using FluentAssertions;
using Hexagon.RoboSim.Models;
using Hexagon.RoboSim.Models.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Directions;
using Hexagon.RoboSim.Movement.Engine.Factories;
using Moq;
using Xunit;

namespace Hexagon.RoboSim.Movement.Engine.UnitTests
{
    public class DirectionTest
    {
        [Theory]
        [InlineData("NORTH","LEFT", "WEST")]
        [InlineData("NORTH", "RIGHT", "EAST")]
        [InlineData("SOUTH", "LEFT", "EAST")]
        [InlineData("SOUTH", "RIGHT", "WEST")]
        [InlineData("EAST", "LEFT", "NORTH")]
        [InlineData("EAST", "RIGHT", "SOUTH")]
        [InlineData("WEST", "LEFT", "SOUTH")]
        [InlineData("WEST", "RIGHT", "NORTH")]
        [InlineData("NORTHWEST", "RIGHT", "NORTHEAST")]
        [InlineData("NORTHWEST", "LEFT", "SOUTHWEST")]
        [InlineData("SOUTHWEST", "RIGHT", "NORTHWEST")]
        [InlineData("SOUTHWEST", "LEFT", "SOUTHEAST")]
        [InlineData("NORTHEAST", "RIGHT", "SOUTHEAST")]
        [InlineData("NORTHEAST", "LEFT", "NORTHWEST")]
        [InlineData("SOUTHEAST", "RIGHT", "SOUTHWEST")]
        [InlineData("SOUTHEAST", "LEFT", "NORTHEAST")]
        public void Action_Compute_Cordinate_Should_Return_Same_Cordinate(string currentDirection , string command ,string expectedDirection)
        {
            var currDirection   = DirectionFactory<int, int>.Create(currentDirection);
            var action          = ActionFactory<int, int>.Create(command);
            var expectDirection = DirectionFactory<int, int>.Create(expectedDirection);

            var result          = currDirection.NextFacing(action);

            result.GetType().Should().BeSameAs(expectDirection.GetType());
        }
    }
}
