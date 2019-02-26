using Rationals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;

namespace Radicals.Test
{
    public class CompositeRadicalRatioTests
    {
        [Fact]
        public void DefaultConstructorTests()
        {
            var test = new CompositeRadicalRatio();
            Assert.Equal(CompositeRadicalRatio.Zero, test);
        }

        [Fact]
        public void SimplificationTests()
        {
            
        }
    }
}
