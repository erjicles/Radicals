using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct Radical
    {
        public static bool operator <(Radical left, Radical right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Radical left, Radical right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Radical left, Radical right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool operator >=(Radical left, Radical right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator ==(Radical left, Radical right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Radical left, Radical right)
        {
            return !left.Equals(right);
        }

        public static Radical operator -(Radical value)
        {
            return Negate(value);
        }

        public static Radical operator +(Radical value)
        {
            return value;
        }

        public static Radical operator +(Radical left, Radical right)
        {
            return Add(left, right);
        }

        public static Radical operator -(Radical left, Radical right)
        {
            return Subtract(left, right);
        }

        public static Radical operator *(Radical left, Radical right)
        {
            return Multiply(left, right);
        }
    }
}
