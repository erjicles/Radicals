﻿using Open.Numeric.Primes;
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
        // R = Coefficient * root(Degree)(Radicand)
        public readonly Rational coefficient_unsimplified;
        public readonly BigInteger index_unsimplified;
        public readonly BigInteger radicand_unsimplified;
        private readonly Rational _coefficient;
        private readonly BigInteger _index;
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
        public BigInteger Index
        {
            get
            {
                if (_index < 1)
                    return 1;
                return _index;
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
            : this(coefficient, radicand, 2)
        {
        }

        public Radical(Rational coefficient, BigInteger radicand, BigInteger index)
        {
            if (radicand < 0)
                throw new ArgumentException("Cannot have negative radicand");
            if (index < 1)
                throw new ArgumentException("Index must be a positive integer");
            coefficient_unsimplified = coefficient;
            index_unsimplified = index;
            radicand_unsimplified = radicand;
            ToSimplestForm(
                coefficient_in: coefficient,
                radicand_in: radicand,
                index_in: index,
                coefficient_out: out this._coefficient,
                radicand_out: out this._radicand,
                index_out: out this._index);
        }

        /// <summary>
        /// Zero
        /// </summary>
        public static readonly Radical Zero = new Radical(0, 0, 1);

        /// <summary>
        /// One
        /// </summary>
        public static readonly Radical One = new Radical(1, 1, 1);

        public bool IsRational
        {
            get
            {
                if (Radicand < 2)
                    return true;
                return false;
            }
        }

        public Rational RaisedToIndexPower
        {
            get
            {
                var result = (Rational)1;
                for (int i = 0; i < Index; i++)
                    result *= Coefficient;
                result *= Radicand;
                return result;
            }
        }

        public bool IsOne
        {
            get { return this == One; }
        }

        public bool IsZero
        {
            get { return this == Zero; }
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

            return RaisedToIndexPower.CompareTo(other.RaisedToIndexPower);
        }

        public bool Equals(Radical other)
        {
            return CompareTo(other) == 0;
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
            int h3 = (((h1 << 5) + h1) ^ h2);
            int h4 = Index.GetHashCode();
            return (((h3 << 5) + h3) ^ h4);
        }

        public bool GetIsCompatibleRadical(Radical other)
        {
            return Index == other.Index && Radicand == other.Radicand;
        }

        public double ToDouble()
        {
            if (Index == 1)
                return (double)(Coefficient * Radicand);
            if (Index == 2)
                return (double)Coefficient * Math.Sqrt((double)Radicand);
            return (double)Coefficient 
                * Math.Pow((double)Radicand, (double)(new Rational(1, Index)));
        }

        public Rational ToRational()
        {
            // TODO: Handle radical part?
            return Coefficient;
        }

        public static Radical Sqrt(Rational value)
        {
            return new Radical(value);
        }

        public static Radical NthRoot(Rational value, BigInteger index)
        {
            var radicand = value.Numerator;
            for (int i = 0; i < index; i++)
            {
                radicand *= value.Denominator;
            }
            return new Radical(new Rational(1, value.Denominator), radicand, index);
        }

        public static explicit operator double(Radical value)
        {
            return value.ToDouble();
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
            return new Radical(-value.Coefficient, value.Radicand, value.Index);
        }

        public static Radical Invert(Radical value)
        {
            // Given r_1, find r_2 such that:
            // r_1 * r_2 = 1
            // r_1 = c * root[i](r)
            // r_2 = 1 / [c * root[i](r)] = (1/c) * root[i](r)^(i-1) / r_1
            //     = [1 / (c * r)] * root[i](r^(i-1))
            var coefficient = (Rational)1 / (value.Coefficient * value.Radicand);
            var radicand = BigInteger.Pow(value.Radicand, (int)(value.Index - 1));
            var result = new Radical(coefficient, radicand, value.Index);
            return result;
        }

        public static Radical AddCompatible(Radical left, Radical right)
        {
            if (left.Radicand != right.Radicand)
                throw new Exception("Trying to add compatible radicals that aren't compatible");
            return new Radical(left.Coefficient + right.Coefficient, left.Radicand, left.Index);
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
            if (left.Index == right.Index)
                return new Radical(left.Coefficient * right.Coefficient, left.Radicand * right.Radicand, left.Index);
            // Get LCM of the indexes - that will be the new index
            var newIndex = Utilities.GetLeastCommonMultiple(left.Index, right.Index);
            var leftPower = newIndex / left.Index;
            var rightPower = newIndex / right.Index;
            var leftRadicand = BigInteger.Pow(left.Radicand, (int)leftPower);
            var rightRadicand = BigInteger.Pow(right.Radicand, (int)rightPower);
            return new Radical(left.Coefficient * right.Coefficient, leftRadicand * rightRadicand, newIndex);
        }

        public static Radical Divide(Radical left, Radical right)
        {
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            // r' = [c1 * sqrt(r1)] / [c2 * sqrt(r2)]
            //    = [c1 / c2] * sqrt(r1 / r2)
            //    = [c1 / c2] * sqrt(r1 * r2 / r2 * r2)
            //    = [c1 / (c2 * r2)] * sqrt(r1 * r2)
            if (left.Index == right.Index)
                return new Radical(left.Coefficient / (right.Coefficient * right.Radicand), left.Radicand * right.Radicand, left.Index);
            // Convert to multiplication problem by removing denominator
            var rightInverse = Radical.Invert(right);
            return Multiply(left, rightInverse);
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
            if (format == null)
                format = "S";

            string cPart = "";
            string rPart = "";

            if ("S".Equals(format))
            {
                if (!Coefficient.IsOne)
                    cPart = Coefficient.ToString();
                if (!Radicand.IsOne)
                {
                    if (Index == 2)
                        rPart += "Sqrt";
                    else
                        rPart += "Nth-Root[n:" + Index.ToString() + "]";
                    rPart = "(" + Radicand.ToString() + ")";
                } 
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
                if (Index == 2)
                    rPart += "Sqrt";
                else
                    rPart += "Nth-Root[n:" + Index.ToString() + "]";
                rPart += "(" + r.CanonicalForm.ToString() + ")";
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
            Rational coefficient_in,
            BigInteger radicand_in,
            BigInteger index_in,
            out Rational coefficient_out,
            out BigInteger radicand_out,
            out BigInteger index_out)
        {
            if (coefficient_in.IsZero || radicand_in.IsZero)
            {
                coefficient_out = Rational.Zero;
                coefficient_out = BigInteger.Zero;
                index_out = 1;
                return;
            }

            List<BigInteger> perfectPowerFactors = new List<BigInteger>();
            List<BigInteger> finalFactors = new List<BigInteger>();
            var currentCount = 0;
            BigInteger currentFactor = 0;
            foreach (BigInteger factor in Prime.Factors(radicand_in).OrderBy(f => f))
            {
                if (factor != currentFactor)
                {
                    if (currentCount > 0)
                        finalFactors.Add(currentFactor);
                    currentCount = 0;
                    currentFactor = factor;
                }
                currentCount++;
                if (currentCount == index_in)
                {
                    perfectPowerFactors.Add(currentFactor);
                    currentCount = 0;
                    currentFactor = 0;
                }
                else if (currentCount > index_in)
                    throw new Exception("This shouldn't happen");
            }
            if (currentCount > 0 && currentCount < index_in)
                finalFactors.Add(currentFactor);
            else if (currentCount >= index_in)
                throw new Exception("This should not happen");

            Rational simplestCoefficient = coefficient_in;
            BigInteger simplestRadicand = 1;
            BigInteger simplestIndex = index_in;

            foreach (BigInteger perfectPowerFactor in perfectPowerFactors)
                simplestCoefficient *= perfectPowerFactor;
            foreach (BigInteger factor in finalFactors)
                simplestRadicand *= factor;
            if (simplestRadicand < 2)
                simplestIndex = 1;

            coefficient_out = simplestCoefficient.CanonicalForm;
            radicand_out = simplestRadicand;
            index_out = simplestIndex;
            return;
        }

    }
}
