using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        public static CompositeRadicalRatio Negate(CompositeRadicalRatio value)
        {
            return new CompositeRadicalRatio(-value.numerator, value.denominator);
        }

        public static CompositeRadicalRatio Add(
            CompositeRadicalRatio left, 
            CompositeRadicalRatio right)
        {
            // n1   n2   (n1 * d2) + (n2 * d1)
            // -- + -- = ---------------------
            // d1   d2          d1 * d2
            CompositeRadical numerator =
                (left.numerator * right.denominator)
                + (right.numerator * left.denominator);
            CompositeRadical denominator = left.denominator * right.denominator;
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
            CompositeRadical numerator = left.numerator * right.numerator;
            CompositeRadical denominator = left.denominator * right.denominator;
            return new CompositeRadicalRatio(numerator, denominator);
        }

        public static CompositeRadicalRatio Divide(
            CompositeRadicalRatio left,
            CompositeRadicalRatio right)
        {
            CompositeRadical numerator = left.numerator * right.denominator;
            CompositeRadical denominator = left.denominator * right.numerator;
            return new CompositeRadicalRatio(numerator, denominator);
        }
    }
}
