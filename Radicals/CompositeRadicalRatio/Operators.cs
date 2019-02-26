using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        public static bool operator <(CompositeRadicalRatio left, CompositeRadicalRatio right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(CompositeRadicalRatio left, CompositeRadicalRatio right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(CompositeRadicalRatio left, CompositeRadicalRatio right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool operator >=(CompositeRadicalRatio left, CompositeRadicalRatio right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator ==(CompositeRadicalRatio left, CompositeRadicalRatio right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CompositeRadicalRatio left, CompositeRadicalRatio right)
        {
            return !left.Equals(right);
        }

        public static CompositeRadicalRatio operator -(CompositeRadicalRatio value)
        {
            return Negate(value);
        }

        public static CompositeRadicalRatio operator +(CompositeRadicalRatio value)
        {
            return value;
        }

        public static CompositeRadicalRatio operator +(CompositeRadicalRatio left, CompositeRadicalRatio right)
        {
            return Add(left, right);
        }

        public static CompositeRadicalRatio operator -(CompositeRadicalRatio left, CompositeRadicalRatio right)
        {
            return Subtract(left, right);
        }

        public static CompositeRadicalRatio operator *(CompositeRadicalRatio left, CompositeRadicalRatio right)
        {
            return Multiply(left, right);
        }
    }
}
