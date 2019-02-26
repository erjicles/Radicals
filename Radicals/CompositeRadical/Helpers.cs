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
            var numerators = new BigInteger[radicals.Length];
            var denominators = new BigInteger[radicals.Length];
            for (int i = 0; i < radicals.Length; i++)
            {
                numerators[i] = radicals[i].c.Numerator;
                denominators[i] = radicals[i].c.Denominator;
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
