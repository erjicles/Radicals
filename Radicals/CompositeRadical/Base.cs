using Open.Numeric.Primes;
using Rationals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Radicals
{
    public readonly partial struct CompositeRadical
    {
        // R = b1 + b2 + ... + bn
        // bi = ci * sqrt(ri)
        private readonly BasicRadical[] _radicals;

        public BasicRadical[] Radicals
        {
            get
            {
                if (_radicals == null)
                    return new BasicRadical[1] { BasicRadical.Zero };
                return _radicals;
            }
        }

        public CompositeRadical(Rational r)
            : this(new BasicRadical(r))
        {
        }

        public CompositeRadical(ref Rational c, ref BigInteger r)
        {
            if (r < 0)
                throw new InvalidOperationException("Negative value under radical");
            _radicals = new BasicRadical[1] { new BasicRadical(c, r) };
        }

        public CompositeRadical(Rational c, BigInteger r)
        {
            if (r < 0)
                throw new InvalidOperationException("Negative value under radical");
            _radicals = new BasicRadical[1] { new BasicRadical(c, r) };
        }

        public CompositeRadical(BasicRadical basicRadical)
        {
            if (basicRadical == null)
                throw new ArgumentNullException(nameof(basicRadical));
            _radicals = new BasicRadical[1] { basicRadical };
        }

        public CompositeRadical(BasicRadical[] basicRadicals)
        {
            if (basicRadicals == null)
                throw new ArgumentNullException(nameof(basicRadicals));
            if (basicRadicals.Length == 0)
                throw new Exception("No basic radicals in array");
            
            _radicals = BasicRadical.SimplifyRadicals(basicRadicals);
        }

    }
}
