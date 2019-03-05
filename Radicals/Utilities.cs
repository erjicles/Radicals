using Open.Numeric.Primes;
using Rationals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public class Utilities
    {
        public static BigInteger CombineFactors(BigInteger[] factors)
        {
            BigInteger result = 1;
            foreach (BigInteger factor in factors)
                result *= factor;
            return result;
        }

        public static void GetCommonFactors(
            Rational[] rationals,
            out BigInteger[] upstairs, 
            out BigInteger[] downstairs)
        {
            var numerators = new BigInteger[rationals.Length];
            var denominators = new BigInteger[rationals.Length];
            for (int i = 0; i < rationals.Length; i++)
            {
                numerators[i] = rationals[i].Numerator;
                denominators[i] = rationals[i].Denominator;
            }
            var numeratorFactors = new List<BigInteger>();
            var denominatorFactors = new List<BigInteger>();
            foreach (BigInteger factor in Prime.CommonFactors(numerators))
                numeratorFactors.Add(factor);
            foreach (BigInteger factor in Prime.CommonFactors(denominators))
                denominatorFactors.Add(factor);
            // Prime.CommonFactors doesn't get -1, account for this:
            BigInteger sign = -1;
            foreach (BigInteger numerator in numerators)
                if (numerator >= 0)
                    sign = 1;
            if (sign == -1)
                numeratorFactors.Add(sign);

            numeratorFactors.Sort();
            denominatorFactors.Sort();
            upstairs = numeratorFactors.ToArray();
            downstairs = denominatorFactors.ToArray();
        }

        public static Rational GetCommonFactor(Rational[] rationals)
        {
            BigInteger[] upstairs = new BigInteger[0];
            BigInteger[] downstairs = new BigInteger[0];
            GetCommonFactors(rationals: rationals, upstairs: out upstairs, downstairs: out downstairs);
            var result = new Rational(Utilities.CombineFactors(upstairs), Utilities.CombineFactors(downstairs));
            return result.CanonicalForm;
        }
    }
}
