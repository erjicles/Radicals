using Rationals;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly struct RadicalSum
        : IComparable, IComparable<RadicalSum>, IEquatable<RadicalSum>, IFormattable
    {
        // S = r_1 + r_2 + ... + r_n
        // r_i = coefficient_i * root[index_i](radicand_i)
        private readonly Radical[] _radicals;

        public Radical[] Radicals
        {
            get
            {
                if (_radicals == null)
                    return new Radical[1] { Radical.Zero };
                return _radicals;
            }
        }

        public RadicalSum(Rational radicand)
            : this(new Radical(radicand))
        {
        }

        public RadicalSum(Rational coefficient, BigInteger radicand)
        {
            _radicals = new Radical[1] { new Radical(coefficient, radicand) };
        }

        public RadicalSum(Radical radical)
        {
            if (radical == null)
                throw new ArgumentNullException(nameof(radical));
            _radicals = new Radical[1] { radical };
        }

        public RadicalSum(Radical[] radicals)
        {
            if (radicals == null)
                throw new ArgumentNullException(nameof(radicals));
            if (radicals.Length == 0)
                throw new Exception("No radicals provided");
            
            _radicals = SimplifyRadicals(radicals);
        }

        /// <summary>
        /// Zero
        /// </summary>
        public static readonly RadicalSum Zero = new RadicalSum(0, 0);

        /// <summary>
        /// One
        /// </summary>
        public static readonly RadicalSum One = new RadicalSum(1, 1);

        public bool IsRational
        {
            get
            {
                for (int i = 0; i < Radicals.Length; i++)
                    if (Radicals[i].Index > 1 && Radicals[i].Radicand > 1)
                        return false;
                return true;
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

        public Rational[] Coefficients
        {
            get
            {
                var result = new Rational[Radicals.Length];
                for (int i = 0; i < Radicals.Length; i++)
                    result[i] = Radicals[i].Coefficient;
                return result;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is RadicalSum))
                throw new ArgumentException("Invalid type comparison", nameof(obj));
            return CompareTo((RadicalSum)obj);
        }

        public int CompareTo(RadicalSum other)
        {
            return ToDouble().CompareTo(other.ToDouble());
        }

        public bool Equals(RadicalSum other)
        {
            if (other == null)
                return false;

            if (Radicals.Length != other.Radicals.Length)
                return false;

            for (int i = 0; i < Radicals.Length; i++)
                if (Radicals[i] != other.Radicals[i])
                    return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is RadicalSum))
                throw new ArgumentException("Invalid type equality check", nameof(obj));
            return Equals((RadicalSum)obj);
        }

        public override int GetHashCode()
        {
            if (Radicals.Length == 0)
                return 0;
            int h = Radicals[0].GetHashCode();
            for (int i = 1; i < Radicals.Length; i++)
            {
                h = (((h << 5) + h) ^ Radicals[i].GetHashCode());
            }
            return h;
        }

        public double ToDouble()
        {
            double result = 0;
            for (int i = 0; i < Radicals.Length; i++)
            {
                result += Radicals[i].ToDouble();
            }
            return result;
        }

        public Rational ToRational()
        {
            Rational result = 0;
            for (int i = 0; i < Radicals.Length; i++)
            {
                result += (Rational)Radicals[i];
            }
            return result;
        }

        public static explicit operator double(RadicalSum radical)
        {
            return radical.ToDouble();
        }

        public static explicit operator Rational(RadicalSum value)
        {
            return value.ToRational();
        }

        public static implicit operator RadicalSum(Rational value)
        {
            return new RadicalSum(value, 1);
        }

        public static implicit operator RadicalSum(Radical value)
        {
            return new RadicalSum(value);
        }

        public static implicit operator RadicalSum(byte value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        [CLSCompliant(false)]
        public static implicit operator RadicalSum(sbyte value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        public static implicit operator RadicalSum(short value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        [CLSCompliant(false)]
        public static implicit operator RadicalSum(ushort value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        public static implicit operator RadicalSum(int value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        [CLSCompliant(false)]
        public static implicit operator RadicalSum(uint value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        public static implicit operator RadicalSum(long value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        [CLSCompliant(false)]
        public static implicit operator RadicalSum(ulong value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        public static RadicalSum Negate(RadicalSum value)
        {
            Radical[] radicals = new Radical[value.Radicals.Length];
            for (int i = 0; i < value.Radicals.Length; i++)
                radicals[i] = -value.Radicals[i];
            var result = new RadicalSum(radicals);
            return result;
        }

        public static RadicalSum Add(RadicalSum left, RadicalSum right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            var z = new Radical[left.Radicals.Length + right.Radicals.Length];
            left.Radicals.CopyTo(z, 0);
            right.Radicals.CopyTo(z, left.Radicals.Length);
            return new RadicalSum(z);
        }

        public static RadicalSum Subtract(RadicalSum left, RadicalSum right)
        {
            return Add(left, -right);
        }

        public static RadicalSum Multiply(RadicalSum left, RadicalSum right)
        {
            var z = new Radical[left.Radicals.Length * right.Radicals.Length];
            for (int i = 0; i < left.Radicals.Length; i++)
                for (int j = 0; j < right.Radicals.Length; j++)
                    z[(i * right.Radicals.Length) + j] = left.Radicals[i] * right.Radicals[j];
            return new RadicalSum(z);
        }

        public static RadicalSum Divide(RadicalSum left, Radical right)
        {
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            var z = new Radical[left.Radicals.Length];
            for (int i = 0; i < left.Radicals.Length; i++)
                z[i] = left.Radicals[i] / right;
            return new RadicalSum(z);
        }

        public static RadicalSumRatio Divide(RadicalSum left, RadicalSum right)
        {
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            return new RadicalSumRatio(left, right);
        }

        /// <summary>
        /// [Experimental] Multiplies numerator by multiplicative inverse of denominator to return another radical sum.
        /// WARNING: Finding the inverse could take a very long time and consume considerable memory and CPU. Use with caution.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static RadicalSum GroupDivide(RadicalSum left, RadicalSum right)
        {
            if (right.IsZero)
                throw new DivideByZeroException("Cannot divide by zero");
            var right_inverse = RadicalSum.GetRationalizer(right);
            if (right * right_inverse != RadicalSum.One)
                throw new Exception("Failed to find inverse");
            return left * right_inverse;
        }

        public static RadicalSum Pow(RadicalSum left, int exponent)
        {
            // TODO: Optimize
            var result = RadicalSum.One;
            for (int i = 0; i < exponent; i++)
                result *= left;
            return result;
        }

        public static bool operator <(RadicalSum left, RadicalSum right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <(RadicalSum left, Rational right)
        {
            return left.CompareTo(new RadicalSum(right, 1)) < 0;
        }

        public static bool operator <(Rational left, RadicalSum right)
        {
            return (new RadicalSum(left, 1)).CompareTo(right) < 0;
        }

        public static bool operator <(RadicalSum left, Radical right)
        {
            return left.CompareTo(new RadicalSum(right)) < 0;
        }

        public static bool operator <(Radical left, RadicalSum right)
        {
            return (new RadicalSum(left)).CompareTo(right) < 0;
        }

        public static bool operator <=(RadicalSum left, RadicalSum right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator <=(RadicalSum left, Rational right)
        {
            return left.CompareTo(new RadicalSum(right, 1)) <= 0;
        }

        public static bool operator <=(Rational left, RadicalSum right)
        {
            return (new RadicalSum(left, 1)).CompareTo(right) <= 0;
        }

        public static bool operator <=(RadicalSum left, Radical right)
        {
            return left.CompareTo(new RadicalSum(right)) <= 0;
        }

        public static bool operator <=(Radical left, RadicalSum right)
        {
            return (new RadicalSum(left)).CompareTo(right) <= 0;
        }

        public static bool operator >(RadicalSum left, RadicalSum right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >(RadicalSum left, Rational right)
        {
            return left.CompareTo(new RadicalSum(right, 1)) > 0;
        }

        public static bool operator >(Rational left, RadicalSum right)
        {
            return (new RadicalSum(left, 1)).CompareTo(right) > 0;
        }

        public static bool operator >(RadicalSum left, Radical right)
        {
            return left.CompareTo(new RadicalSum(right)) > 0;
        }

        public static bool operator >(Radical left, RadicalSum right)
        {
            return (new RadicalSum(left)).CompareTo(right) > 0;
        }

        public static bool operator >=(RadicalSum left, RadicalSum right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator >=(RadicalSum left, Rational right)
        {
            return left.CompareTo(new RadicalSum(right, 1)) >= 0;
        }

        public static bool operator >=(Rational left, RadicalSum right)
        {
            return (new RadicalSum(left, 1)).CompareTo(right) >= 0;
        }

        public static bool operator >=(RadicalSum left, Radical right)
        {
            return left.CompareTo(new RadicalSum(right)) >= 0;
        }

        public static bool operator >=(Radical left, RadicalSum right)
        {
            return (new RadicalSum(left)).CompareTo(right) >= 0;
        }

        public static bool operator ==(RadicalSum left, RadicalSum right)
        {
            return left.Equals(right);
        }

        public static bool operator ==(RadicalSum left, Rational right)
        {
            return left.Equals(new RadicalSum(right, 1));
        }

        public static bool operator ==(Rational left, RadicalSum right)
        {
            return (new RadicalSum(left, 1)).Equals(right);
        }

        public static bool operator ==(RadicalSum left, Radical right)
        {
            return left.Equals(new RadicalSum(right));
        }

        public static bool operator ==(Radical left, RadicalSum right)
        {
            return (new RadicalSum(left)).Equals(right);
        }

        public static bool operator !=(RadicalSum left, RadicalSum right)
        {
            return !left.Equals(right);
        }

        public static bool operator !=(RadicalSum left, Rational right)
        {
            return !left.Equals(new RadicalSum(right, 1));
        }

        public static bool operator !=(Rational left, RadicalSum right)
        {
            return !(new RadicalSum(left, 1)).Equals(right);
        }

        public static bool operator !=(RadicalSum left, Radical right)
        {
            return !left.Equals(new RadicalSum(right));
        }

        public static bool operator !=(Radical left, RadicalSum right)
        {
            return !(new RadicalSum(left)).Equals(right);
        }

        public static RadicalSum operator -(RadicalSum value)
        {
            return Negate(value);
        }

        public static RadicalSum operator +(RadicalSum value)
        {
            return value;
        }

        public static RadicalSum operator +(RadicalSum left, RadicalSum right)
        {
            return Add(left, right);
        }

        public static RadicalSum operator +(RadicalSum left, Rational right)
        {
            return Add(left, new RadicalSum(right, 1));
        }

        public static RadicalSum operator +(Rational left, RadicalSum right)
        {
            return Add(new RadicalSum(left, 1), right);
        }

        public static RadicalSum operator +(RadicalSum left, Radical right)
        {
            return Add(left, new RadicalSum(right));
        }

        public static RadicalSum operator +(Radical left, RadicalSum right)
        {
            return Add(new RadicalSum(left), right);
        }

        public static RadicalSum operator -(RadicalSum left, RadicalSum right)
        {
            return Subtract(left, right);
        }

        public static RadicalSum operator -(RadicalSum left, Rational right)
        {
            return Subtract(left, new RadicalSum(right, 1));
        }

        public static RadicalSum operator -(Rational left, RadicalSum right)
        {
            return Subtract(new RadicalSum(left, 1), right);
        }

        public static RadicalSum operator -(RadicalSum left, Radical right)
        {
            return Subtract(left, new RadicalSum(right));
        }

        public static RadicalSum operator -(Radical left, RadicalSum right)
        {
            return Subtract(new RadicalSum(left), right);
        }

        public static RadicalSum operator *(RadicalSum left, RadicalSum right)
        {
            return Multiply(left, right);
        }

        public static RadicalSum operator *(RadicalSum left, Rational right)
        {
            return Multiply(left, new RadicalSum(right, 1));
        }

        public static RadicalSum operator *(Rational left, RadicalSum right)
        {
            return Multiply(new RadicalSum(left, 1), right);
        }

        public static RadicalSum operator *(RadicalSum left, Radical right)
        {
            return Multiply(left, new RadicalSum(right));
        }

        public static RadicalSum operator *(Radical left, RadicalSum right)
        {
            return Multiply(new RadicalSum(left), right);
        }

        public static RadicalSum operator /(RadicalSum left, Rational right)
        {
            return Divide(left, new Radical(right, 1));
        }

        public static RadicalSum operator /(RadicalSum left, Radical right)
        {
            return Divide(left, right);
        }

        public static RadicalSumRatio operator /(RadicalSum left, RadicalSum right)
        {
            return Divide(left, right);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var result = new StringBuilder();
            for (int i = 0; i < Radicals.Length; i++)
            {
                if (i > 0)
                    result.Append(" + ");
                result.Append(Radicals[i].ToString(format, formatProvider));
            }
            return result.ToString();
        }

        public override string ToString()
        {
            return ToString("S", CultureInfo.InvariantCulture);
        }

        public static Radical[] SimplifyRadicals(Radical[] radicals)
        {
            if (radicals == null)
                return null;
            if (radicals.Length == 0)
                return new Radical[0];
            if (radicals.Length == 1)
                return radicals;

            var uniqueRadicals = new Dictionary<Tuple<BigInteger, BigInteger>, Radical>();
            for (int i = 0; i < radicals.Length; i++)
            {
                Radical b = radicals[i];
                var key = new Tuple<BigInteger, BigInteger>(b.Radicand, b.Index);
                if (uniqueRadicals.ContainsKey(key))
                    uniqueRadicals[key] = Radical.AddCompatible(uniqueRadicals[key], b);
                else if (!b.IsZero)
                    uniqueRadicals[key] = b;
            }

            Radical[] result =
                uniqueRadicals.Values
                .Where(f => !f.IsZero)
                .OrderBy(f => f.Index)
                .ThenBy(f => f.Radicand)
                .ToArray();
            if (result.Length == 0)
                result = new Radical[1] { Radical.Zero };
            return result;
        }

        /// <summary>
        /// Returns multiplicative inverse, i.e., B such that this * B = 1
        /// </summary>
        /// <returns></returns>
        public static RadicalSum GetRationalizer(RadicalSum value)
        {
            // Method derived from:
            // Rationalizing Denominators
            // Allan Berele and Stefan Catoiu
            // https://www.jstor.org/stable/10.4169/mathmaga.88.issue-2
            // See Example 9
            
            if (value == 0)
                throw new Exception("Cannot get rationalization of zero");

            // Solve for the largest field extensions in turn
            // S = r_1 + r_2 + ... + r_n
            // r_n = S - r_1 - r_2 - ... - r_(n-1)
            // r_n^2 = [S - r_1 - r_2 - ... - r_(n-1)]^2


            // Create polynomial in S
            // S - r_1 - r_2 - ... - r_n = 0
            var term1 = new Polynomials.PolynomialTerm(1, 1);
            var term0 = new Polynomials.PolynomialTerm(-value, 0);
            var p = new Polynomials.Polynomial(new Polynomials.PolynomialTerm[2] { term0, term1 });

            // Get the set of unique indexes and radicands for each index
            var uniquePrimeNthRoots = p.GetUniquePrimeNthRoots();

            // Main loop
            for (int i = uniquePrimeNthRoots.Length - 1; i >= 0; i--)
            {
                var currentPrimeNthRoot = uniquePrimeNthRoots[i];
                if (currentPrimeNthRoot.Item1 < 2) // Index
                    continue;
                if (currentPrimeNthRoot.Item2 < 2) // Radicand
                    continue;
                // Solve for largest radicand
                // p = p_reduced + p_extract = 0
                // p_extract = root[currentIndex](currentRadicand) * p' for some p'
                Polynomials.Polynomial p_reduced;
                Polynomials.Polynomial p_extract;
                p.ExtractTermsContainingNthRoot(
                    radicand: currentPrimeNthRoot.Item2,
                    index: currentPrimeNthRoot.Item1,
                    p_reduced: out p_reduced,
                    p_extract: out p_extract);

                // Remove root[currentIndex](currentRadicand) to keep p' === p_extract_coefficient
                var r = new Radical(1, currentPrimeNthRoot.Item2, currentPrimeNthRoot.Item1);
                var p_extract_coefficient = p_extract /= r;

                // p_reduced = -p_extract
                // p_reduced^currentIndex = (-p_extract)^currentIndex
                // p_reduced^currentIndex = (-p_extract_coefficient)^currentIndex * currentRadicand
                // p => p_reduced^currentIndex - currentRadicand * (-p_extract_coefficient)^currentIndex = 0
                var left = Polynomials.Polynomial.Pow(p_reduced, currentPrimeNthRoot.Item1);
                var right = Polynomials.Polynomial.Pow(-p_extract, currentPrimeNthRoot.Item1) * currentPrimeNthRoot.Item2;
                p = left - right;
            }

            // We now have a polynomial in S with all radicals removed:
            // a_0 + a_1 * S + a_2 * S^2 + ... + a_n * S^n = 0
            // Radicalizer then becomes:
            // -a_0 / S = a_1 + a_2 * S + ... + a_n * S^(n-1)
            //
            // 1   a_1 + a_2 * S + ... + a_n * S^(n-1)
            // - = -----------------------------------
            // S                 -a_0                 
            var constantTerm = p.GetConstantTerm();
            var constantP = new Polynomials.Polynomial(constantTerm);
            p -= constantP;
            if (!p.IsDivisibleByX)
                throw new Exception("All terms should have at least degree 1");
            if (!constantTerm.IsRational)
                throw new Exception("No radicals should remain in any terms");
            p = Polynomials.Polynomial.DivideByX(p);
            p /= (Rational)(-constantTerm);

            var rationalizer = p.EvaluateAt(value);

            return rationalizer;
        }

    }
}
