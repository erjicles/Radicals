using Open.Numeric.Primes;
using Rationals;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Radicals
{
    public readonly struct Radical
        : IComparable, IComparable<Radical>, IEquatable<Radical>, IFormattable
    {
        // R = Coefficient * sqrt(Radicand)
        public readonly Rational coefficient_unsimplified;
        public readonly BigInteger radicand_unsimplified;
        private readonly Rational _coefficient;
        private readonly BigInteger _radicand;

        public Rational Coefficient
        {
            get
            {
                if (_coefficient == null)
                    return 0;
                return _coefficient;
            }
        }
        public BigInteger Radicand
        {
            get
            {
                if (_radicand == null)
                    return 0;
                return _radicand;
            }
        }

        public Radical(Rational radicand)
            : this(new Rational(1, radicand.Denominator), radicand.Numerator * radicand.Denominator)
        {
        }

        public Radical(Rational coefficient, BigInteger radicand)
        {
            if (radicand < 0)
                throw new ArgumentException("Cannot have negative radicand");
            coefficient_unsimplified = coefficient;
            radicand_unsimplified = radicand;
            ToSimplestForm(
                c_orig: coefficient,
                r_orig: radicand,
                c_final: out this._coefficient,
                r_final: out this._radicand);
        }

        /// <summary>
        /// Zero
        /// </summary>
        public static readonly Radical Zero = new Radical(0, 0);

        /// <summary>
        /// One
        /// </summary>
        public static readonly Radical One = new Radical(1, 1);

        public bool IsRational
        {
            get
            {
                if (Radicand < 2)
                    return true;
                return false;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is Radical))
                throw new ArgumentException("Invalid type comparison", nameof(obj));
            return CompareTo((Radical)obj);
        }

        public int CompareTo(Radical other)
        {
            if (other == null)
                return 1;

            if (Coefficient == other.Coefficient && Radicand == other.Radicand)
                return 0;

            return (Coefficient * Coefficient * Radicand).CompareTo(other.Coefficient * other.Coefficient * other.Radicand);
        }

        public bool Equals(Radical other)
        {
            return Coefficient == other.Coefficient && Radicand == other.Radicand;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Radical))
                throw new ArgumentException("Invalid type equality check", nameof(obj));
            return Equals((Radical)obj);
        }

        public override int GetHashCode()
        {
            int h1 = Coefficient.GetHashCode();
            int h2 = Radicand.GetHashCode();
            return (((h1 << 5) + h1) ^ h2);
        }

        public bool IsCompatibleRadical(Radical b)
        {
            return Radicand == b.Radicand;
        }

        public double ToDouble()
        {
            return (double)Coefficient * Math.Sqrt((double)Radicand);
        }

        public Rational ToRational()
        {
            return Coefficient;
        }

        public static Radical Sqrt(Rational value)
        {
            return new Radical(value);
        }

        public static explicit operator double(Radical basicRadical)
        {
            return basicRadical.ToDouble();
        }

        public static explicit operator Rational(Radical value)
        {
            return value.ToRational();
        }

        public static implicit operator Radical(Rational value)
        {
            return new Radical(value, 1);
        }

        public static implicit operator Radical(byte value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(sbyte value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(short value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(ushort value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(int value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(uint value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(long value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(ulong value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static Radical Negate(Radical value)
        {
            return new Radical(-value.Coefficient, value.Radicand);
        }

        public static Radical AddCompatible(Radical left, Radical right)
        {
            if (left.Radicand != right.Radicand)
                throw new Exception("Trying to add compatible radicals that aren't compatible");
            return new Radical(left.Coefficient + right.Coefficient, left.Radicand);
        }

        public static RadicalSum Add(Radical left, Radical right)
        {
            return new RadicalSum(new Radical[2] { left, right });
        }

        public static RadicalSum Subtract(Radical left, Radical right)
        {
            return Add(left, Negate(right));
        }

        public static Radical Multiply(Radical left, Radical right)
        {
            return new Radical(left.Coefficient * right.Coefficient, left.Radicand * right.Radicand);
        }

        public static Radical Divide(Radical left, Radical right)
        {
            // r' = [c1 * sqrt(r1)] / [c2 * sqrt(r2)]
            //    = [c1 / c2] * sqrt(r1 / r2)
            //    = [c1 / c2] * sqrt(r1 * r2 / r2 * r2)
            //    = [c1 / (c2 * r2)] * sqrt(r1 * r2)
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            return new Radical(left.Coefficient / (right.Coefficient * right.Radicand), left.Radicand * right.Radicand);
        }

        public static bool operator <(Radical left, Radical right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <(Radical left, Rational right)
        {
            return left.CompareTo(new Radical(right, 1)) < 0;
        }

        public static bool operator <(Rational left, Radical right)
        {
            return (new Radical(left, 1)).CompareTo(right) < 0;
        }

        public static bool operator <=(Radical left, Radical right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator <=(Radical left, Rational right)
        {
            return left.CompareTo(new Radical(right, 1)) <= 0;
        }

        public static bool operator <=(Rational left, Radical right)
        {
            return (new Radical(left, 1)).CompareTo(right) <= 0;
        }

        public static bool operator >(Radical left, Radical right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >(Radical left, Rational right)
        {
            return left.CompareTo(new Radical(right, 1)) > 0;
        }

        public static bool operator >(Rational left, Radical right)
        {
            return (new Radical(left, 1)).CompareTo(right) > 0;
        }

        public static bool operator >=(Radical left, Radical right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator >=(Radical left, Rational right)
        {
            return left.CompareTo(new Radical(right, 1)) >= 0;
        }

        public static bool operator >=(Rational left, Radical right)
        {
            return (new Radical(left, 1)).CompareTo(right) >= 0;
        }

        public static bool operator ==(Radical left, Radical right)
        {
            return left.Equals(right);
        }

        public static bool operator ==(Radical left, Rational right)
        {
            return left.Equals(new Radical(right, 1));
        }

        public static bool operator ==(Rational left, Radical right)
        {
            return (new Radical(left, 1)).Equals(right);
        }

        public static bool operator !=(Radical left, Radical right)
        {
            return !left.Equals(right);
        }

        public static bool operator !=(Radical left, Rational right)
        {
            return !left.Equals(new Radical(right, 1));
        }

        public static bool operator !=(Rational left, Radical right)
        {
            return !(new Radical(left, 1)).Equals(right);
        }

        public static Radical operator -(Radical value)
        {
            return Negate(value);
        }

        public static Radical operator +(Radical value)
        {
            return value;
        }

        public static RadicalSum operator +(Radical left, Radical right)
        {
            return Add(left, right);
        }

        public static RadicalSum operator +(Radical left, Rational right)
        {
            return Add(left, new Radical(right, 1));
        }

        public static RadicalSum operator +(Rational left, Radical right)
        {
            return Add(new Radical(left, 1), right);
        }

        public static RadicalSum operator -(Radical left, Radical right)
        {
            return Subtract(left, right);
        }

        public static RadicalSum operator -(Radical left, Rational right)
        {
            return Subtract(left, new Radical(right, 1));
        }

        public static RadicalSum operator -(Rational left, Radical right)
        {
            return Subtract(new Radical(left, 1), right);
        }

        public static Radical operator *(Radical left, Radical right)
        {
            return Multiply(left, right);
        }

        public static Radical operator *(Radical left, Rational right)
        {
            return Multiply(left, new Radical(right, 1));
        }

        public static Radical operator *(Rational left, Radical right)
        {
            return Multiply(new Radical(left, 1), right);
        }

        public static Radical operator /(Radical left, Radical right)
        {
            return Divide(left, right);
        }

        public static Radical operator /(Radical left, Rational right)
        {
            return Divide(left, new Radical(right, 1));
        }

        public static Radical operator /(Rational left, Radical right)
        {
            return Divide(new Radical(left, 1), right);
        }

        /// <summary>
        /// S = simplest form, R = all under the radical
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (Coefficient.IsZero)
                return "0";
            if (Radicand.IsZero)
                return "0";

            string cPart = "";
            string rPart = "";

            if ("S".Equals(format))
            {
                if (!Coefficient.IsOne)
                    cPart = Coefficient.ToString();
                if (!Radicand.IsOne)
                    rPart = "Sqrt(" + Radicand.ToString() + ")";
                if (this == One)
                    cPart = "1";
                if (cPart.Length > 0 && rPart.Length > 0)
                {
                    if (Coefficient.Denominator == 1)
                        cPart = cPart + " * ";
                    else
                        cPart = "(" + cPart + ") * ";
                }
            }
            else if ("R".Equals(format))
            {
                if (Radicand.IsOne)
                    return Coefficient.ToString();
                Rational r = new Rational(Coefficient.Numerator * Coefficient.Numerator * Radicand, Coefficient.Denominator * Coefficient.Denominator);
                int sign = (Coefficient >= 0) ? 1 : -1;
                if (sign == -1)
                    rPart += "-";
                rPart += "Sqrt(" + r.CanonicalForm.ToString() + ")";
            }

            return cPart + rPart;
        }

        public override string ToString()
        {
            return ToString("S", CultureInfo.InvariantCulture);
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

            c_final = simplestCoefficient.CanonicalForm;
            r_final = simplestRadical;
            return;
        }

    }
}
