using FluentAssertions;
using Hexagon.RoboSim.Models.Abstractions;
using Hexagon.RoboSim.Movement.Engine.Factories;
using Moq;
using System;
using Xunit;

namespace Hexagon.RoboSim.Movement.Engine.UnitTests
{
    public class RoboTest
    {
        [Theory]
        [InlineData("PLACE 0,0,SOUTHWEST", 0, 0, "SOUTHWEST")]
        public void Robo_Process_Action_Should_Return_Correct_Result(string command, int x, int y, string direction)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var robo = new Robo<int, int>(moveAreaMock.Object);
            robo.ProcessAction(command);

            var expetedDirection = DirectionFactory<int, int>.Create(direction);

            robo.Direction.GetType().Should().BeSameAs(expetedDirection.GetType());
            robo.Position.X.Should().Be(x);
            robo.Position.Y.Should().Be(y);
        }
        [Theory]
        [InlineData("JUMP", "Invalid command line arguments")]
        [InlineData("MOVE 0,0,SOUTHWEST", "PLACE command missing")]
        [InlineData("PLACE", "PLACE command missing arguments")]
        public void Robo_Process_Action_Should_Throw_ArgumentException_Exception(string command, string exceptionMessage)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var robo = new Robo<int, int>(moveAreaMock.Object);

            Action act = () => robo.ProcessAction(command);
            act.Should().Throw<ArgumentException>().WithMessage(exceptionMessage);
        }
        [Theory]
        [InlineData("PLACE 0,0,SOUTHWEST", 0, 0, "SOUTHWEST")]
        public void Robo_Initialize_Should_Return_Correct_Result(string command, int x, int y, string direction)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var robo = new Robo<int, int>(moveAreaMock.Object);
            robo.Initialize(command);

            var expetedDirection = DirectionFactory<int, int>.Create(direction);

            robo.Direction.GetType().Should().BeSameAs(expetedDirection.GetType());
            robo.Position.X.Should().Be(x);
            robo.Position.Y.Should().Be(y);
        }
        [Theory]
        [InlineData("JUMP", "Invalid command line arguments")]
        [InlineData("MOVE 0,0,SOUTHWEST", "PLACE command missing")]
        public void Robo_Initialize_Should_Throw_Exception(string command, string exceptionMessage)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var robo = new Robo<int, int>(moveAreaMock.Object);

            Action act = () => robo.Initialize(command);
            act.Should().Throw<ArgumentException>().WithMessage(exceptionMessage);
        }
    }
}
