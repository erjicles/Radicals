using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals.Polynomials
{
    internal readonly struct PolynomialTerm
        : IEquatable<PolynomialTerm>
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
                if (_coefficient == null)
                    return RadicalSum.Zero;
                return _coefficient;
            }
        }

        public PolynomialTerm(RadicalSum constant)
            : this(constant, 0)
        {
        }

        public PolynomialTerm(RadicalSum coefficient, int degree)
        {
            if (coefficient == null)
                throw new ArgumentNullException(nameof(coefficient));
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
        

        public static PolynomialTerm Negate(PolynomialTerm value)
        {
            var result = new PolynomialTerm(-value.Coefficient, value.Degree);
            return result;
        }

        public static Polynomial Add(PolynomialTerm left, PolynomialTerm right)
        {
            return new Polynomial(new PolynomialTerm[2] { left, right });
        }

        public static Polynomial Subtract(PolynomialTerm left, PolynomialTerm right)
        {
            return Add(left, -right);
        }

        public static PolynomialTerm Multiply(PolynomialTerm left, PolynomialTerm right)
        {
            return new PolynomialTerm(left.Coefficient * right.Coefficient, left.Degree + right.Degree);
        }

        public bool Equals(PolynomialTerm other)
        {
            return Degree == other.Degree && Coefficient == other.Coefficient;
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
    }
}
