using FluentAssertions;
using Hexagon.RoboSim.Movement.Engine.Directions;
using Hexagon.RoboSim.Movement.Engine.Factories;
using System;
using Xunit;

namespace Hexagon.RoboSim.Movement.Engine.UnitTests
{

    public class DirectionFactoryTest
    {
        [Theory]
        [InlineData("East", typeof(East<int, int>))]
        [InlineData("North", typeof(North<int, int>))]
        [InlineData("South", typeof(South<int, int>))]
        [InlineData("West", typeof(West<int, int>))]
        [InlineData("NorthEast", typeof(NorthEast<int, int>))]
        [InlineData("NorthWest", typeof(NorthWest<int, int>))]
        [InlineData("SouthEast", typeof(SouthEast<int, int>))]
        [InlineData("SouthWest", typeof(SouthWest<int, int>))]
        [InlineData("EAST", typeof(East<int, int>))]
        [InlineData("NORTH", typeof(North<int, int>))]
        [InlineData("SOUTH", typeof(South<int, int>))]
        [InlineData("WEST", typeof(West<int, int>))]
        [InlineData("NORTHEAST", typeof(NorthEast<int, int>))]
        [InlineData("NORTHWEST", typeof(NorthWest<int, int>))]
        [InlineData("SOUTHEAST", typeof(SouthEast<int, int>))]
        [InlineData("SOUTHWEST", typeof(SouthWest<int, int>))]
        public void Direction_Factory_Should_Create_Correct_Direction_Object(string command, Type type)
        {
            var direction = DirectionFactory<int, int>.Create(command);

            direction.Should().BeOfType(type);
        }

        [Theory]
        [InlineData("Jump", "Invalid Direction to create from factory")]
        [InlineData("run", "Invalid Direction to create from factory")]
        public void Direction_Factory_Should_Throw_ArgumentException_Exception(string command, string exceptionMessage)
        {
            Action act = () => DirectionFactory<int, int>.Create(command);
            act.Should().Throw<ArgumentException>().WithMessage(exceptionMessage);
        }
    }
}
