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

        // R = b1 + b2 + ... + bn
        // bi = ci * sqrt(ri)
        private readonly BasicRadical[] radicals;

        public Radical(Rational r)
            : this(r.Denominator, r.Numerator * r.Denominator)
        {
        }

        public Radical(ref Rational c, ref BigInteger r)
        {
            if (r < 0)
                throw new InvalidOperationException("Negative value under radical");
            radicals = new BasicRadical[1];
            radicals[0] = new BasicRadical(c, r);
        }

        public Radical(Rational c, BigInteger r)
        {
            if (r < 0)
                throw new InvalidOperationException("Negative value under radical");
            radicals = new BasicRadical[1];
            radicals[0] = new BasicRadical(c, r);
        }

        internal Radical(BasicRadical basicRadical)
        {
            if (basicRadical == null)
                throw new ArgumentNullException(nameof(basicRadical));
            radicals = new BasicRadical[1] { basicRadical };
        }

        internal Radical(BasicRadical[] basicRadicals)
        {
            if (basicRadicals == null)
                throw new ArgumentNullException(nameof(basicRadicals));
            if (basicRadicals.Length == 0)
                throw new Exception("No basic radicals in array");
            
            radicals = BasicRadical.SimplifyRadicals(basicRadicals);
        }

    }
}
