using Demo;
using System;
using Xunit;

namespace DemoTests
{
    public class CalculatorTests
    {
        [Fact]
        public void ShouldAdd()
        {
            // Arrange
            var sut = new Calculator();  // sut - System Under Test

            // Act
            var result = sut.Add(x: 2, y: 3);

            // Assert
            Assert.Equal(expected: 5, actual: result);
        }
    }
}
