using Rationals;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Radicals
{
    internal readonly partial struct BasicRadical
    {
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
    }
}
