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
        private readonly CompositeRadical _numerator;
        private readonly CompositeRadical _denominator;
        public CompositeRadical Numerator
        {
            get
            {
                if (_numerator == null)
                    return CompositeRadical.Zero;
                return _numerator;
            }
        }
        public CompositeRadical Denominator
        {
            get
            {
                // Should not happen
                if (_denominator == null)
                    return CompositeRadical.One;
                // Should only happen when default constructor used
                if (_denominator == CompositeRadical.Zero)
                    return CompositeRadical.One;
                return _denominator;
            }
        }

        public CompositeRadicalRatio(Rational r)
            : this(new CompositeRadical(r), CompositeRadical.One)
        {
        }

        public CompositeRadicalRatio(Rational c, BigInteger r)
            :this(new CompositeRadical(c, r))
        {
        }

        public CompositeRadicalRatio(BasicRadical b)
            : this(new CompositeRadical(b), CompositeRadical.One)
        {
        }

        public CompositeRadicalRatio(CompositeRadical n)
            : this(n, CompositeRadical.One)
        {
        }

        public CompositeRadicalRatio(ref CompositeRadical n, ref CompositeRadical d)
        {
            if (CompositeRadical.Zero == d)
                throw new ArgumentException("Denominator cannot be zero", nameof(d));
            if (d < CompositeRadical.Zero)
            {
                ToSimplestForm(-n, -d, out _numerator, out _denominator);
            }
            else
            {
                ToSimplestForm(n, d, out _numerator, out _denominator);
            }
        }

        public CompositeRadicalRatio(CompositeRadical n, CompositeRadical d)
        {
            if (CompositeRadical.Zero == d)
                throw new ArgumentException("Denominator cannot be zero", nameof(d));
            if (d < CompositeRadical.Zero)
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
