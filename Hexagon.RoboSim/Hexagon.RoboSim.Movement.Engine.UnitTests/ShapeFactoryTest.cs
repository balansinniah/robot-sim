using FluentAssertions;
using Hexagon.RoboSim.Models.ModelFactories;
using Hexagon.RoboSim.Models.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hexagon.RoboSim.Movement.Engine.UnitTests
{
    public class ShapeFactoryTest
    {
        [Theory]
        [InlineData("rectangle", typeof(Rectangle<int>))]
        [InlineData("square", typeof(Square<int>))]
        [InlineData("SQUARE", typeof(Square<int>))]
        [InlineData("RECTANGLE", typeof(Rectangle<int>))]
        public void Shape_Factory_Should_Create_Correct_Shape_Object(string shape, Type type)
        {
            var result = ShapeFactory<int>.Create(shape);

            result.Should().BeOfType(type);
        }
        [Theory]
        [InlineData("Circle", "Shape not valid")]
        [InlineData("Hexagon", "Shape not valid")]
        public void Shape_Factory_Should_Throw_ArgumentException_Exception(string shape, string exceptionMessage)
        {
            Action act = () => ShapeFactory<int>.Create(shape);
            act.Should().Throw<ArgumentException>().WithMessage(exceptionMessage);
        }
    }
}
