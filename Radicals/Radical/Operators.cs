using Rationals;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Radicals
{
    public readonly partial struct Radical
    {
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

    }
}
