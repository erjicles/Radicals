using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct Radical
    {
        public double ToDouble()
        {
            return (double)Coefficient * Math.Sqrt((double)Radicand);
        }

        public Rational ToRational()
        {
            return Coefficient;
        }

        public static explicit operator double(Radical basicRadical)
        {
            return basicRadical.ToDouble();
        }

        public static explicit operator Rational(Radical value)
        {
            return value.ToRational();
        }
        
    }
}
