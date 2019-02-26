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
        private readonly CompositeRadical numerator;
        private readonly CompositeRadical denominator;

        public CompositeRadicalRatio(Rational r)
            : this(new CompositeRadical(r), CompositeRadical.One)
        {
        }

        public CompositeRadicalRatio(Rational c, BigInteger r)
            :this(new CompositeRadical(c, r))
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
                ToSimplestForm(-n, -d, out numerator, out denominator);
            }
            else
            {
                ToSimplestForm(n, d, out numerator, out denominator);
            }
        }

        public CompositeRadicalRatio(CompositeRadical n, CompositeRadical d)
        {
            if (CompositeRadical.Zero == d)
                throw new ArgumentException("Denominator cannot be zero", nameof(d));
            if (d < CompositeRadical.Zero)
            {
                ToSimplestForm(-n, -d, out numerator, out denominator);
            }
            else
            {
                ToSimplestForm(n, d, out numerator, out denominator);
            }
        }
    }
}
