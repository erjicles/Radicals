using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSumRatio
    {
        public static RadicalSumRatio Negate(RadicalSumRatio value)
        {
            return new RadicalSumRatio(-value.Numerator, value.Denominator);
        }

        public static RadicalSumRatio Invert(RadicalSumRatio value)
        {
            return new RadicalSumRatio(value.Denominator, value.Numerator);
        }

        public static RadicalSumRatio Add(
            RadicalSumRatio left, 
            RadicalSumRatio right)
        {
            // n1   n2   (n1 * d2) + (n2 * d1)
            // -- + -- = ---------------------
            // d1   d2          d1 * d2
            RadicalSum numerator =
                (left.Numerator * right.Denominator)
                + (right.Numerator * left.Denominator);
            RadicalSum denominator = left.Denominator * right.Denominator;
            return new RadicalSumRatio(numerator, denominator);
        }

        public static RadicalSumRatio Subtract(
            RadicalSumRatio left, 
            RadicalSumRatio right)
        {
            return Add(left, -right);
        }

        public static RadicalSumRatio Multiply(
            RadicalSumRatio left, 
            RadicalSumRatio right)
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
                c_left_n_reduced = RadicalSum.One;
                d_right = RadicalSum.One;
            }
            if (c_right_n_reduced == d_left)
            {
                c_right_n_reduced = RadicalSum.One;
                d_left = RadicalSum.One;
            }

            RadicalSum numerator = commonFactor * c_left_n_reduced * c_right_n_reduced;
            RadicalSum denominator = d_left * d_right;

            var result = new RadicalSumRatio(numerator, denominator);
            return result;
        }

        public static RadicalSumRatio Divide(
            RadicalSumRatio left,
            RadicalSumRatio right)
        {
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            return Multiply(left, Invert(right));
        }
    }
}
