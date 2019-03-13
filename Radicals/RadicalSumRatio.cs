using Rationals;
using System;
using System.Globalization;
using System.Numerics;

namespace Radicals
{
    public readonly struct RadicalSumRatio
        : IComparable, IComparable<RadicalSumRatio>, IEquatable<RadicalSumRatio>, IFormattable
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

        /// <summary>
        /// Zero
        /// </summary>
        public static readonly RadicalSumRatio Zero = new RadicalSumRatio(RadicalSum.Zero, RadicalSum.One);

        /// <summary>
        /// One
        /// </summary>
        public static readonly RadicalSumRatio One = new RadicalSumRatio(RadicalSum.One, RadicalSum.One);

        public bool IsRational
        {
            get
            {
                if (Numerator.Radicals.Length == 1)
                    if (Denominator.Radicals.Length == 1)
                        if (Numerator.Radicals[0].Radicand < 2)
                            if (Denominator.Radicals[0].Radicand < 2)
                                return true;
                return false;
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

        public Rational[] CoefficientsNumerator
        {
            get
            {
                return Numerator.Coefficients;
            }
        }

        public Rational[] CoefficientsDenominator
        {
            get
            {
                return Denominator.Coefficients;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is RadicalSumRatio))
                throw new ArgumentException("Invalid type comparison", nameof(obj));
            return CompareTo((RadicalSumRatio)obj);
        }

        public int CompareTo(RadicalSumRatio other)
        {
            return ToDouble().CompareTo(other.ToDouble());
        }

        public bool Equals(RadicalSumRatio other)
        {
            if (other == null)
                return false;
            return Numerator == other.Numerator
                && Denominator == other.Denominator;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is RadicalSumRatio))
                throw new ArgumentException("Invalid type equality check", nameof(obj));
            return Equals((RadicalSumRatio)obj);
        }

        public override int GetHashCode()
        {
            int h1 = Numerator.GetHashCode();
            int h2 = Denominator.GetHashCode();
            return (((h1 << 5) + h1) ^ h2);
        }

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

        public static implicit operator RadicalSumRatio(Rational value)
        {
            return new RadicalSumRatio(value, 1);
        }

        public static implicit operator RadicalSumRatio(byte value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(sbyte value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(short value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(ushort value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(int value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(uint value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(long value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(ulong value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static RadicalSumRatio Negate(RadicalSumRatio value)
        {
            return new RadicalSumRatio(-value.Numerator, value.Denominator);
        }

        public static RadicalSumRatio Invert(RadicalSumRatio value)
        {
            return new RadicalSumRatio(value.Denominator, value.Numerator);
        }

        public static RadicalSumRatio Add(
            RadicalSumRatio left,
            RadicalSumRatio right)
        {
            // n1   n2   (n1 * d2) + (n2 * d1)
            // -- + -- = ---------------------
            // d1   d2          d1 * d2
            RadicalSum numerator =
                (left.Numerator * right.Denominator)
                + (right.Numerator * left.Denominator);
            RadicalSum denominator = left.Denominator * right.Denominator;
            return new RadicalSumRatio(numerator, denominator);
        }

        public static RadicalSumRatio Subtract(
            RadicalSumRatio left,
            RadicalSumRatio right)
        {
            return Add(left, -right);
        }

        public static RadicalSumRatio Multiply(
            RadicalSumRatio left,
            RadicalSumRatio right)
        {
            //               C1n   C2n   commonFactorLeft * C1n_reduced   commonFactorRight * C2n_reduced
            // R = R1 * R2 = --- * --- = ------------------------------ * -------------------------------
            //               C1d   C2d                      C1d                               C2d        
            //
            //     commonFactor * c1n_reduced * c2n_reduced
            //   = ----------------------------------------
            //                            c2d * c1d       
            //
            // ci_reduced === ci / commonFactori
            // commonFactor === commonFactorLeft * commonFactorRight;
            // Simplest form puts all common factors in numerator
            var commonFactorLeft = Utilities.GetCommonFactor(left.Numerator.Coefficients);
            var commonFactorRight = Utilities.GetCommonFactor(right.Numerator.Coefficients);
            var commonFactor = commonFactorLeft * commonFactorRight;
            var c_left_n_reduced = left.Numerator / commonFactorLeft;
            var c_right_n_reduced = right.Numerator / commonFactorRight;
            var d_left = left.Denominator;
            var d_right = right.Denominator;
            if (c_left_n_reduced == d_right)
            {
                c_left_n_reduced = RadicalSum.One;
                d_right = RadicalSum.One;
            }
            if (c_right_n_reduced == d_left)
            {
                c_right_n_reduced = RadicalSum.One;
                d_left = RadicalSum.One;
            }

            RadicalSum numerator = commonFactor * c_left_n_reduced * c_right_n_reduced;
            RadicalSum denominator = d_left * d_right;

            var result = new RadicalSumRatio(numerator, denominator);
            return result;
        }

        public static RadicalSumRatio Divide(
            RadicalSumRatio left,
            RadicalSumRatio right)
        {
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            return Multiply(left, Invert(right));
        }

        public static bool operator <(RadicalSumRatio left, RadicalSumRatio right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <(RadicalSumRatio left, Rational right)
        {
            return left.CompareTo(new RadicalSumRatio(right, 1)) < 0;
        }

        public static bool operator <(Rational left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left, 1)).CompareTo(right) < 0;
        }

        public static bool operator <(RadicalSumRatio left, Radical right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) < 0;
        }

        public static bool operator <(Radical left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) < 0;
        }

        public static bool operator <(RadicalSumRatio left, RadicalSum right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) < 0;
        }

        public static bool operator <(RadicalSum left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) < 0;
        }

        public static bool operator <=(RadicalSumRatio left, RadicalSumRatio right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator <=(RadicalSumRatio left, Rational right)
        {
            return left.CompareTo(new RadicalSumRatio(right, 1)) <= 0;
        }

        public static bool operator <=(Rational left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left, 1)).CompareTo(right) <= 0;
        }

        public static bool operator <=(RadicalSumRatio left, Radical right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) <= 0;
        }

        public static bool operator <=(Radical left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) <= 0;
        }

        public static bool operator <=(RadicalSumRatio left, RadicalSum right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) <= 0;
        }

        public static bool operator <=(RadicalSum left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) <= 0;
        }

        public static bool operator >(RadicalSumRatio left, RadicalSumRatio right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >(RadicalSumRatio left, Rational right)
        {
            return left.CompareTo(new RadicalSumRatio(right, 1)) > 0;
        }

        public static bool operator >(Rational left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left, 1)).CompareTo(right) > 0;
        }

        public static bool operator >(RadicalSumRatio left, Radical right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) > 0;
        }

        public static bool operator >(Radical left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) > 0;
        }

        public static bool operator >(RadicalSumRatio left, RadicalSum right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) > 0;
        }

        public static bool operator >(RadicalSum left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) > 0;
        }

        public static bool operator >=(RadicalSumRatio left, RadicalSumRatio right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator >=(RadicalSumRatio left, Rational right)
        {
            return left.CompareTo(new RadicalSumRatio(right, 1)) >= 0;
        }

        public static bool operator >=(Rational left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left, 1)).CompareTo(right) >= 0;
        }

        public static bool operator >=(RadicalSumRatio left, Radical right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) >= 0;
        }

        public static bool operator >=(Radical left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) >= 0;
        }

        public static bool operator >=(RadicalSumRatio left, RadicalSum right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) >= 0;
        }

        public static bool operator >=(RadicalSum left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) >= 0;
        }

        public static bool operator ==(RadicalSumRatio left, RadicalSumRatio right)
        {
            return left.Equals(right);
        }

        public static bool operator ==(RadicalSumRatio left, Rational right)
        {
            return left.Equals(new RadicalSumRatio(right, 1));
        }

        public static bool operator ==(Rational left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left, 1)).Equals(right);
        }

        public static bool operator ==(RadicalSumRatio left, Radical right)
        {
            return left.Equals(new RadicalSumRatio(right));
        }

        public static bool operator ==(Radical left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).Equals(right);
        }

        public static bool operator ==(RadicalSumRatio left, RadicalSum right)
        {
            return left.Equals(new RadicalSumRatio(right));
        }

        public static bool operator ==(RadicalSum left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).Equals(right);
        }

        public static bool operator !=(RadicalSumRatio left, RadicalSumRatio right)
        {
            return !left.Equals(right);
        }

        public static bool operator !=(RadicalSumRatio left, Rational right)
        {
            return !left.Equals(new RadicalSumRatio(right, 1));
        }

        public static bool operator !=(Rational left, RadicalSumRatio right)
        {
            return !(new RadicalSumRatio(left, 1)).Equals(right);
        }

        public static bool operator !=(RadicalSumRatio left, Radical right)
        {
            return !left.Equals(new RadicalSumRatio(right));
        }

        public static bool operator !=(Radical left, RadicalSumRatio right)
        {
            return !(new RadicalSumRatio(left)).Equals(right);
        }

        public static bool operator !=(RadicalSumRatio left, RadicalSum right)
        {
            return !left.Equals(new RadicalSumRatio(right));
        }

        public static bool operator !=(RadicalSum left, RadicalSumRatio right)
        {
            return !(new RadicalSumRatio(left)).Equals(right);
        }

        public static RadicalSumRatio operator -(RadicalSumRatio value)
        {
            return Negate(value);
        }

        public static RadicalSumRatio operator +(RadicalSumRatio value)
        {
            return value;
        }

        public static RadicalSumRatio operator +(
            RadicalSumRatio left,
            RadicalSumRatio right)
        {
            return Add(left, right);
        }

        public static RadicalSumRatio operator +(
            RadicalSumRatio left,
            Rational right)
        {
            return Add(left, new RadicalSumRatio(right, 1));
        }

        public static RadicalSumRatio operator +(
            Rational left,
            RadicalSumRatio right)
        {
            return Add(new RadicalSumRatio(left, 1), right);
        }

        public static RadicalSumRatio operator +(
            RadicalSumRatio left,
            Radical right)
        {
            return Add(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator +(
            Radical left,
            RadicalSumRatio right)
        {
            return Add(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator +(
            RadicalSumRatio left,
            RadicalSum right)
        {
            return Add(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator +(
            RadicalSum left,
            RadicalSumRatio right)
        {
            return Add(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator -(
            RadicalSumRatio left,
            RadicalSumRatio right)
        {
            return Subtract(left, right);
        }

        public static RadicalSumRatio operator -(
            RadicalSumRatio left,
            Rational right)
        {
            return Subtract(left, new RadicalSumRatio(right, 1));
        }

        public static RadicalSumRatio operator -(
            Rational left,
            RadicalSumRatio right)
        {
            return Subtract(new RadicalSumRatio(left, 1), right);
        }

        public static RadicalSumRatio operator -(
            RadicalSumRatio left,
            Radical right)
        {
            return Subtract(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator -(
            Radical left,
            RadicalSumRatio right)
        {
            return Subtract(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator -(
            RadicalSumRatio left,
            RadicalSum right)
        {
            return Subtract(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator -(
            RadicalSum left,
            RadicalSumRatio right)
        {
            return Subtract(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator *(
            RadicalSumRatio left,
            RadicalSumRatio right)
        {
            return Multiply(left, right);
        }

        public static RadicalSumRatio operator *(
            RadicalSumRatio left,
            Rational right)
        {
            return Multiply(left, new RadicalSumRatio(right, 1));
        }

        public static RadicalSumRatio operator *(
            Rational left,
            RadicalSumRatio right)
        {
            return Multiply(new RadicalSumRatio(left, 1), right);
        }

        public static RadicalSumRatio operator *(
            RadicalSumRatio left,
            Radical right)
        {
            return Multiply(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator *(
            Radical left,
            RadicalSumRatio right)
        {
            return Multiply(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator *(
            RadicalSumRatio left,
            RadicalSum right)
        {
            return Multiply(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator *(
            RadicalSum left,
            RadicalSumRatio right)
        {
            return Multiply(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator /(
            RadicalSumRatio left,
            RadicalSumRatio right)
        {
            return Divide(left, right);
        }

        public static RadicalSumRatio operator /(
            RadicalSumRatio left,
            Rational right)
        {
            return Divide(left, new RadicalSumRatio(right, 1));
        }

        public static RadicalSumRatio operator /(
            Rational left,
            RadicalSumRatio right)
        {
            return Divide(new RadicalSumRatio(left, 1), right);
        }

        public static RadicalSumRatio operator /(
            RadicalSumRatio left,
            Radical right)
        {
            return Divide(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator /(
            Radical left,
            RadicalSumRatio right)
        {
            return Divide(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator /(
            RadicalSumRatio left,
            RadicalSum right)
        {
            return Divide(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator /(
            RadicalSum left,
            RadicalSumRatio right)
        {
            return Divide(new RadicalSumRatio(left), right);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (RadicalSum.One == Denominator)
                return Numerator.ToString(format, formatProvider);
            var result = "[" + Numerator.ToString(format, formatProvider) 
                + "] / [" + Denominator.ToString(format, formatProvider) + "]";
            return result;
        }

        public override string ToString()
        {
            return ToString("S", CultureInfo.InvariantCulture);
        }

        private static void ToSimplestForm(
            RadicalSum n_in,
            RadicalSum d_in,
            out RadicalSum n_out,
            out RadicalSum d_out)
        {
            // First extract common factors from numerator and denominator:
            // N = common_factor_n * (N/common_factor_n); n_reduced === N/common_factor_n
            // D = common_factor_d * (D/common_factor_d); d_reduced === D/common_factor_d
            Rational common_factor_n = Utilities.GetCommonFactor(n_in.Coefficients);
            Rational common_factor_d = Utilities.GetCommonFactor(d_in.Coefficients);
            var n_reduced = n_in * Rational.Invert(common_factor_n);
            var d_reduced = d_in * Rational.Invert(common_factor_d);

            //     N   common_factor_n * (N/common_factor_n)   common_factor_n * n_reduced                   n_reduced
            // C = - = ------------------------------------- = --------------------------- = common_factor * ---------
            //     D   common_factor_d * (D/common_factor_d)   common_factor_d * d_reduced                   d_reduced
            //
            // common_factor === common_factor_n / common_factor_d
            var common_factor = common_factor_n / common_factor_d;

            if (n_reduced == d_reduced)
            {
                n_out = common_factor * RadicalSum.One;
                d_out = RadicalSum.One;
            }
            else if (d_reduced.Radicals.Length == 1)
            {
                n_out = common_factor * n_reduced / d_reduced.Radicals[0];
                d_out = RadicalSum.One;
            }
            else
            {
                n_out = common_factor * n_reduced;
                d_out = d_reduced;
            }
        }
    }
}
