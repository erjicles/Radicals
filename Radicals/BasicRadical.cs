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

        /// <summary>
        /// Zero
        /// </summary>
        public static readonly BasicRadical Zero = new BasicRadical(0, 0);

        /// <summary>
        /// One
        /// </summary>
        public static readonly BasicRadical One = new BasicRadical(1, 1);

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

        public static BasicRadical AddCompatible(BasicRadical left, BasicRadical right)
        {
            if (left.r != right.r)
                throw new Exception("Trying to add compatible radicals that aren't compatible");
            return new BasicRadical(left.c + right.c, left.r);
        }

        public static BasicRadical[] Add(BasicRadical left, BasicRadical right)
        {
            BasicRadical[] result;
            if (left.IsCompatibleRadical(right))
            {
                result = new BasicRadical[1];
                result[0] = AddCompatible(left, right);
            }
            else
            {
                result = new BasicRadical[2];
                result[0] = left;
                result[1] = right;
            }
            return result;
        }

        public static BasicRadical[] Subtract(BasicRadical left, BasicRadical right)
        {
            return Add(left, -right);
        }

        public static BasicRadical Multiply(BasicRadical left, BasicRadical right)
        {
            return new BasicRadical(left.c * right.c, left.r * right.r);
        }

        public static BasicRadical Divide(BasicRadical left, BasicRadical right)
        {
            // r' = [c1 * sqrt(r1)] / [c2 * sqrt(r2)]
            //    = [c1 / c2] * sqrt(r1 / r2)
            //    = [c1 / c2] * sqrt(r1 * r2 / r2 * r2)
            //    = [c1 / (c2 * r2)] * sqrt(r1 * r2)
            return new BasicRadical(left.c / (right.c * right.r), left.r * right.r);
        }

        public static BasicRadical operator -(BasicRadical value)
        {
            return new BasicRadical(-value.c, value.r);
        }

        public static BasicRadical operator +(BasicRadical value)
        {
            return value;
        }

        public static BasicRadical[] operator +(BasicRadical left, BasicRadical right)
        {
            return Add(left, right);
        }

        public static BasicRadical[] operator -(BasicRadical left, BasicRadical right)
        {
            return Subtract(left, right);
        }

        public static BasicRadical operator *(BasicRadical left, BasicRadical right)
        {
            return Multiply(left, right);
        }

        public static BasicRadical operator /(BasicRadical left, BasicRadical right)
        {
            return Divide(left, right);
        }

        public static bool operator <(BasicRadical left, BasicRadical right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(BasicRadical left, BasicRadical right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(BasicRadical left, BasicRadical right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool operator >=(BasicRadical left, BasicRadical right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator ==(BasicRadical left, BasicRadical right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BasicRadical left, BasicRadical right)
        {
            return !left.Equals(right);
        }

        public static BasicRadical[] SimplifyRadicals(BasicRadical[] basicRadicals)
        {
            if (basicRadicals == null)
                return null;
            if (basicRadicals.Length == 0)
                return new BasicRadical[0];
            if (basicRadicals.Length == 1)
                return basicRadicals;

            Dictionary<BigInteger, BasicRadical> uniqueRadicals = new Dictionary<BigInteger, BasicRadical>();
            for (int i = 0; i < basicRadicals.Length; i++)
            {
                BasicRadical b = basicRadicals[i];
                if (uniqueRadicals.ContainsKey(b.r))
                    uniqueRadicals[b.r] = AddCompatible(uniqueRadicals[b.r], basicRadicals[i]);
                else
                    uniqueRadicals[b.r] = b;
            }

            BasicRadical[] result = uniqueRadicals.Values.OrderBy(f => f).ToArray();
            return result;
        }
    }
}
