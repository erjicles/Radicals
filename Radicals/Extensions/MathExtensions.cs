using Rationals;
using System;
using System.Numerics;

namespace Radicals.Extensions;

public static class MathExtensions
{
    public static T Pow<T>(this T value, int exp)
            where T : IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => value.Pow(exp, T.MultiplicativeIdentity, (t1, t2) => t1 * t2);

    public static Rational Pow(this Rational value, int exp)
        => value.Pow(exp, Rational.One, (r1, r2) => r1 * r2);

    public static T Pow<T>(this T value, int exp, T multiplicativeIdentity, Func<T, T, T> multiplyOperator)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(exp, nameof(exp));

        if (exp == 0)
        {
            return multiplicativeIdentity;
        }

        if (exp == 1)
        {
            return value;
        }

        var basePoly = value;
        var result = multiplicativeIdentity;

        // Power using exponentiation‑by‑squaring.
        // Runs in O(log exp) polynomial multiplications.
        while (exp > 0)
        {
            bool isOdd = (exp % 2) == 1;

            if (isOdd)
            {
                result = multiplyOperator(result, basePoly);
            }

            exp = exp / 2;

            if (exp > 0)
            {
                basePoly = multiplyOperator(basePoly, basePoly);
            }
        }

        return result;
    }
}
