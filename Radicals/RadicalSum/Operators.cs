using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSum
    {
        public static bool operator <(RadicalSum left, RadicalSum right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <(RadicalSum left, Rational right)
        {
            return left.CompareTo(new RadicalSum(right, 1)) < 0;
        }

        public static bool operator <(Rational left, RadicalSum right)
        {
            return (new RadicalSum(left, 1)).CompareTo(right) < 0;
        }

        public static bool operator <(RadicalSum left, Radical right)
        {
            return left.CompareTo(new RadicalSum(right)) < 0;
        }

        public static bool operator <(Radical left, RadicalSum right)
        {
            return (new RadicalSum(left)).CompareTo(right) < 0;
        }

        public static bool operator <=(RadicalSum left, RadicalSum right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator <=(RadicalSum left, Rational right)
        {
            return left.CompareTo(new RadicalSum(right, 1)) <= 0;
        }

        public static bool operator <=(Rational left, RadicalSum right)
        {
            return (new RadicalSum(left, 1)).CompareTo(right) <= 0;
        }

        public static bool operator <=(RadicalSum left, Radical right)
        {
            return left.CompareTo(new RadicalSum(right)) <= 0;
        }

        public static bool operator <=(Radical left, RadicalSum right)
        {
            return (new RadicalSum(left)).CompareTo(right) <= 0;
        }

        public static bool operator >(RadicalSum left, RadicalSum right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >(RadicalSum left, Rational right)
        {
            return left.CompareTo(new RadicalSum(right, 1)) > 0;
        }

        public static bool operator >(Rational left, RadicalSum right)
        {
            return (new RadicalSum(left, 1)).CompareTo(right) > 0;
        }

        public static bool operator >(RadicalSum left, Radical right)
        {
            return left.CompareTo(new RadicalSum(right)) > 0;
        }

        public static bool operator >(Radical left, RadicalSum right)
        {
            return (new RadicalSum(left)).CompareTo(right) > 0;
        }

        public static bool operator >=(RadicalSum left, RadicalSum right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator >=(RadicalSum left, Rational right)
        {
            return left.CompareTo(new RadicalSum(right, 1)) >= 0;
        }

        public static bool operator >=(Rational left, RadicalSum right)
        {
            return (new RadicalSum(left, 1)).CompareTo(right) >= 0;
        }

        public static bool operator >=(RadicalSum left, Radical right)
        {
            return left.CompareTo(new RadicalSum(right)) >= 0;
        }

        public static bool operator >=(Radical left, RadicalSum right)
        {
            return (new RadicalSum(left)).CompareTo(right) >= 0;
        }

        public static bool operator ==(RadicalSum left, RadicalSum right)
        {
            return left.Equals(right);
        }

        public static bool operator ==(RadicalSum left, Rational right)
        {
            return left.Equals(new RadicalSum(right, 1));
        }

        public static bool operator ==(Rational left, RadicalSum right)
        {
            return (new RadicalSum(left, 1)).Equals(right);
        }

        public static bool operator ==(RadicalSum left, Radical right)
        {
            return left.Equals(new RadicalSum(right));
        }

        public static bool operator ==(Radical left, RadicalSum right)
        {
            return (new RadicalSum(left)).Equals(right);
        }

        public static bool operator !=(RadicalSum left, RadicalSum right)
        {
            return !left.Equals(right);
        }

        public static bool operator !=(RadicalSum left, Rational right)
        {
            return !left.Equals(new RadicalSum(right, 1));
        }

        public static bool operator !=(Rational left, RadicalSum right)
        {
            return !(new RadicalSum(left, 1)).Equals(right);
        }

        public static bool operator !=(RadicalSum left, Radical right)
        {
            return !left.Equals(new RadicalSum(right));
        }

        public static bool operator !=(Radical left, RadicalSum right)
        {
            return !(new RadicalSum(left)).Equals(right);
        }

        public static RadicalSum operator -(RadicalSum value)
        {
            return Negate(value);
        }

        public static RadicalSum operator +(RadicalSum value)
        {
            return value;
        }

        public static RadicalSum operator +(RadicalSum left, RadicalSum right)
        {
            return Add(left, right);
        }

        public static RadicalSum operator +(RadicalSum left, Rational right)
        {
            return Add(left, new RadicalSum(right, 1));
        }

        public static RadicalSum operator +(Rational left, RadicalSum right)
        {
            return Add(new RadicalSum(left, 1), right);
        }

        public static RadicalSum operator +(RadicalSum left, Radical right)
        {
            return Add(left, new RadicalSum(right));
        }

        public static RadicalSum operator +(Radical left, RadicalSum right)
        {
            return Add(new RadicalSum(left), right);
        }

        public static RadicalSum operator -(RadicalSum left, RadicalSum right)
        {
            return Subtract(left, right);
        }

        public static RadicalSum operator -(RadicalSum left, Rational right)
        {
            return Subtract(left, new RadicalSum(right, 1));
        }

        public static RadicalSum operator -(Rational left, RadicalSum right)
        {
            return Subtract(new RadicalSum(left, 1), right);
        }

        public static RadicalSum operator -(RadicalSum left, Radical right)
        {
            return Subtract(left, new RadicalSum(right));
        }

        public static RadicalSum operator -(Radical left, RadicalSum right)
        {
            return Subtract(new RadicalSum(left), right);
        }

        public static RadicalSum operator *(RadicalSum left, RadicalSum right)
        {
            return Multiply(left, right);
        }

        public static RadicalSum operator *(RadicalSum left, Rational right)
        {
            return Multiply(left, new RadicalSum(right, 1));
        }

        public static RadicalSum operator *(Rational left, RadicalSum right)
        {
            return Multiply(new RadicalSum(left, 1), right);
        }

        public static RadicalSum operator *(RadicalSum left, Radical right)
        {
            return Multiply(left, new RadicalSum(right));
        }

        public static RadicalSum operator *(Radical left, RadicalSum right)
        {
            return Multiply(new RadicalSum(left), right);
        }

        public static RadicalSum operator /(RadicalSum left, Rational right)
        {
            return Divide(left, new Radical(right,1));
        }

        public static RadicalSum operator /(RadicalSum left, Radical right)
        {
            return Divide(left, right);
        }

        public static CompositeRadicalRatio operator /(RadicalSum left, RadicalSum right)
        {
            return Divide(left, right);
        }
    }
}
