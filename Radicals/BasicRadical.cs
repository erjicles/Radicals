using Open.Numeric.Primes;
using Rationals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Radicals
{
    internal struct BasicRadical
        : IFormattable, IComparable, IComparable<BasicRadical>, IEquatable<BasicRadical>
    {
        // R = c * sqrt(r)
        public readonly Rational c_original;
        public readonly BigInteger r_original;
        public readonly Rational c;
        public readonly BigInteger r;

        public BasicRadical(Rational c, BigInteger r)
        {
            c_original = c;
            r_original = r;
            ToSimplestForm(
                c_orig: c,
                r_orig: r,
                c_final: out this.c,
                r_final: out this.r);
        }

        public bool IsCompatibleRadical(BasicRadical b)
        {
            return r == b.r;
        }

        /// <summary>
        /// Simplest form is irreducible radical where the radical is the smallest possible integer
        /// </summary>
        private static void ToSimplestForm(
            Rational c_orig,
            BigInteger r_orig,
            out Rational c_final,
            out BigInteger r_final)
        {
            if (c_orig.IsZero || r_orig.IsZero)
            {
                c_final = Rational.Zero;
                r_final = BigInteger.Zero;
                return;
            }

            List<BigInteger> perfectSquareFactors = new List<BigInteger>();
            List<BigInteger> finalFactors = new List<BigInteger>();
            var currentCount = 0;
            BigInteger currentFactor = 0;
            foreach (BigInteger factor in Prime.Factors(r_orig).OrderBy(f => f))
            {
                if (factor != currentFactor)
                {
                    if (currentCount > 0)
                        finalFactors.Add(currentFactor);
                    currentCount = 0;
                    currentFactor = factor;
                }
                currentCount++;
                if (currentCount == 2)
                {
                    perfectSquareFactors.Add(currentFactor);
                    currentCount = 0;
                    currentFactor = 0;
                }
                else if (currentCount > 2)
                    throw new Exception("This shouldn't happen");
            }
            if (currentCount == 1)
                finalFactors.Add(currentFactor);
            else if (currentCount == 2)
                throw new Exception("This should not happen");

            Rational simplestCoefficient = c_orig;
            BigInteger simplestRadical = 1;

            foreach (BigInteger perfectSquareFactor in perfectSquareFactors)
                simplestCoefficient *= perfectSquareFactor;
            foreach (BigInteger factor in finalFactors)
                simplestRadical *= factor;

            c_final = simplestCoefficient;
            r_final = simplestRadical;
            return;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public override string ToString()
        {
            if (c.IsZero)
                return "0";
            if (r.IsZero)
                return "0";

            string cPart = "";
            string rPart = "";
            if (!c.IsOne)
                cPart = c.ToString();
            if (!r.IsOne)
                rPart = "Sqrt(" + r.ToString() + ")";
            if (cPart.Length > 0 && rPart.Length > 0)
            {
                if (c.Denominator == 1)
                    cPart = cPart + " * ";
                else
                    cPart = "(" + cPart + ") * ";
            }
            return cPart + rPart;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is BasicRadical))
                throw new ArgumentException("Invalid type comparison", nameof(obj));
            return CompareTo((BasicRadical)obj);
        }

        public int CompareTo(BasicRadical other)
        {
            if (c == other.c && r == other.r)
                return 0;

            return (c * c * r).CompareTo(other.c * other.c * other.r);
        }

        public bool Equals(BasicRadical other)
        {
            return c == other.c && r == other.r;
        }

        public static BasicRadical operator -(BasicRadical value)
        {
            return new BasicRadical(-value.c, value.r);
        }

        public static BasicRadical operator +(BasicRadical value)
        {
            return value;
        }

        public static BasicRadical operator *(BasicRadical left, BasicRadical right)
        {
            return new BasicRadical(left.c * right.c, left.r * right.r);
        }
    }
}
