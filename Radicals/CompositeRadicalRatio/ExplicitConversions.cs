using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        public double ToDouble()
        {
            return numerator.ToDouble() / denominator.ToDouble();
        }

        public static explicit operator double(CompositeRadicalRatio value)
        {
            return value.ToDouble();
        }
    }
}
