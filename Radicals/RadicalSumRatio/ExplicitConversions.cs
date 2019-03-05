using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSumRatio
    {
        public double ToDouble()
        {
            return Numerator.ToDouble() / Denominator.ToDouble();
        }
        public Rational ToRational()
        {
            Rational n = 0;
            Rational d = 0;
            for (int i = 0; i < Numerator.Radicals.Length; i++)
                n += Numerator.Radicals[i].Coefficient * (Rational)Math.Sqrt((double)Numerator.Radicals[i].Radicand);
            for (int i = 0; i < Denominator.Radicals.Length; i++)
                d += Denominator.Radicals[i].Coefficient * (Rational)Math.Sqrt((double)Denominator.Radicals[i].Radicand);
            return n / d;
        }
        public RadicalSum ToRadicalSum()
        {
            return Numerator / Denominator;
        }

        public static explicit operator double(RadicalSumRatio value)
        {
            return value.ToDouble();
        }
        public static explicit operator Rational(RadicalSumRatio value)
        {
            return value.ToRational();
        }
        public static explicit operator RadicalSumRatio(Radical value)
        {
            return new RadicalSumRatio(value);
        }
        public static explicit operator RadicalSumRatio(RadicalSum value)
        {
            return new RadicalSumRatio(value);
        }
        public static explicit operator RadicalSum(RadicalSumRatio value)
        {
            return value.ToRadicalSum();
        }
    }
}
