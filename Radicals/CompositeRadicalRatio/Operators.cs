using Rationals;
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

        public static CompositeRadicalRatio operator +(
            CompositeRadicalRatio left, 
            CompositeRadicalRatio right)
        {
            return Add(left, right);
        }

        public static CompositeRadicalRatio operator +(
            CompositeRadicalRatio left,
            Rational right)
        {
            return Add(left, new CompositeRadicalRatio(right, 1));
        }

        public static CompositeRadicalRatio operator +(
            Rational left,
            CompositeRadicalRatio right)
        {
            return Add(new CompositeRadicalRatio(left, 1), right);
        }

        public static CompositeRadicalRatio operator -(
            CompositeRadicalRatio left, 
            CompositeRadicalRatio right)
        {
            return Subtract(left, right);
        }

        public static CompositeRadicalRatio operator -(
            CompositeRadicalRatio left,
            Rational right)
        {
            return Subtract(left, new CompositeRadicalRatio(right, 1));
        }

        public static CompositeRadicalRatio operator -(
            Rational left,
            CompositeRadicalRatio right)
        {
            return Subtract(new CompositeRadicalRatio(left, 1), right);
        }

        public static CompositeRadicalRatio operator *(
            CompositeRadicalRatio left, 
            CompositeRadicalRatio right)
        {
            return Multiply(left, right);
        }

        public static CompositeRadicalRatio operator *(
            CompositeRadicalRatio left, 
            Rational right)
        {
            return Multiply(left, new CompositeRadicalRatio(right, 1));
        }

        public static CompositeRadicalRatio operator *(
            Rational left,
            CompositeRadicalRatio right)
        {
            return Multiply(new CompositeRadicalRatio(left, 1), right);
        }
    }
}
