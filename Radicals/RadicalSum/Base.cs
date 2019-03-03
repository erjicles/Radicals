using Open.Numeric.Primes;
using Rationals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Radicals
{
    public readonly partial struct RadicalSum
    {
        // S = r_1 + r_2 + ... + r_n
        // r_i = coefficient_i * sqrt(radicand_i)
        private readonly Radical[] _radicals;

        public Radical[] Radicals
        {
            get
            {
                if (_radicals == null)
                    return new Radical[1] { Radical.Zero };
                return _radicals;
            }
        }

        public RadicalSum(Rational radicand)
            : this(new Radical(radicand))
        {
        }

        public RadicalSum(ref Rational coefficient, ref BigInteger radicand)
        {
            _radicals = new Radical[1] { new Radical(coefficient, radicand) };
        }

        public RadicalSum(Rational coefficient, BigInteger radicand)
        {
            _radicals = new Radical[1] { new Radical(coefficient, radicand) };
        }

        public RadicalSum(Radical radical)
        {
            if (radical == null)
                throw new ArgumentNullException(nameof(radical));
            _radicals = new Radical[1] { radical };
        }

        public RadicalSum(Radical[] radicals)
        {
            if (radicals == null)
                throw new ArgumentNullException(nameof(radicals));
            if (radicals.Length == 0)
                throw new Exception("No radicals provided");
            
            _radicals = Radical.SimplifyRadicals(radicals);
        }

    }
}
