using Open.Numeric.Primes;
using Rationals;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
[assembly: CLSCompliant(true)]
[assembly: InternalsVisibleTo("Radicals.Test")]

namespace Radicals
{
    [Serializable]
    public readonly struct Radical
        : IComparable, IComparable<Radical>, IEquatable<Radical>, IFormattable
    {
        // R = Coefficient * root[Index](Radicand)
        public readonly Rational coefficient_unsimplified;
        public readonly int index_unsimplified;
        public readonly BigInteger radicand_unsimplified;
        private readonly Rational _coefficient;
        private readonly int _index;
        private readonly BigInteger _radicand;
        private readonly BigInteger[] _radicandPrimeFactors;

        public Rational Coefficient
        {
            get
            {
                return _coefficient;
            }
        }
        public int Index
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
                return _radicand;
            }
        }
        public BigInteger[] RadicandPrimeFactors
        {
            get
            {
                if (_radicandPrimeFactors == null)
                    return null;
                return _radicandPrimeFactors;
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

        public Radical(Rational coefficient, BigInteger radicand, int index)
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
                index_out: out this._index,
                radicandPrimeFactors_out: out this._radicandPrimeFactors);
        }

        private Radical(Rational coefficient, IEnumerable<BigInteger> radicandPrimeFactors, int index)
        {
            if (radicandPrimeFactors == null)
                throw new ArgumentNullException(nameof(radicandPrimeFactors));
            if (index < 1)
                throw new ArgumentException("Index must be a positive integer");
            ToSimplestForm(
                coefficient_in: coefficient,
                radicandPrimeFactors_in: radicandPrimeFactors,
                index_in: index,
                coefficient_out: out this._coefficient,
                radicand_out: out this._radicand,
                index_out: out this._index,
                radicandPrimeFactors_out: out this._radicandPrimeFactors);
            coefficient_unsimplified = this._coefficient;
            radicand_unsimplified = this._radicand;
            index_unsimplified = this._index;
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
                var result = (Rational)Radicand;
                result *= Utilities.Pow(Coefficient, Index);
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

        public int Sign
        {
            get
            {
                return Coefficient.Sign;
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
            var result = (Sign * Rational.Abs(RaisedToIndexPower))
                .CompareTo(other.Sign * Rational.Abs(other.RaisedToIndexPower));
            return result;
        }

        public bool Equals(Radical other)
        {
            return Index == other.Index 
                && Radicand == other.Radicand 
                && Coefficient == other.Coefficient;
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

        public static Radical Sqrt(Radical value)
        {
            // sqrt[(a/b)root[n](c)] = sqrt[(1/b)root[n](a^n*c)] 
            //                    = (1/b)sqrt[b * root[n](a^n * c)] 
            //                    = (1/b)root[2n][(a^n * b^n * c)]
            var radicand = value.Radicand
                * BigInteger.Pow(value.Coefficient.Numerator, value.Index)
                * BigInteger.Pow(value.Coefficient.Denominator, value.Index);
            return new Radical(new Rational(1, value.Coefficient.Denominator), radicand, value.Index * 2);
        }

        public static Radical NthRoot(Rational value, int index)
        {
            var radicand = value.Numerator;
            for (int i = 0; i < index; i++)
            {
                radicand *= value.Denominator;
            }
            return new Radical(new Rational(1, value.Denominator), radicand, index);
        }

        public static Radical NthRoot(Radical value, int index)
        {
            // root[m][(a/b)root[n](c)] = root[m][(1/b)root[n](a^n*c)] 
            //                    = (1/b)root[m][b^(m-1) * root[n](a^n * c)] 
            //                    = (1/b)root[m*n][(a^n * b^[n*(m-1)] * c)]
            var radicand = value.Radicand
                * BigInteger.Pow(value.Coefficient.Numerator, value.Index)
                * BigInteger.Pow(value.Coefficient.Denominator, value.Index * (index - 1));
            return new Radical(new Rational(1, value.Coefficient.Denominator), radicand, index * value.Index);
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

        [CLSCompliant(false)]
        public static implicit operator Radical(sbyte value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(short value)
        {
            return new Radical(new Rational(value), 1);
        }

        [CLSCompliant(false)]
        public static implicit operator Radical(ushort value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(int value)
        {
            return new Radical(new Rational(value), 1);
        }

        [CLSCompliant(false)]
        public static implicit operator Radical(uint value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(long value)
        {
            return new Radical(new Rational(value), 1);
        }

        [CLSCompliant(false)]
        public static implicit operator Radical(ulong value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static Radical Negate(Radical value)
        {
            if (value.IsZero)
                return Zero;
            return new Radical(-value.Coefficient, value.RadicandPrimeFactors, value.Index);
        }

        public static Radical Invert(Radical value)
        {
            if (value.IsZero)
                throw new ArgumentException("Cannot invert zero");
            // Given r_1, find r_2 such that:
            // r_1 * r_2 = 1
            // r_1 = c * root[i](r)
            // r_2 = 1 / [c * root[i](r)] = (1/c) * root[i](r)^(i-1) / r_1
            //     = [1 / (c * r)] * root[i](r^(i-1))
            var newCoefficient = 1 / (value.Coefficient * value.Radicand);
            var newRadicandPrimeFactors = Utilities.Pow(value.RadicandPrimeFactors, value.Index - 1);
            var result = new Radical(newCoefficient, newRadicandPrimeFactors, value.Index);
            return result;
        }

        public static Radical AddCompatible(Radical left, Radical right)
        {
            if (left.Index != right.Index || left.Radicand != right.Radicand)
                throw new Exception("Trying to add compatible radicals that aren't compatible");
            if (left.IsZero)
                return Zero;
            return new Radical(left.Coefficient + right.Coefficient, left.RadicandPrimeFactors, left.Index);
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
            if (left.IsZero || right.IsZero)
                return Zero;
            // Calculate the index and radicand prime factor powers
            var newIndex = left.Index;
            IEnumerable<BigInteger> leftRadicandPrimeFactors = left.RadicandPrimeFactors;
            IEnumerable<BigInteger> rightRadicandPrimeFactors = right.RadicandPrimeFactors;
            if (left.Index != right.Index)
            {
                // Get LCM of the indexes - that will be the new index
                newIndex = Utilities.GetLeastCommonMultiple(left.Index, right.Index);
                var leftPower = newIndex / left.Index;
                var rightPower = newIndex / right.Index;
                leftRadicandPrimeFactors = Utilities.Pow(left.RadicandPrimeFactors, leftPower);
                rightRadicandPrimeFactors = Utilities.Pow(right.RadicandPrimeFactors, rightPower);
            }

            // Calculate the new coefficient and radicand prime factors
            var newCoefficient = left.Coefficient * right.Coefficient;
            var newRadicandPrimeFactors = new List<BigInteger>();
            newRadicandPrimeFactors.AddRange(leftRadicandPrimeFactors);
            newRadicandPrimeFactors.AddRange(rightRadicandPrimeFactors);
            
            return new Radical(newCoefficient, newRadicandPrimeFactors, newIndex);
        }

        public static Radical Divide(Radical left, Radical right)
        {
            if (right.IsZero)
                throw new DivideByZeroException("Cannot divide by zero");
            if (left.IsZero)
                return Zero;
            // r' = [c1 * sqrt(r1)] / [c2 * sqrt(r2)]
            //    = [c1 / c2] * sqrt(r1 / r2)
            //    = [c1 / c2] * sqrt(r1 * r2 / r2 * r2)
            //    = [c1 / (c2 * r2)] * sqrt(r1 * r2)
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

            if (IsZero)
                return 0.ToString();
            if (IsOne)
                return 1.ToString();

            var result = new StringBuilder();

            // Simplest form
            if ("S".Equals(format))
            {
                // Handle coefficient
                // TODO: Pass format through
                if (Coefficient.Denominator.IsOne)
                {
                    if (!Coefficient.Numerator.IsOne
                        || (Coefficient.Numerator.IsOne && Radicand.IsOne))
                    {
                        if (Sign < 0)
                            result.Append("(" + Coefficient.Numerator.ToString() + ")");
                        else
                            result.Append(Coefficient.Numerator.ToString());
                    }   
                }
                else
                    result.Append("(" + Coefficient.ToString() + ")");

                // Handle radicand
                if (Index > 1 && Radicand > 1)
                {
                    if (result.Length > 0)
                        result.Append("*");
                    if (Index == 2)
                        result.Append("Sqrt");
                    else
                        result.Append("Nth-Root[index:" + Index.ToString() + "]");
                    result.Append("(" + Radicand.ToString() + ")");
                }
            }
            else if ("R".Equals(format))
            {
                if (Index == 1)
                {
                    if (Coefficient.Denominator.IsOne)
                    {
                        if (!Coefficient.Numerator.IsOne
                            || (Coefficient.Numerator.IsOne && Radicand.IsOne))
                            result.Append(Coefficient.Numerator.ToString());
                    }
                    else
                        result.Append("(" + Coefficient.ToString() + ")");
                }
                else
                {
                    if (Sign < 0)
                        result.Append("-");
                    // Get the rational for all under the radical
                    if (Index == 2)
                        result.Append("Sqrt");
                    else
                        result.Append("Nth-Root[index:" + Index.ToString() + "]");
                    var radicand = RaisedToIndexPower;
                    if (radicand.Denominator.IsOne)
                        result.Append("(" + BigInteger.Abs(radicand.Numerator).ToString() + ")");
                    else
                        result.Append("(" + Rational.Abs(radicand.CanonicalForm).ToString() + ")");
                }
            }

            return result.ToString();
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
            int index_in,
            out Rational coefficient_out,
            out BigInteger radicand_out,
            out int index_out,
            out BigInteger[] radicandPrimeFactors_out)
        {
            if (coefficient_in.IsZero || radicand_in.IsZero)
            {
                coefficient_out = Rational.Zero;
                radicand_out = BigInteger.Zero;
                index_out = 1;
                radicandPrimeFactors_out = null;
                return;
            }

            var radicandPrimeFactors = Prime.Factors(radicand_in);
            ToSimplestForm(
                coefficient_in: coefficient_in,
                radicandPrimeFactors_in: radicandPrimeFactors,
                index_in: index_in,
                coefficient_out: out coefficient_out,
                radicand_out: out radicand_out,
                index_out: out index_out,
                radicandPrimeFactors_out: out radicandPrimeFactors_out);
        }

        /// <summary>
        /// Simplest form is irreducible radical where the radical is the smallest possible integer
        /// </summary>
        private static void ToSimplestForm(
            Rational coefficient_in,
            IEnumerable<BigInteger> radicandPrimeFactors_in,
            int index_in,
            out Rational coefficient_out,
            out BigInteger radicand_out,
            out int index_out,
            out BigInteger[] radicandPrimeFactors_out)
        {
            if (radicandPrimeFactors_in == null)
                throw new ArgumentNullException(nameof(radicandPrimeFactors_in));
            if (coefficient_in.IsZero)
            {
                coefficient_out = Rational.Zero;
                radicand_out = BigInteger.Zero;
                index_out = 1;
                radicandPrimeFactors_out = null;
                return;
            }

            List<BigInteger> perfectPowerFactors = new List<BigInteger>();
            List<BigInteger> radicandPrimeFactors = new List<BigInteger>();
            var currentCount = 0;
            BigInteger currentFactor = 0;
            foreach (BigInteger factor in radicandPrimeFactors_in.OrderBy(f => f))
            {
                if (factor != currentFactor)
                {
                    if (currentCount > 0)
                    {
                        if (currentFactor == 1)
                            radicandPrimeFactors.Add(currentFactor);
                        else
                            for (int j = 0; j < currentCount; j++)
                                radicandPrimeFactors.Add(currentFactor);
                    }
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
            {
                for (int j = 0; j < currentCount; j++)
                    radicandPrimeFactors.Add(currentFactor);
            }
            else if (currentCount >= index_in)
                throw new Exception("This should not happen");

            // Calculate the coefficient
            Rational simplestCoefficient = coefficient_in;
            foreach (BigInteger perfectPowerFactor in perfectPowerFactors)
                simplestCoefficient *= perfectPowerFactor;

            // Simplify the index
            // Is the radicand a number raised to a power p where p divides the index?
            int simplestIndex = index_in;
            var indexFactors = Prime.Factors(simplestIndex);
            foreach (int indexFactor in indexFactors)
            {
                if (indexFactor == 1)
                    continue;
                if (indexFactor == simplestIndex) // Already extracted perfect powers of the index
                    continue;
                Utilities.IntegerIsPerfectPower(
                    valuePrimeFactors: radicandPrimeFactors,
                    exponent: indexFactor,
                    isPerfectPower: out bool radicandIsFactorPower,
                    nthRootPrimeFactors: out List<BigInteger> radicandFactorRootPrimeFactors);
                if (radicandIsFactorPower)
                {
                    radicandPrimeFactors = radicandFactorRootPrimeFactors;
                    simplestIndex /= indexFactor;
                }
            }

            // Calculate the radicand
            BigInteger simplestRadicand = 1;
            foreach (BigInteger factor in radicandPrimeFactors)
                simplestRadicand *= factor;
            if (simplestRadicand < 2)
                simplestIndex = 1;
            if (simplestIndex == 1)
            {
                simplestCoefficient *= simplestRadicand;
                simplestRadicand = 1;
                radicandPrimeFactors = new List<BigInteger>(new BigInteger[1] { 1 });
            }

            coefficient_out = simplestCoefficient.CanonicalForm;
            radicand_out = simplestRadicand;
            index_out = simplestIndex;
            radicandPrimeFactors_out = radicandPrimeFactors.OrderBy(p => p).ToArray();
            return;
        }

    }
}
