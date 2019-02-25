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
        public void ConstructorTest()
        {
            // (3/4) * sqrt(12)
            // = (2*3/4) * sqrt(3)
            // = (3/2) * sqrt(3)
            var basicRadical = new BasicRadical(new Rational(3, 4), 12);

            // assert
            Assert.Equal(new BasicRadical(new Rational(3,2), 3), basicRadical);
        }

        [Fact]
        public void AddTest()
        {
            var b11 = new BasicRadical(1, 2);
            var b12 = new BasicRadical(1, 3);
            var combined1 = new BasicRadical[2] { b11, b12 };
            var b21 = new BasicRadical(1, 2);
            var b22 = new BasicRadical(2, 2);
            var combined2 = new BasicRadical[1] { new BasicRadical(3, 2) };

            Assert.Equal(combined1, b11 + b12);
            Assert.Equal(combined2, b21 + b22);
        }
    }
}
