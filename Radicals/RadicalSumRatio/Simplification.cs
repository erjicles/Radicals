using Rationals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSumRatio
    {
        private static void ToSimplestForm(
            RadicalSum n_in,
            RadicalSum d_in,
            out RadicalSum n_out,
            out RadicalSum d_out)
        {
            // First extract common factors from numerator and denominator:
            // N = common_factor_n * (N/common_factor_n); n_reduced === N/common_factor_n
            // D = common_factor_d * (D/common_factor_d); d_reduced === D/common_factor_d
            Rational common_factor_n = n_in.GetCommonFactor();
            Rational common_factor_d = d_in.GetCommonFactor();
            var n_reduced = n_in * Rational.Invert(common_factor_n);
            var d_reduced = d_in * Rational.Invert(common_factor_d);

            //     N   common_factor_n * (N/common_factor_n)   common_factor_n * n_reduced                   n_reduced
            // C = - = ------------------------------------- = --------------------------- = common_factor * ---------
            //     D   common_factor_d * (D/common_factor_d)   common_factor_d * d_reduced                   d_reduced
            //
            // common_factor === common_factor_n / common_factor_d
            var common_factor = common_factor_n / common_factor_d;

            if (n_reduced == d_reduced)
            {
                n_out = common_factor * RadicalSum.One;
                d_out = RadicalSum.One;
            }
            else if (d_reduced.Radicals.Length == 1)
            {
                n_out = common_factor * n_reduced / d_reduced.Radicals[0];
                d_out = RadicalSum.One;
            }
            else
            {
                n_out = common_factor * n_reduced;
                d_out = d_reduced;
            }
        }
    }
}
