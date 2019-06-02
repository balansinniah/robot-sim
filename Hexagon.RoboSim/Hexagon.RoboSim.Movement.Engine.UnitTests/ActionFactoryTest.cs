using FluentAssertions;
using Hexagon.RoboSim.Movement.Engine.Actions;
using Hexagon.RoboSim.Movement.Engine.Factories;
using System;
using Xunit;

namespace Hexagon.RoboSim.Movement.Engine.UnitTests
{
    public class ActionFactoryTest
    {
        [Theory]
        [InlineData("MOVE", typeof(Move<int,int>))]
        [InlineData("LEFT", typeof(Left<int, int>))]
        [InlineData("PLACE", typeof(Place<int, int>))]
        [InlineData("RIGHT", typeof(Right<int, int>))]
        [InlineData("move", typeof(Move<int, int>))]
        [InlineData("left", typeof(Left<int, int>))]
        [InlineData("place", typeof(Place<int, int>))]
        [InlineData("right", typeof(Right<int, int>))]
        public void Action_Factory_Should_Create_Correct_Action_Object(string command, Type type)
        {
            var action = ActionFactory<int,int>.Create(command);

            action.Should().BeOfType(type);
        }
        [Theory]
        [InlineData("Jump", "Invalid Action to create from factory")]
        [InlineData("run", "Invalid Action to create from factory")]
        public void Shape_Factory_Should_Throw_ArgumentException_Exception(string command, string exceptionMessage)
        {
            Action act = () => ActionFactory<int, int>.Create(command);
            act.Should().Throw<ArgumentException>().WithMessage(exceptionMessage);
        }
    }
}
