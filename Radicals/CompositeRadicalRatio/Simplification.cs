using Rationals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        private static void ToSimplestForm(
            CompositeRadical n_in,
            CompositeRadical d_in,
            out CompositeRadical n_out,
            out CompositeRadical d_out)
        {
            // Get all common factors for both numerator and denominator
            BigInteger[] commonFactors_n_in_n;
            BigInteger[] commonFactors_n_in_d;
            BigInteger[] commonFactors_d_in_n;
            BigInteger[] commonFactors_d_in_d;
            n_in.GetCommonFactors(
                upstairs: out commonFactors_n_in_n, 
                downstairs: out commonFactors_n_in_d);
            d_in.GetCommonFactors(
                upstairs: out commonFactors_d_in_n,
                downstairs: out commonFactors_d_in_d);

            // First extract common factors from numerator and denominator:
            // N = common_factor_n * (N/common_factor_n)
            // D = common_factor_d * (D/common_factor_d)
            Rational common_factor_n = 1;
            Rational common_factor_d = 1;
            for (int i = 0; i < commonFactors_n_in_n.Length; i++)
                common_factor_n *= commonFactors_n_in_n[i];
            for (int i = 0; i < commonFactors_n_in_d.Length; i++)
                common_factor_n /= commonFactors_n_in_d[i];
            for (int i = 0; i < commonFactors_d_in_n.Length; i++)
                common_factor_d *= commonFactors_d_in_n[i];
            for (int i = 0; i < commonFactors_d_in_d.Length; i++)
                common_factor_d /= commonFactors_d_in_d[i];

            var n_reduced = n_in * Rational.Invert(common_factor_n);
            var d_reduced = d_in * Rational.Invert(common_factor_d);
            var common_factor_reduced = common_factor_n / common_factor_d;

            n_out = common_factor_reduced * n_reduced;
            d_out = d_reduced;
        }
    }
}
