using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSumRatio
    {
        public static bool operator <(RadicalSumRatio left, RadicalSumRatio right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <(RadicalSumRatio left, Rational right)
        {
            return left.CompareTo(new RadicalSumRatio(right, 1)) < 0;
        }

        public static bool operator <(Rational left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left, 1)).CompareTo(right) < 0;
        }

        public static bool operator <(RadicalSumRatio left, Radical right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) < 0;
        }

        public static bool operator <(Radical left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) < 0;
        }

        public static bool operator <(RadicalSumRatio left, RadicalSum right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) < 0;
        }

        public static bool operator <(RadicalSum left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) < 0;
        }

        public static bool operator <=(RadicalSumRatio left, RadicalSumRatio right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator <=(RadicalSumRatio left, Rational right)
        {
            return left.CompareTo(new RadicalSumRatio(right, 1)) <= 0;
        }

        public static bool operator <=(Rational left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left, 1)).CompareTo(right) <= 0;
        }

        public static bool operator <=(RadicalSumRatio left, Radical right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) <= 0;
        }

        public static bool operator <=(Radical left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) <= 0;
        }

        public static bool operator <=(RadicalSumRatio left, RadicalSum right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) <= 0;
        }

        public static bool operator <=(RadicalSum left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) <= 0;
        }

        public static bool operator >(RadicalSumRatio left, RadicalSumRatio right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >(RadicalSumRatio left, Rational right)
        {
            return left.CompareTo(new RadicalSumRatio(right, 1)) > 0;
        }

        public static bool operator >(Rational left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left, 1)).CompareTo(right) > 0;
        }

        public static bool operator >(RadicalSumRatio left, Radical right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) > 0;
        }

        public static bool operator >(Radical left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) > 0;
        }

        public static bool operator >(RadicalSumRatio left, RadicalSum right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) > 0;
        }

        public static bool operator >(RadicalSum left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) > 0;
        }

        public static bool operator >=(RadicalSumRatio left, RadicalSumRatio right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator >=(RadicalSumRatio left, Rational right)
        {
            return left.CompareTo(new RadicalSumRatio(right, 1)) >= 0;
        }

        public static bool operator >=(Rational left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left, 1)).CompareTo(right) >= 0;
        }

        public static bool operator >=(RadicalSumRatio left, Radical right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) >= 0;
        }

        public static bool operator >=(Radical left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) >= 0;
        }

        public static bool operator >=(RadicalSumRatio left, RadicalSum right)
        {
            return left.CompareTo(new RadicalSumRatio(right)) >= 0;
        }

        public static bool operator >=(RadicalSum left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).CompareTo(right) >= 0;
        }

        public static bool operator ==(RadicalSumRatio left, RadicalSumRatio right)
        {
            return left.Equals(right);
        }

        public static bool operator ==(RadicalSumRatio left, Rational right)
        {
            return left.Equals(new RadicalSumRatio(right, 1));
        }

        public static bool operator ==(Rational left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left, 1)).Equals(right);
        }

        public static bool operator ==(RadicalSumRatio left, Radical right)
        {
            return left.Equals(new RadicalSumRatio(right));
        }

        public static bool operator ==(Radical left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).Equals(right);
        }

        public static bool operator ==(RadicalSumRatio left, RadicalSum right)
        {
            return left.Equals(new RadicalSumRatio(right));
        }

        public static bool operator ==(RadicalSum left, RadicalSumRatio right)
        {
            return (new RadicalSumRatio(left)).Equals(right);
        }

        public static bool operator !=(RadicalSumRatio left, RadicalSumRatio right)
        {
            return !left.Equals(right);
        }

        public static bool operator !=(RadicalSumRatio left, Rational right)
        {
            return !left.Equals(new RadicalSumRatio(right, 1));
        }

        public static bool operator !=(Rational left, RadicalSumRatio right)
        {
            return !(new RadicalSumRatio(left, 1)).Equals(right);
        }

        public static bool operator !=(RadicalSumRatio left, Radical right)
        {
            return !left.Equals(new RadicalSumRatio(right));
        }

        public static bool operator !=(Radical left, RadicalSumRatio right)
        {
            return !(new RadicalSumRatio(left)).Equals(right);
        }

        public static bool operator !=(RadicalSumRatio left, RadicalSum right)
        {
            return !left.Equals(new RadicalSumRatio(right));
        }

        public static bool operator !=(RadicalSum left, RadicalSumRatio right)
        {
            return !(new RadicalSumRatio(left)).Equals(right);
        }

        public static RadicalSumRatio operator -(RadicalSumRatio value)
        {
            return Negate(value);
        }

        public static RadicalSumRatio operator +(RadicalSumRatio value)
        {
            return value;
        }

        public static RadicalSumRatio operator +(
            RadicalSumRatio left, 
            RadicalSumRatio right)
        {
            return Add(left, right);
        }

        public static RadicalSumRatio operator +(
            RadicalSumRatio left,
            Rational right)
        {
            return Add(left, new RadicalSumRatio(right, 1));
        }

        public static RadicalSumRatio operator +(
            Rational left,
            RadicalSumRatio right)
        {
            return Add(new RadicalSumRatio(left, 1), right);
        }

        public static RadicalSumRatio operator +(
            RadicalSumRatio left,
            Radical right)
        {
            return Add(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator +(
            Radical left,
            RadicalSumRatio right)
        {
            return Add(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator +(
            RadicalSumRatio left,
            RadicalSum right)
        {
            return Add(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator +(
            RadicalSum left,
            RadicalSumRatio right)
        {
            return Add(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator -(
            RadicalSumRatio left, 
            RadicalSumRatio right)
        {
            return Subtract(left, right);
        }

        public static RadicalSumRatio operator -(
            RadicalSumRatio left,
            Rational right)
        {
            return Subtract(left, new RadicalSumRatio(right, 1));
        }

        public static RadicalSumRatio operator -(
            Rational left,
            RadicalSumRatio right)
        {
            return Subtract(new RadicalSumRatio(left, 1), right);
        }

        public static RadicalSumRatio operator -(
            RadicalSumRatio left,
            Radical right)
        {
            return Subtract(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator -(
            Radical left,
            RadicalSumRatio right)
        {
            return Subtract(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator -(
            RadicalSumRatio left,
            RadicalSum right)
        {
            return Subtract(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator -(
            RadicalSum left,
            RadicalSumRatio right)
        {
            return Subtract(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator *(
            RadicalSumRatio left, 
            RadicalSumRatio right)
        {
            return Multiply(left, right);
        }

        public static RadicalSumRatio operator *(
            RadicalSumRatio left, 
            Rational right)
        {
            return Multiply(left, new RadicalSumRatio(right, 1));
        }

        public static RadicalSumRatio operator *(
            Rational left,
            RadicalSumRatio right)
        {
            return Multiply(new RadicalSumRatio(left, 1), right);
        }

        public static RadicalSumRatio operator *(
            RadicalSumRatio left,
            Radical right)
        {
            return Multiply(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator *(
            Radical left,
            RadicalSumRatio right)
        {
            return Multiply(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator *(
            RadicalSumRatio left,
            RadicalSum right)
        {
            return Multiply(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator *(
            RadicalSum left,
            RadicalSumRatio right)
        {
            return Multiply(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator /(
            RadicalSumRatio left,
            RadicalSumRatio right)
        {
            return Divide(left, right);
        }

        public static RadicalSumRatio operator /(
            RadicalSumRatio left,
            Rational right)
        {
            return Divide(left, new RadicalSumRatio(right, 1));
        }

        public static RadicalSumRatio operator /(
            Rational left,
            RadicalSumRatio right)
        {
            return Divide(new RadicalSumRatio(left, 1), right);
        }

        public static RadicalSumRatio operator /(
            RadicalSumRatio left,
            Radical right)
        {
            return Divide(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator /(
            Radical left,
            RadicalSumRatio right)
        {
            return Divide(new RadicalSumRatio(left), right);
        }

        public static RadicalSumRatio operator /(
            RadicalSumRatio left,
            RadicalSum right)
        {
            return Divide(left, new RadicalSumRatio(right));
        }

        public static RadicalSumRatio operator /(
            RadicalSum left,
            RadicalSumRatio right)
        {
            return Divide(new RadicalSumRatio(left), right);
        }
    }
}
