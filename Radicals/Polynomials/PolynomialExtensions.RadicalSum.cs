using Open.Numeric.Primes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Radicals.Polynomials;

internal static class PolynomialExtensions
{
    public static Polynomial<RadicalSum> Divide(this Polynomial<RadicalSum> numerator, Radical denominator)
    {
        return new Polynomial<RadicalSum>(numerator.Coefficients.ToDictionary(kvp => kvp.Key, kvp => kvp.Value / denominator));
    }

    public static void ExtractTermsContainingNthRoot(
        this Polynomial<RadicalSum> p,
        BigInteger radicand,
        int index,
        out Polynomial<RadicalSum> p_reduced,
        out Polynomial<RadicalSum> p_extract)
    {
        p_reduced = Polynomial<RadicalSum>.Zero;
        p_extract = Polynomial<RadicalSum>.Zero;
        foreach (var (degree, coefficient) in p.Coefficients)
        {
            RadicalSum coeff_reduced = coefficient;
            RadicalSum coeff_target = RadicalSum.Zero;

            for (int j = 0; j < coefficient.Radicals.Length; j++)
            {
                if (coefficient.Radicals[j].Index == index)
                {
                    if (coefficient.Radicals[j].Radicand % radicand == 0)
                    {
                        coeff_target += coefficient.Radicals[j];
                        coeff_reduced -= coefficient.Radicals[j];
                    }
                }
            }

            p_reduced = p_reduced.Add(new Polynomial<RadicalSum>(new() { { degree, coeff_reduced } }));
            p_extract = p_extract.Add(new Polynomial<RadicalSum>(new() { { degree, coeff_target } }));
        }
    }

    public static Tuple<int, BigInteger>[] GetUniquePrimeNthRoots(this Polynomial<RadicalSum> p)
    {
        // Set of found index/radicand pairs
        var foundPrimeNthRoots = new HashSet<Tuple<int, BigInteger>>();

        foreach (var (_, coefficient) in p.Coefficients)
        {
            for (int j = 0; j < coefficient.Radicals.Length; j++)
            {
                var r = coefficient.Radicals[j];
                foreach (BigInteger primeFactor in Prime.Factors(r.Radicand))
                {
                    var key = new Tuple<int, BigInteger>(r.Index, primeFactor);
                    if (!foundPrimeNthRoots.Contains(key))
                        foundPrimeNthRoots.Add(key);
                }
            }
        }

        return foundPrimeNthRoots.OrderBy(k => k.Item1).ThenBy(k => k.Item2).ToArray();
    }

    public static Polynomial<RadicalSum> Multiply(this Polynomial<RadicalSum> value, BigInteger multiplier)
    {
        return new(value.Coefficients.ToDictionary(kvp => kvp.Key, kvp => kvp.Value * multiplier));
    }

    public static Polynomial<RadicalSum> Negate(this Polynomial<RadicalSum> value)
    {
        return new(value.Coefficients.ToDictionary(kvp => kvp.Key, kvp => -kvp.Value));
    }

    public static Polynomial<RadicalSum> Subtract(this Polynomial<RadicalSum> value, Polynomial<RadicalSum> other)
    {
        return value.Add(other.Negate());
    }
}
