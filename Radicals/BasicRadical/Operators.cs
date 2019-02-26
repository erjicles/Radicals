using Rationals;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Radicals
{
    public readonly partial struct BasicRadical
    {
        public static bool operator <(BasicRadical left, BasicRadical right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <(BasicRadical left, Rational right)
        {
            return left.CompareTo(new BasicRadical(right, 1)) < 0;
        }

        public static bool operator <(Rational left, BasicRadical right)
        {
            return (new BasicRadical(left, 1)).CompareTo(right) < 0;
        }

        public static bool operator <=(BasicRadical left, BasicRadical right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator <=(BasicRadical left, Rational right)
        {
            return left.CompareTo(new BasicRadical(right, 1)) <= 0;
        }

        public static bool operator <=(Rational left, BasicRadical right)
        {
            return (new BasicRadical(left, 1)).CompareTo(right) <= 0;
        }

        public static bool operator >(BasicRadical left, BasicRadical right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >(BasicRadical left, Rational right)
        {
            return left.CompareTo(new BasicRadical(right, 1)) > 0;
        }

        public static bool operator >(Rational left, BasicRadical right)
        {
            return (new BasicRadical(left, 1)).CompareTo(right) > 0;
        }

        public static bool operator >=(BasicRadical left, BasicRadical right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator >=(BasicRadical left, Rational right)
        {
            return left.CompareTo(new BasicRadical(right, 1)) >= 0;
        }

        public static bool operator >=(Rational left, BasicRadical right)
        {
            return (new BasicRadical(left, 1)).CompareTo(right) >= 0;
        }

        public static bool operator ==(BasicRadical left, BasicRadical right)
        {
            return left.Equals(right);
        }

        public static bool operator ==(BasicRadical left, Rational right)
        {
            return left.Equals(new BasicRadical(right, 1));
        }

        public static bool operator ==(Rational left, BasicRadical right)
        {
            return (new BasicRadical(left, 1)).Equals(right);
        }

        public static bool operator !=(BasicRadical left, BasicRadical right)
        {
            return !left.Equals(right);
        }

        public static bool operator !=(BasicRadical left, Rational right)
        {
            return !left.Equals(new BasicRadical(right, 1));
        }

        public static bool operator !=(Rational left, BasicRadical right)
        {
            return !(new BasicRadical(left, 1)).Equals(right);
        }

        public static BasicRadical operator -(BasicRadical value)
        {
            return Negate(value);
        }

        public static BasicRadical operator +(BasicRadical value)
        {
            return value;
        }

        public static BasicRadical[] operator +(BasicRadical left, BasicRadical right)
        {
            return Add(left, right);
        }

        public static BasicRadical[] operator +(BasicRadical left, BasicRadical[] right)
        {
            return Add(left, right);
        }

        public static BasicRadical[] operator +(BasicRadical[] left, BasicRadical right)
        {
            return Add(right, left);
        }

        public static BasicRadical[] operator -(BasicRadical left, BasicRadical right)
        {
            return Subtract(left, right);
        }

        public static BasicRadical[] operator -(BasicRadical left, BasicRadical[] right)
        {
            return Subtract(left, right);
        }

        public static BasicRadical[] operator -(BasicRadical[] left, BasicRadical right)
        {
            // a - b = -b - (-a)
            return Subtract(Negate(right), Negate(left));
        }

        public static BasicRadical operator *(BasicRadical left, BasicRadical right)
        {
            return Multiply(left, right);
        }

        public static BasicRadical operator *(BasicRadical left, Rational right)
        {
            return Multiply(left, new BasicRadical(right, 1));
        }

        public static BasicRadical operator *(Rational left, BasicRadical right)
        {
            return Multiply(new BasicRadical(left, 1), right);
        }

        public static BasicRadical[] operator *(BasicRadical left, BasicRadical[] right)
        {
            return Multiply(left, right);
        }

        public static BasicRadical[] operator *(BasicRadical[] left, BasicRadical right)
        {
            return Multiply(right, left);
        }

        public static BasicRadical operator /(BasicRadical left, BasicRadical right)
        {
            return Divide(left, right);
        }

        public static BasicRadical operator /(BasicRadical left, Rational right)
        {
            return Divide(left, new BasicRadical(right, 1));
        }

        public static BasicRadical operator /(Rational left, BasicRadical right)
        {
            return Divide(new BasicRadical(left, 1), right);
        }
        
    }
}
