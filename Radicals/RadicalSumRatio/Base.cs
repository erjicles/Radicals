using Rationals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSumRatio
    {
        // R = r_n1 + r_n2 + ... + r_nM
        //     ------------------------
        //     r_d1 + r_d2 + ... + r_dN
        // r_i = coefficient_i * sqrt(radicand_i)
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

        public RadicalSumRatio(Rational radicand)
            : this(new RadicalSum(radicand), RadicalSum.One)
        {
        }

        public RadicalSumRatio(Rational coefficient, BigInteger radicand)
            :this(new RadicalSum(coefficient, radicand))
        {
        }

        public RadicalSumRatio(Radical radical)
            : this(new RadicalSum(radical), RadicalSum.One)
        {
        }

        public RadicalSumRatio(Radical[] numerator)
            :this(new RadicalSum(numerator), RadicalSum.One)
        {
        }

        public RadicalSumRatio(Radical[] numerator, Radical[] denominator)
            :this(new RadicalSum(numerator), new RadicalSum(denominator))
        {
        }

        public RadicalSumRatio(RadicalSum numerator)
            : this(numerator, RadicalSum.One)
        {
        }

        public RadicalSumRatio(ref RadicalSum numerator, ref RadicalSum denominator)
        {
            if (RadicalSum.Zero == denominator)
                throw new ArgumentException("Denominator cannot be zero", nameof(denominator));
            if (denominator < RadicalSum.Zero)
            {
                ToSimplestForm(-numerator, -denominator, out _numerator, out _denominator);
            }
            else
            {
                ToSimplestForm(numerator, denominator, out _numerator, out _denominator);
            }
        }

        public RadicalSumRatio(RadicalSum numerator, RadicalSum denominator)
        {
            if (RadicalSum.Zero == denominator)
                throw new ArgumentException("Denominator cannot be zero", nameof(denominator));
            if (denominator < RadicalSum.Zero)
            {
                ToSimplestForm(-numerator, -denominator, out _numerator, out _denominator);
            }
            else
            {
                ToSimplestForm(numerator, denominator, out _numerator, out _denominator);
            }
        }
    }
}
