using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        public static CompositeRadicalRatio Negate(CompositeRadicalRatio value)
        {
            return new CompositeRadicalRatio(-value.Numerator, value.Denominator);
        }

        public static CompositeRadicalRatio Invert(CompositeRadicalRatio value)
        {
            return new CompositeRadicalRatio(value.Denominator, value.Numerator);
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
            //               C1n   C2n   commonFactorLeft * C1n_reduced   commonFactorRight * C2n_reduced
            // R = R1 * R2 = --- * --- = ------------------------------ * -------------------------------
            //               C1d   C2d                      C1d                               C2d        
            //
            //     commonFactor * c1n_reduced * c2n_reduced
            //   = ----------------------------------------
            //                            c2d * c1d       
            //
            // ci_reduced === ci / commonFactori
            // commonFactor === commonFactorLeft * commonFactorRight;
            // Simplest form puts all common factors in numerator
            var commonFactorLeft = left.Numerator.GetCommonFactor();
            var commonFactorRight = right.Numerator.GetCommonFactor();
            var commonFactor = commonFactorLeft * commonFactorRight;
            var c_left_n_reduced = left.Numerator / commonFactorLeft;
            var c_right_n_reduced = right.Numerator / commonFactorRight;
            var d_left = left.Denominator;
            var d_right = right.Denominator;
            if (c_left_n_reduced == d_right)
            {
                c_left_n_reduced = CompositeRadical.One;
                d_right = CompositeRadical.One;
            }
            if (c_right_n_reduced == d_left)
            {
                c_right_n_reduced = CompositeRadical.One;
                d_left = CompositeRadical.One;
            }

            CompositeRadical numerator = commonFactor * c_left_n_reduced * c_right_n_reduced;
            CompositeRadical denominator = d_left * d_right;

            var result = new CompositeRadicalRatio(numerator, denominator);
            return result;
        }

        public static CompositeRadicalRatio Divide(
            CompositeRadicalRatio left,
            CompositeRadicalRatio right)
        {
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            return Multiply(left, Invert(right));
        }
    }
}
