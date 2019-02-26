using Open.Numeric.Primes;
using Rationals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Radicals
{
    public readonly partial struct BasicRadical
    {
        /// <summary>
        /// Simplest form is irreducible radical where the radical is the smallest possible integer
        /// </summary>
        private static void ToSimplestForm(
            Rational c_orig,
            BigInteger r_orig,
            out Rational c_final,
            out BigInteger r_final)
        {
            if (c_orig.IsZero || r_orig.IsZero)
            {
                c_final = Rational.Zero;
                r_final = BigInteger.Zero;
                return;
            }

            List<BigInteger> perfectSquareFactors = new List<BigInteger>();
            List<BigInteger> finalFactors = new List<BigInteger>();
            var currentCount = 0;
            BigInteger currentFactor = 0;
            foreach (BigInteger factor in Prime.Factors(r_orig).OrderBy(f => f))
            {
                if (factor != currentFactor)
                {
                    if (currentCount > 0)
                        finalFactors.Add(currentFactor);
                    currentCount = 0;
                    currentFactor = factor;
                }
                currentCount++;
                if (currentCount == 2)
                {
                    perfectSquareFactors.Add(currentFactor);
                    currentCount = 0;
                    currentFactor = 0;
                }
                else if (currentCount > 2)
                    throw new Exception("This shouldn't happen");
            }
            if (currentCount == 1)
                finalFactors.Add(currentFactor);
            else if (currentCount == 2)
                throw new Exception("This should not happen");

            Rational simplestCoefficient = c_orig;
            BigInteger simplestRadical = 1;

            foreach (BigInteger perfectSquareFactor in perfectSquareFactors)
                simplestCoefficient *= perfectSquareFactor;
            foreach (BigInteger factor in finalFactors)
                simplestRadical *= factor;

            c_final = simplestCoefficient.CanonicalForm;
            r_final = simplestRadical;
            return;
        }

        public static BasicRadical[] SimplifyRadicals(BasicRadical[] basicRadicals)
        {
            if (basicRadicals == null)
                return null;
            if (basicRadicals.Length == 0)
                return new BasicRadical[0];
            if (basicRadicals.Length == 1)
                return basicRadicals;

            Dictionary<BigInteger, BasicRadical> uniqueRadicals = new Dictionary<BigInteger, BasicRadical>();
            for (int i = 0; i < basicRadicals.Length; i++)
            {
                BasicRadical b = basicRadicals[i];
                if (uniqueRadicals.ContainsKey(b.R))
                    uniqueRadicals[b.R] = AddCompatible(uniqueRadicals[b.R], basicRadicals[i]);
                else if (BasicRadical.Zero != b)
                    uniqueRadicals[b.R] = b;
            }

            BasicRadical[] result = uniqueRadicals.Values.OrderBy(f => f.R).ToArray();
            return result;
        }
    }
}
