using Rationals;
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

        public static bool operator <(CompositeRadical left, Rational right)
        {
            return left.CompareTo(new CompositeRadical(right, 1)) < 0;
        }

        public static bool operator <(Rational left, CompositeRadical right)
        {
            return (new CompositeRadical(left, 1)).CompareTo(right) < 0;
        }

        public static bool operator <(CompositeRadical left, BasicRadical right)
        {
            return left.CompareTo(new CompositeRadical(right)) < 0;
        }

        public static bool operator <(BasicRadical left, CompositeRadical right)
        {
            return (new CompositeRadical(left)).CompareTo(right) < 0;
        }

        public static bool operator <=(CompositeRadical left, CompositeRadical right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator <=(CompositeRadical left, Rational right)
        {
            return left.CompareTo(new CompositeRadical(right, 1)) <= 0;
        }

        public static bool operator <=(Rational left, CompositeRadical right)
        {
            return (new CompositeRadical(left, 1)).CompareTo(right) <= 0;
        }

        public static bool operator <=(CompositeRadical left, BasicRadical right)
        {
            return left.CompareTo(new CompositeRadical(right)) <= 0;
        }

        public static bool operator <=(BasicRadical left, CompositeRadical right)
        {
            return (new CompositeRadical(left)).CompareTo(right) <= 0;
        }

        public static bool operator >(CompositeRadical left, CompositeRadical right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >(CompositeRadical left, Rational right)
        {
            return left.CompareTo(new CompositeRadical(right, 1)) > 0;
        }

        public static bool operator >(Rational left, CompositeRadical right)
        {
            return (new CompositeRadical(left, 1)).CompareTo(right) > 0;
        }

        public static bool operator >(CompositeRadical left, BasicRadical right)
        {
            return left.CompareTo(new CompositeRadical(right)) > 0;
        }

        public static bool operator >(BasicRadical left, CompositeRadical right)
        {
            return (new CompositeRadical(left)).CompareTo(right) > 0;
        }

        public static bool operator >=(CompositeRadical left, CompositeRadical right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator >=(CompositeRadical left, Rational right)
        {
            return left.CompareTo(new CompositeRadical(right, 1)) >= 0;
        }

        public static bool operator >=(Rational left, CompositeRadical right)
        {
            return (new CompositeRadical(left, 1)).CompareTo(right) >= 0;
        }

        public static bool operator >=(CompositeRadical left, BasicRadical right)
        {
            return left.CompareTo(new CompositeRadical(right)) >= 0;
        }

        public static bool operator >=(BasicRadical left, CompositeRadical right)
        {
            return (new CompositeRadical(left)).CompareTo(right) >= 0;
        }

        public static bool operator ==(CompositeRadical left, CompositeRadical right)
        {
            return left.Equals(right);
        }

        public static bool operator ==(CompositeRadical left, Rational right)
        {
            return left.Equals(new CompositeRadical(right, 1));
        }

        public static bool operator ==(Rational left, CompositeRadical right)
        {
            return (new CompositeRadical(left, 1)).Equals(right);
        }

        public static bool operator ==(CompositeRadical left, BasicRadical right)
        {
            return left.Equals(new CompositeRadical(right));
        }

        public static bool operator ==(BasicRadical left, CompositeRadical right)
        {
            return (new CompositeRadical(left)).Equals(right);
        }

        public static bool operator !=(CompositeRadical left, CompositeRadical right)
        {
            return !left.Equals(right);
        }

        public static bool operator !=(CompositeRadical left, Rational right)
        {
            return !left.Equals(new CompositeRadical(right, 1));
        }

        public static bool operator !=(Rational left, CompositeRadical right)
        {
            return !(new CompositeRadical(left, 1)).Equals(right);
        }

        public static bool operator !=(CompositeRadical left, BasicRadical right)
        {
            return !left.Equals(new CompositeRadical(right));
        }

        public static bool operator !=(BasicRadical left, CompositeRadical right)
        {
            return !(new CompositeRadical(left)).Equals(right);
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

        public static CompositeRadical operator +(CompositeRadical left, Rational right)
        {
            return Add(left, new CompositeRadical(right, 1));
        }

        public static CompositeRadical operator +(Rational left, CompositeRadical right)
        {
            return Add(new CompositeRadical(left, 1), right);
        }

        public static CompositeRadical operator +(CompositeRadical left, BasicRadical right)
        {
            return Add(left, new CompositeRadical(right));
        }

        public static CompositeRadical operator +(BasicRadical left, CompositeRadical right)
        {
            return Add(new CompositeRadical(left), right);
        }

        public static CompositeRadical operator -(CompositeRadical left, CompositeRadical right)
        {
            return Subtract(left, right);
        }

        public static CompositeRadical operator -(CompositeRadical left, Rational right)
        {
            return Subtract(left, new CompositeRadical(right, 1));
        }

        public static CompositeRadical operator -(Rational left, CompositeRadical right)
        {
            return Subtract(new CompositeRadical(left, 1), right);
        }

        public static CompositeRadical operator -(CompositeRadical left, BasicRadical right)
        {
            return Subtract(left, new CompositeRadical(right));
        }

        public static CompositeRadical operator -(BasicRadical left, CompositeRadical right)
        {
            return Subtract(new CompositeRadical(left), right);
        }

        public static CompositeRadical operator *(CompositeRadical left, CompositeRadical right)
        {
            return Multiply(left, right);
        }

        public static CompositeRadical operator *(CompositeRadical left, Rational right)
        {
            return Multiply(left, new CompositeRadical(right, 1));
        }

        public static CompositeRadical operator *(Rational left, CompositeRadical right)
        {
            return Multiply(new CompositeRadical(left, 1), right);
        }

        public static CompositeRadical operator *(CompositeRadical left, BasicRadical right)
        {
            return Multiply(left, new CompositeRadical(right));
        }

        public static CompositeRadical operator *(BasicRadical left, CompositeRadical right)
        {
            return Multiply(new CompositeRadical(left), right);
        }

        public static CompositeRadical operator /(CompositeRadical left, Rational right)
        {
            return Divide(left, new BasicRadical(right,1));
        }

        public static CompositeRadical operator /(CompositeRadical left, BasicRadical right)
        {
            return Divide(left, right);
        }
    }
}
