using Open.Numeric.Primes;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadical
    {
        public void GetCommonFactors(out BigInteger[] upstairs, out BigInteger[] downstairs)
        {
            var numerators = new BigInteger[Radicals.Length];
            var denominators = new BigInteger[Radicals.Length];
            for (int i = 0; i < Radicals.Length; i++)
            {
                numerators[i] = Radicals[i].C.Numerator;
                denominators[i] = Radicals[i].C.Denominator;
            }
            var numeratorFactors = new List<BigInteger>();
            var denominatorFactors = new List<BigInteger>();
            foreach (BigInteger factor in Prime.CommonFactors(numerators))
                numeratorFactors.Add(factor);
            foreach (BigInteger factor in Prime.CommonFactors(denominators))
                denominatorFactors.Add(factor);
            numeratorFactors.Sort();
            denominatorFactors.Sort();
            upstairs = numeratorFactors.ToArray();
            downstairs = denominatorFactors.ToArray();
        }
    }
}
