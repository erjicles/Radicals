using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        public static CompositeRadicalRatio Negate(CompositeRadicalRatio value)
        {
            return new CompositeRadicalRatio(-value.Numerator, value.Denominator);
        }

        public static CompositeRadicalRatio Add(
            CompositeRadicalRatio left, 
            CompositeRadicalRatio right)
        {
            // n1   n2   (n1 * d2) + (n2 * d1)
            // -- + -- = ---------------------
            // d1   d2          d1 * d2
            CompositeRadical numerator =
                (left.Numerator * right.Denominator)
                + (right.Numerator * left.Denominator);
            CompositeRadical denominator = left.Denominator * right.Denominator;
            return new CompositeRadicalRatio(numerator, denominator);
        }

        public static CompositeRadicalRatio Subtract(
            CompositeRadicalRatio left, 
            CompositeRadicalRatio right)
        {
            return Add(left, -right);
        }

        public static CompositeRadicalRatio Multiply(
            CompositeRadicalRatio left, 
            CompositeRadicalRatio right)
        {
            CompositeRadical numerator = left.Numerator * right.Numerator;
            CompositeRadical denominator = left.Denominator * right.Denominator;
            return new CompositeRadicalRatio(numerator, denominator);
        }

        public static CompositeRadicalRatio Divide(
            CompositeRadicalRatio left,
            CompositeRadicalRatio right)
        {
            CompositeRadical numerator = left.Numerator * right.Denominator;
            CompositeRadical denominator = left.Denominator * right.Numerator;
            return new CompositeRadicalRatio(numerator, denominator);
        }
    }
}
