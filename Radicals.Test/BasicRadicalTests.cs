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
            // sqrt(1/2) = (1/2)sqrt(2)
            var actual3 = new BasicRadical(new Rational(1, 2));
            var expected3 = new BasicRadical(new Rational(1, 2), 2);


            // assert
            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
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
            // 2*sqrt(2) + 5*sqrt(28) + sqrt(1/2) + 3 + sqrt(7/9) + 11*sqrt(4) = 25 + (5/2)sqrt(2) + (31/3)*sqrt(7)
            var b51 = new BasicRadical(2, 2);
            var b52 = new BasicRadical(5, 28);
            var b53 = new BasicRadical(new Rational(1, 2));
            var b54 = new BasicRadical(3, 1);
            var b55 = new BasicRadical(new Rational(7, 9));
            var b56 = new BasicRadical(11, 4);
            var actual5 = b51 + b52 + b53 + b54 + b55 + b56;
            var expected5 = new BasicRadical[3] {
                new BasicRadical(25, 1),
                new BasicRadical(new Rational(5,2),2),
                new BasicRadical(new Rational(31,3),7)
            };

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual4);
            Assert.Equal(expected5, actual5);
        }

        [Fact]
        public void SubtractionTests()
        {
            // sqrt(2) - sqrt(3)
            var b11 = new BasicRadical(1, 2);
            var b12 = new BasicRadical(1, 3);
            var actual1 = b11 - b12;
            var expected1 = new BasicRadical[2] { new BasicRadical(1, 2), new BasicRadical(-1, 3) };
            // sqrt(2) - 2 * sqrt(2) = -1 * sqrt(2)
            var b21 = new BasicRadical(1, 2);
            var b22 = new BasicRadical(2, 2);
            var actual2 = b21 - b22;
            var expected2 = new BasicRadical[1] { new BasicRadical(-1, 2) };
            // 5*sqrt(27) - 7*sqrt(12) = 15*sqrt(3) - 14*sqrt(3) = 1*sqrt(3)
            var b31 = new BasicRadical(5, 27);
            var b32 = new BasicRadical(7, 12);
            var actual3 = b31 - b32;
            var expected3 = new BasicRadical[1] { new BasicRadical(1, 3) };
            // 3*sqrt(2) - 2*sqrt(3)
            var b41 = new BasicRadical(3, 2);
            var b42 = new BasicRadical(2, 3);
            var actual4 = b41 - b42;
            var expected4 = new BasicRadical[2] { new BasicRadical(3, 2), new BasicRadical(-2, 3) };
            // 2*sqrt(2) + 5*sqrt(28) - sqrt(1/2) + 3 - sqrt(7/9) + 11*sqrt(4) = 25 + (3/2)sqrt(2) + (29/3)*sqrt(7)
            var b51 = new BasicRadical(2, 2);
            var b52 = new BasicRadical(5, 28);
            var b53 = new BasicRadical(new Rational(1, 2));
            var b54 = new BasicRadical(3, 1);
            var b55 = new BasicRadical(new Rational(7, 9));
            var b56 = new BasicRadical(11, 4);
            var actual5 = b51 + b52 - b53 + b54 - b55 + b56;
            var expected5 = new BasicRadical[3] {
                new BasicRadical(25, 1),
                new BasicRadical(new Rational(3,2),2),
                new BasicRadical(new Rational(29,3),7)
            };

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual4);
            Assert.Equal(expected5, actual5);
        }
    }
}
