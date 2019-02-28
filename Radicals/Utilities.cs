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
    }
}
