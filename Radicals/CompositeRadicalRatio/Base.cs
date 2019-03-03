using Rationals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        // R = b11 + b12 + ... + b1n
        //     ---------------------
        //     b21 + b22 + ... + b2n
        // bi = ci * sqrt(ri)
        private readonly RadicalSum _numerator;
        private readonly RadicalSum _denominator;
        public RadicalSum Numerator
        {
            get
            {
                if (_numerator == null)
                    return RadicalSum.Zero;
                return _numerator;
            }
        }
        public RadicalSum Denominator
        {
            get
            {
                // Should not happen
                if (_denominator == null)
                    return RadicalSum.One;
                // Should only happen when default constructor used
                if (_denominator == RadicalSum.Zero)
                    return RadicalSum.One;
                return _denominator;
            }
        }

        public CompositeRadicalRatio(Rational r)
            : this(new RadicalSum(r), RadicalSum.One)
        {
        }

        public CompositeRadicalRatio(Rational c, BigInteger r)
            :this(new RadicalSum(c, r))
        {
        }

        public CompositeRadicalRatio(Radical b)
            : this(new RadicalSum(b), RadicalSum.One)
        {
        }

        public CompositeRadicalRatio(Radical[] n)
            :this(new RadicalSum(n), RadicalSum.One)
        {
        }

        public CompositeRadicalRatio(Radical[] n, Radical[] d)
            :this(new RadicalSum(n), new RadicalSum(d))
        {
        }

        public CompositeRadicalRatio(RadicalSum n)
            : this(n, RadicalSum.One)
        {
        }

        public CompositeRadicalRatio(ref RadicalSum n, ref RadicalSum d)
        {
            if (RadicalSum.Zero == d)
                throw new ArgumentException("Denominator cannot be zero", nameof(d));
            if (d < RadicalSum.Zero)
            {
                ToSimplestForm(-n, -d, out _numerator, out _denominator);
            }
            else
            {
                ToSimplestForm(n, d, out _numerator, out _denominator);
            }
        }

        public CompositeRadicalRatio(RadicalSum n, RadicalSum d)
        {
            if (RadicalSum.Zero == d)
                throw new ArgumentException("Denominator cannot be zero", nameof(d));
            if (d < RadicalSum.Zero)
            {
                ToSimplestForm(-n, -d, out _numerator, out _denominator);
            }
            else
            {
                ToSimplestForm(n, d, out _numerator, out _denominator);
            }
        }
    }
}
