using Open.Numeric.Primes;
using Rationals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Radicals
{
    public readonly partial struct Radical
    {

        // R = c * sqrt(r)
        public Rational C { get; }
        public BigInteger R { get; }

        

        public Radical(Rational fractionalRadical)
            : this(fractionalRadical.Denominator, fractionalRadical.Numerator * fractionalRadical.Denominator)
        {
        }

        public Radical(ref Rational c, ref BigInteger r)
        {
            if (r < 0)
                throw new InvalidOperationException("Negative value under radical");
            C = c;
            R = r;
        }

        public Radical(Rational c, BigInteger r)
        {
            if (r < 0)
                throw new InvalidOperationException("Negative value under radical");
            C = c;
            R = r;
        }

        /// <summary>
        /// Simplest form is irreducible radical where the radical is the smallest possible integer
        /// </summary>
        public Radical SimplestForm
        {
            get
            {
                if (C.IsZero)
                    return Zero;
                if (R.IsZero)
                    return Zero;

                List<BigInteger> perfectSquareFactors = new List<BigInteger>();
                List<BigInteger> finalFactors = new List<BigInteger>();
                var currentCount = 0;
                BigInteger currentFactor = 0;
                foreach (BigInteger factor in Prime.Factors(R).OrderBy(f => f))
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

                Rational simplestCoefficient = C;
                BigInteger simplestRadical = 1;

                foreach (BigInteger perfectSquareFactor in perfectSquareFactors)
                    simplestCoefficient *= perfectSquareFactor;
                foreach (BigInteger factor in finalFactors)
                    simplestRadical *= factor;

                var simplest = new Radical(simplestCoefficient.CanonicalForm, simplestRadical);
                return simplest;
            }
        }
    }
}
