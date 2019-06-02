using FluentAssertions;
using Hexagon.RoboSim.Models.Abstractions;
using Hexagon.RoboSim.Movement.Engine;
using Hexagon.RoboSim.Movement.Engine.Factories;
using Moq;
using System;
using Xunit;

namespace Hexagon.RoboSim.Services.UnitTests
{
    public class MovementServiceTest
    {
        [Theory]
        [InlineData("PLACE 0,0,NORTH", 0, 0, "NORTH")]
        [InlineData("PLACE 5,5,SOUTH", 5, 5, "SOUTH")]
        public void Movement_Service_Initialize_Should_Initialize_Correctly(string command, int x, int y, string direction)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);
            var robo                = new Robo<int, int>(moveAreaMock.Object);
            var service             = new MovementService<int, int>(robo);
            var expectedDirection   = DirectionFactory<int, int>.Create(direction);

            service.Initialize(command);

            robo.Direction.GetType().Should().BeSameAs(expectedDirection.GetType());
            robo.Position.X.Should().Be(x);
            robo.Position.Y.Should().Be(y);
        }
        [Theory]
        [InlineData("MOVE", "Robo unable to perform this command without initial direction")]
        public void Movement_Service_Start_Without_Initialize_Should_Throw_Exceptions(string command, string exceptionMessage)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);
            var robo                = new Robo<int, int>(moveAreaMock.Object);
            var service             = new MovementService<int, int>(robo);

            Action act = () => service.ProcessCommand(command);
            act.Should().Throw<InvalidOperationException>().WithMessage(exceptionMessage);
        }

        [Fact]
        public void Movement_Service_Start_With_Initialize_Should_Return_Correct_Result1()
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);
            var robo    = new Robo<int, int>(moveAreaMock.Object);
            var service = new MovementService<int, int>(robo);

            robo.Step = 1;

            service.Initialize("PLACE 0,0,NORTH");
            service.ProcessCommand("MOVE");

            var expectedDirection = DirectionFactory<int, int>.Create("NORTH");

            robo.Direction.GetType().Should().BeSameAs(expectedDirection.GetType());
            robo.Position.X.Should().Be(0);
            robo.Position.Y.Should().Be(1);
        }
        [Fact]
        public void Movement_Service_Start_With_Initialize_Should_Return_Correct_Result2()
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);
            var robo    = new Robo<int, int>(moveAreaMock.Object);
            var service = new MovementService<int, int>(robo);

            robo.Step = 1;

            service.Initialize("PLACE 1,2,EAST");
            service.ProcessCommand("MOVE");
            service.ProcessCommand("MOVE");
            service.ProcessCommand("LEFT");
            service.ProcessCommand("MOVE");

            var expectedDirection = DirectionFactory<int, int>.Create("NORTH");

            robo.Direction.GetType().Should().BeSameAs(expectedDirection.GetType());
            robo.Position.X.Should().Be(3);
            robo.Position.Y.Should().Be(3);

            service.ProcessCommand("MOVE");
            robo.Position.X.Should().Be(3);
            robo.Position.Y.Should().Be(4);
        }
    }
}
