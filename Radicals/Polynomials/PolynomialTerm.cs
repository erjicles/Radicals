using System;
using System.Globalization;
using System.Text;

namespace Radicals.Polynomials
{
    internal readonly struct PolynomialTerm
        : IEquatable<PolynomialTerm>, IFormattable
    {
        private readonly int _degree;
        private readonly RadicalSum _coefficient;
        public int Degree
        {
            get
            {
                return _degree;
            }
        }
        public RadicalSum Coefficient
        {
            get
            {
                return _coefficient;
            }
        }

        public PolynomialTerm(RadicalSum constant)
            : this(constant, 0)
        {
        }

        public PolynomialTerm(RadicalSum coefficient, int degree)
        {
            if (degree < 0)
                throw new ArgumentException("Degree cannot be negative", nameof(degree));
            _coefficient = coefficient;
            if (coefficient == RadicalSum.Zero)
                _degree = 0;
            else
                _degree = degree;
        }

        public static PolynomialTerm Zero = new PolynomialTerm(RadicalSum.Zero, 0);
        public static PolynomialTerm One = new PolynomialTerm(RadicalSum.One, 0);

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is PolynomialTerm))
                throw new ArgumentException("Invalid type equality check", nameof(obj));
            return Equals((PolynomialTerm)obj);
        }

        public bool Equals(PolynomialTerm other)
        {
            return Degree == other.Degree && Coefficient == other.Coefficient;
        }

        public override int GetHashCode()
        {
            int h1 = Coefficient.GetHashCode();
            int h2 = Degree.GetHashCode();
            return (((h1 << 5) + h1) ^ h2);
        }

        public static PolynomialTerm Negate(PolynomialTerm value)
        {
            var result = new PolynomialTerm(-value.Coefficient, value.Degree);
            return result;
        }

        public static Polynomial Add(PolynomialTerm left, PolynomialTerm right)
        {
            return new Polynomial(new PolynomialTerm[2] { left, right });
        }

        public static PolynomialTerm AddCompatible(PolynomialTerm left, PolynomialTerm right)
        {
            if (left.Degree != right.Degree)
                throw new ArgumentException("Degrees don't match");
            return new PolynomialTerm(left.Coefficient + right.Coefficient, left.Degree);
        }

        public static Polynomial Subtract(PolynomialTerm left, PolynomialTerm right)
        {
            return Add(left, -right);
        }

        public static PolynomialTerm Multiply(PolynomialTerm left, PolynomialTerm right)
        {
            return new PolynomialTerm(left.Coefficient * right.Coefficient, left.Degree + right.Degree);
        }

        public static PolynomialTerm Pow(PolynomialTerm left, int exponent)
        {
            var result = PolynomialTerm.One;
            for (int i = 0; i < exponent; i++)
                result *= left;
            return result;
        }

        public static PolynomialTerm operator -(PolynomialTerm value)
        {
            return Negate(value);
        }

        public static PolynomialTerm operator +(PolynomialTerm value)
        {
            return value;
        }

        public static Polynomial operator +(PolynomialTerm left, PolynomialTerm right)
        {
            return Add(left, right);
        }

        public static Polynomial operator -(PolynomialTerm left, PolynomialTerm right)
        {
            return Subtract(left, right);
        }

        public static PolynomialTerm operator *(PolynomialTerm left, PolynomialTerm right)
        {
            return Multiply(left, right);
        }

        public static bool operator ==(PolynomialTerm left, PolynomialTerm right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PolynomialTerm left, PolynomialTerm right)
        {
            return !left.Equals(right);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var result = new StringBuilder();
            if (Coefficient.IsZero)
                return "0";
            if (Coefficient.IsOne)
            {
                if (Degree == 0)
                    return "1";
                else if (Degree == 1)
                    return "X";
                else
                    return "X^" + Degree.ToString();
            }
            if (Degree == 0)
                return Coefficient.ToString();
            else if (Degree == 1)
                return "(" + Coefficient.ToString(format, formatProvider) + ")*X";
            else
                return "(" + Coefficient.ToString(format, formatProvider) + ")*X^" + Degree.ToString();
        }

        public override string ToString()
        {
            return ToString(null, CultureInfo.InvariantCulture);
        }
    }
}
