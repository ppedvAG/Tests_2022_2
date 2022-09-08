using System;
using Xunit;

namespace Calculator.Tests
{
    public class CalcTests
    {
        [Fact]
        public void Sum_3_and_4_results_7()
        {
            //Arrange
            Calc calc = new Calc();

            //Act
            int result = calc.Sum(3, 4);

            //Assert
            Assert.Equal(7, result);
        }

        [Fact]
        public void Sum_n3_and_n4_results_n7()
        {
            //Arrange
            Calc calc = new Calc();

            //Act
            int result = calc.Sum(-3, -4);

            //Assert
            Assert.Equal(-7, result);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 2, 3)]
        [InlineData(230, 140, 370)]
        [InlineData(-23, -14, -37)]
        [InlineData(-20, 40, 20)]
        public void Sum_all_ok(int a, int b, int exp)
        {
            //Arrange
            Calc calc = new Calc();

            //Act
            int result = calc.Sum(a, b);

            //Assert
            Assert.Equal(exp, result);
        }

        [Theory]
        [Trait("Category","Unittest")]
        [InlineData(int.MaxValue, 1)]
        [InlineData(1, int.MaxValue)]
        [InlineData(int.MinValue, -1)]
        [InlineData(-1, int.MinValue)]
        public void Sum_overflow_throws_OverFlowException(int a, int b)
        {
            Calc calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(a, b));
        }
    }
}
