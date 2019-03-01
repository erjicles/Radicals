using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
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
                n += Numerator.Radicals[i].C * (Rational)Math.Sqrt((double)Numerator.Radicals[i].R);
            for (int i = 0; i < Denominator.Radicals.Length; i++)
                d += Denominator.Radicals[i].C * (Rational)Math.Sqrt((double)Denominator.Radicals[i].R);
            return n / d;
        }

        public static explicit operator double(CompositeRadicalRatio value)
        {
            return value.ToDouble();
        }
        public static explicit operator Rational(CompositeRadicalRatio value)
        {
            return value.ToRational();
        }
    }
}
