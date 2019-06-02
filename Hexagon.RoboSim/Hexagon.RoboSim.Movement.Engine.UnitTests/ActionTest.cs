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
    public class ActionTest
    {

        [Theory]
        [InlineData("LEFT", 10, 10)]
        [InlineData("RIGHT", 10, 10)]
        [InlineData("PLACE", 10, 10)]
        [InlineData("REPORT", 10, 10)]
        public void Action_Compute_Cordinate_Should_Return_Same_Cordinate(string command , int x, int y)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(It.IsAny<int>);
            moveAreaMock.SetupGet(s => s.Height).Returns(It.IsAny<int>);

            var directionMock = new Mock<IDirection<int, int>>();
            directionMock.Setup(s => s.NextFacing(It.IsAny<IAction<int, int>>())).Returns(new East<int, int>());

            var action      = ActionFactory<int, int>.Create(command);
            var cordinate   = new Cordinate<int>
            {
                X = x,
                Y = y
            };
            var result = action.ComputeNewCordinate(cordinate, 1, moveAreaMock.Object, directionMock.Object);

            result.Should().BeSameAs(cordinate);
        }

        [Theory]
        [InlineData("MOVE", "WEST", 0, 0, 0, 0, 1)]
        [InlineData("MOVE", "WEST", 10, 10, 9, 10, 1)]
        [InlineData("MOVE", "WEST", 0, 10, 0, 10, 1)]
        [InlineData("MOVE", "WEST", 10, 0, 9, 0, 1)]
        [InlineData("MOVE", "WEST", 5, 0, 4, 0, 1)]
        [InlineData("MOVE", "WEST", 0, 5, 0, 5, 1)]
        [InlineData("MOVE", "WEST", 5, 10, 4, 10, 1)]
        [InlineData("MOVE", "WEST", 10, 5, 9, 5, 1)]
        [InlineData("MOVE", "WEST", 5, 6, 4, 6, 1)]
        [InlineData("MOVE", "WEST", 1, 9, 0, 9, 1)]
        public void Horizontal_West_Move_Action_Compute_Cordinate_Should_Return_Correct_New_Cordinate(string command, string directi, int x, int y, int exX, int exY, int step)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var direction   = DirectionFactory<int, int>.Create(directi);
            var action      = ActionFactory<int, int>.Create(command);
            var cordinate   = new Cordinate<int>
            {
                X = x,
                Y = y
            };

            var result = action.ComputeNewCordinate(cordinate, step, moveAreaMock.Object, direction);

            result.X.Should().Be(exX);
            result.Y.Should().Be(exY);
        }
        [Theory]
        [InlineData("MOVE", "EAST", 0, 0, 1, 0, 1)]
        [InlineData("MOVE", "EAST", 10, 10, 10, 10, 1)]
        [InlineData("MOVE", "EAST", 0, 10, 1, 10, 1)]
        [InlineData("MOVE", "EAST", 10, 0, 10, 0, 1)]
        [InlineData("MOVE", "EAST", 5, 0, 6, 0, 1)]
        [InlineData("MOVE", "EAST", 0, 5, 1, 5, 1)]
        [InlineData("MOVE", "EAST", 5, 10, 6, 10, 1)]
        [InlineData("MOVE", "EAST", 10, 5, 10, 5, 1)]
        [InlineData("MOVE", "EAST", 5, 6, 6, 6, 1)]
        [InlineData("MOVE", "EAST", 1, 9, 2, 9, 1)]
        public void Horizontal_East_Move_Action_Compute_Cordinate_Should_Return_Correct_New_Cordinate(string command, string directi, int x, int y, int exX, int exY, int step)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var direction   = DirectionFactory<int, int>.Create(directi);
            var action      = ActionFactory<int, int>.Create(command);
            var cordinate   = new Cordinate<int>
            {
                X = x,
                Y = y
            };

            var result = action.ComputeNewCordinate(cordinate, step, moveAreaMock.Object, direction);

            result.X.Should().Be(exX);
            result.Y.Should().Be(exY);
        }

        [Theory]
        [InlineData("MOVE", "NORTH", 0, 0, 0, 1, 1)]
        [InlineData("MOVE", "NORTH", 10, 10, 10, 10, 1)]
        [InlineData("MOVE", "NORTH", 0, 10, 0, 10, 1)]
        [InlineData("MOVE", "NORTH", 10, 0, 10, 1, 1)]
        [InlineData("MOVE", "NORTH", 5, 0, 5, 1, 1)]
        [InlineData("MOVE", "NORTH", 0, 5, 0, 6, 1)]
        [InlineData("MOVE", "NORTH", 5, 10, 5, 10, 1)]
        [InlineData("MOVE", "NORTH", 10, 5, 10, 6, 1)]
        [InlineData("MOVE", "NORTH", 5, 6, 5, 7, 1)]
        [InlineData("MOVE", "NORTH", 1, 9, 1, 10, 1)]
        public void Vertical_North_Move_Action_Compute_Cordinate_Should_Return_Correct_New_Cordinate(string command, string directi, int x, int y, int exX, int exY, int step)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var direction   = DirectionFactory<int, int>.Create(directi);
            var action      = ActionFactory<int, int>.Create(command);
            var cordinate   = new Cordinate<int>
            {
                X = x,
                Y = y
            };

            var result = action.ComputeNewCordinate(cordinate, step, moveAreaMock.Object, direction);

            result.X.Should().Be(exX);
            result.Y.Should().Be(exY);
        }
        [Theory]
        [InlineData("MOVE", "SOUTH", 0, 0, 0, 0, 1)]
        [InlineData("MOVE", "SOUTH", 10, 10, 10, 9, 1)]
        [InlineData("MOVE", "SOUTH", 0, 10, 0, 9, 1)]
        [InlineData("MOVE", "SOUTH", 10, 0, 10, 0, 1)]
        [InlineData("MOVE", "SOUTH", 5, 0, 5, 0, 1)]
        [InlineData("MOVE", "SOUTH", 0, 5, 0, 4, 1)]
        [InlineData("MOVE", "SOUTH", 5, 10, 5, 9, 1)]
        [InlineData("MOVE", "SOUTH", 10, 5, 10, 4, 1)]
        [InlineData("MOVE", "SOUTH", 5, 6, 5, 5, 1)]
        [InlineData("MOVE", "SOUTH", 1, 9, 1, 8, 1)]
        public void Vertical_South_Move_Action_Compute_Cordinate_Should_Return_Correct_New_Cordinate(string command, string directi, int x, int y, int exX, int exY, int step)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var direction   = DirectionFactory<int, int>.Create(directi);
            var action      = ActionFactory<int, int>.Create(command);
            var cordinate   = new Cordinate<int>
            {
                X = x,
                Y = y
            };

            var result = action.ComputeNewCordinate(cordinate, step, moveAreaMock.Object, direction);

            result.X.Should().Be(exX);
            result.Y.Should().Be(exY);
        }
        [Theory]
        [InlineData("MOVE", "NORTHEAST", 0, 0, 1, 1, 1)]
        [InlineData("MOVE", "NORTHEAST", 10, 10, 10, 10, 1)]
        [InlineData("MOVE", "NORTHEAST", 0, 10, 0, 10, 1)]
        [InlineData("MOVE", "NORTHEAST", 10, 0, 10, 0, 1)]
        [InlineData("MOVE", "NORTHEAST", 5, 0, 6, 1, 1)]
        [InlineData("MOVE", "NORTHEAST", 0, 5, 1, 6, 1)]
        [InlineData("MOVE", "NORTHEAST", 5, 10, 5, 10, 1)]
        [InlineData("MOVE", "NORTHEAST", 10, 5, 10, 5, 1)]
        [InlineData("MOVE", "NORTHEAST", 5, 6, 6, 7, 1)]
        [InlineData("MOVE", "NORTHEAST", 1, 9, 2, 10, 1)]
        public void Diagonal_NorthEast_Move_Action_Compute_Cordinate_Should_Return_Correct_New_Cordinate(string command, string directi, int x, int y, int exX, int exY, int step)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var direction   = DirectionFactory<int, int>.Create(directi);
            var action      = ActionFactory<int, int>.Create(command);
            var cordinate   = new Cordinate<int>
            {
                X = x,
                Y = y
            };

            var result = action.ComputeNewCordinate(cordinate, step, moveAreaMock.Object, direction);

            result.X.Should().Be(exX);
            result.Y.Should().Be(exY);
        }
        [Theory]
        [InlineData("MOVE", "NORTHWEST", 0, 0, 0, 0, 1)]
        [InlineData("MOVE", "NORTHWEST", 10, 10, 10, 10, 1)]
        [InlineData("MOVE", "NORTHWEST", 0, 10, 0, 10, 1)]
        [InlineData("MOVE", "NORTHWEST", 10, 0, 9, 1, 1)]
        [InlineData("MOVE", "NORTHWEST", 5, 0, 4, 1, 1)]
        [InlineData("MOVE", "NORTHWEST", 0, 5, 0, 5, 1)]
        [InlineData("MOVE", "NORTHWEST", 5, 10, 5, 10, 1)]
        [InlineData("MOVE", "NORTHWEST", 10, 5, 9, 6, 1)]
        [InlineData("MOVE", "NORTHWEST", 5, 6, 4, 7, 1)]
        [InlineData("MOVE", "NORTHWEST", 1, 9, 0, 10, 1)]
        public void Diagonal_NortWest_Move_Action_Compute_Cordinate_Should_Return_Correct_New_Cordinate(string command, string directi, int x, int y, int exX, int exY, int step)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var direction   = DirectionFactory<int, int>.Create(directi);
            var action      = ActionFactory<int, int>.Create(command);
            var cordinate   = new Cordinate<int>
            {
                X = x,
                Y = y
            };

            var result = action.ComputeNewCordinate(cordinate, step, moveAreaMock.Object, direction);

            result.X.Should().Be(exX);
            result.Y.Should().Be(exY);
        }
        [Theory]
        [InlineData("MOVE", "SOUTHEAST", 0, 0, 0, 0, 1)]
        [InlineData("MOVE", "SOUTHEAST", 10, 10, 10, 10, 1)]
        [InlineData("MOVE", "SOUTHEAST", 0, 10, 1, 9, 1)]
        [InlineData("MOVE", "SOUTHEAST", 10, 0, 10, 0, 1)]
        [InlineData("MOVE", "SOUTHEAST", 5, 0, 5, 0, 1)]
        [InlineData("MOVE", "SOUTHEAST", 0, 5, 1, 4, 1)]
        [InlineData("MOVE", "SOUTHEAST", 5, 10, 6, 9, 1)]
        [InlineData("MOVE", "SOUTHEAST", 10, 5, 10, 5, 1)]
        [InlineData("MOVE", "SOUTHEAST", 5, 6, 6, 5, 1)]
        [InlineData("MOVE", "SOUTHEAST", 1, 9, 2, 8, 1)]
        public void Diagonal_SouthEast_Move_Action_Compute_Cordinate_Should_Return_Correct_New_Cordinate(string command, string directi, int x, int y, int exX, int exY, int step)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var direction   = DirectionFactory<int, int>.Create(directi);
            var action      = ActionFactory<int, int>.Create(command);
            var cordinate   = new Cordinate<int>
            {
                X = x,
                Y = y
            };

            var result = action.ComputeNewCordinate(cordinate, step, moveAreaMock.Object, direction);

            result.X.Should().Be(exX);
            result.Y.Should().Be(exY);
        }
        [Theory]
        [InlineData("MOVE", "SOUTHWEST", 0, 0, 0, 0, 1)]
        [InlineData("MOVE", "SOUTHWEST", 10, 10, 9, 9, 1)]
        [InlineData("MOVE", "SOUTHWEST", 0, 10, 0, 10, 1)]
        [InlineData("MOVE", "SOUTHWEST", 10, 0, 10, 0, 1)]
        [InlineData("MOVE", "SOUTHWEST", 5, 0, 5, 0, 1)]
        [InlineData("MOVE", "SOUTHWEST", 0, 5, 0, 5, 1)]
        [InlineData("MOVE", "SOUTHWEST", 5, 10, 4, 9, 1)]
        [InlineData("MOVE", "SOUTHWEST", 10, 5, 9, 4, 1)]
        [InlineData("MOVE", "SOUTHWEST", 5, 6, 4, 5, 1)]
        [InlineData("MOVE", "SOUTHWEST", 1, 9, 0, 8, 1)]
        public void Diagonal_SouthWest_Move_Action_Compute_Cordinate_Should_Return_Correct_New_Cordinate(string command, string directi, int x, int y, int exX, int exY, int step)
        {
            var moveAreaMock = new Mock<IMoveArea<int>>();
            moveAreaMock.SetupGet(s => s.Width).Returns(10);
            moveAreaMock.SetupGet(s => s.Height).Returns(10);

            var direction   = DirectionFactory<int, int>.Create(directi);
            var action      = ActionFactory<int, int>.Create(command);
            var cordinate   = new Cordinate<int>
            {
                X = x,
                Y = y
            };

            var result = action.ComputeNewCordinate(cordinate, step, moveAreaMock.Object, direction);

            result.X.Should().Be(exX);
            result.Y.Should().Be(exY);
        }
    }
}
