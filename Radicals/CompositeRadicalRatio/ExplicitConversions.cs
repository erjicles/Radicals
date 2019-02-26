using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        public double ToDouble()
        {
            return Numerator.ToDouble() / Denominator.ToDouble();
        }

        public static explicit operator double(CompositeRadicalRatio value)
        {
            return value.ToDouble();
        }
    }
}
