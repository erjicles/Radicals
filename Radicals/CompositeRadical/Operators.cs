using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadical
    {
        public static bool operator <(CompositeRadical left, CompositeRadical right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(CompositeRadical left, CompositeRadical right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(CompositeRadical left, CompositeRadical right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool operator >=(CompositeRadical left, CompositeRadical right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator ==(CompositeRadical left, CompositeRadical right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CompositeRadical left, CompositeRadical right)
        {
            return !left.Equals(right);
        }

        public static CompositeRadical operator -(CompositeRadical value)
        {
            return Negate(value);
        }

        public static CompositeRadical operator +(CompositeRadical value)
        {
            return value;
        }

        public static CompositeRadical operator +(CompositeRadical left, CompositeRadical right)
        {
            return Add(left, right);
        }

        public static CompositeRadical operator -(CompositeRadical left, CompositeRadical right)
        {
            return Subtract(left, right);
        }

        public static CompositeRadical operator *(CompositeRadical left, CompositeRadical right)
        {
            return Multiply(left, right);
        }
    }
}
