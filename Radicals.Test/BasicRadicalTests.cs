using Rationals;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Radicals.Test
{
    public class BasicRadicalTests
    {
        [Fact]
        public void ConstructorTests()
        {
            // (3/4) * sqrt(12)
            // = (2*3/4) * sqrt(3)
            // = (3/2) * sqrt(3)
            var actual1 = new BasicRadical(new Rational(3, 4), 12);
            var expected1 = new BasicRadical(new Rational(3, 2), 3);
            // sqrt(2/9) = (1/3)sqrt(2)
            var actual2 = new BasicRadical(new Rational(2,9));
            var expected2 = new BasicRadical(new Rational(1, 3), 2);


            // assert
            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
        }

        [Fact]
        public void AdditionTests()
        {
            // sqrt(2) + sqrt(3)
            var b11 = new BasicRadical(1, 2);
            var b12 = new BasicRadical(1, 3);
            var actual1 = b11 + b12;
            var expected1 = new BasicRadical[2] { new BasicRadical(1, 2), new BasicRadical(1, 3) };
            // sqrt(2) + 2 * sqrt(2) = 3 * sqrt(2)
            var b21 = new BasicRadical(1, 2);
            var b22 = new BasicRadical(2, 2);
            var actual2 = b21 + b22;
            var expected2 = new BasicRadical[1] { new BasicRadical(3, 2) };
            // 5*sqrt(27) + 7*sqrt(12) = 15*sqrt(3) + 14*sqrt(3) = 29*sqrt(3)
            var b31 = new BasicRadical(5, 27);
            var b32 = new BasicRadical(7, 12);
            var actual3 = b31 + b32;
            var expected3 = new BasicRadical[1] { new BasicRadical(29, 3) };
            // 3*sqrt(2) + 2*sqrt(3)
            var b41 = new BasicRadical(3, 2);
            var b42 = new BasicRadical(2, 3);
            var actual4 = b41 + b42;
            var expected4 = new BasicRadical[2] { new BasicRadical(3, 2), new BasicRadical(2, 3) };

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual4);
        }

        [Fact]
        public void SubtractionTests()
        {
            // sqrt(2) + sqrt(3)
            var b11 = new BasicRadical(1, 2);
            var b12 = new BasicRadical(1, 3);
            var actual1 = b11 + b12;
            var expected1 = new BasicRadical[2] { new BasicRadical(1, 2), new BasicRadical(1, 3) };
            // sqrt(2) + 2 * sqrt(2) = 3 * sqrt(2)
            var b21 = new BasicRadical(1, 2);
            var b22 = new BasicRadical(2, 2);
            var actual2 = b21 + b22;
            var expected2 = new BasicRadical[1] { new BasicRadical(3, 2) };
            // 5*sqrt(27) + 7*sqrt(12) = 15*sqrt(3) + 14*sqrt(3) = 29*sqrt(3)
            var b31 = new BasicRadical(5, 27);
            var b32 = new BasicRadical(7, 12);
            var actual3 = b31 + b32;
            var expected3 = new BasicRadical[1] { new BasicRadical(29, 3) };
            // 3*sqrt(2) + 2*sqrt(3)
            var b41 = new BasicRadical(3, 2);
            var b42 = new BasicRadical(2, 3);
            var actual4 = b41 + b42;
            var expected4 = new BasicRadical[2] { new BasicRadical(3, 2), new BasicRadical(2, 3) };

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual4);
        }
    }
}
