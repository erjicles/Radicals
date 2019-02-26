using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
        : IComparable, IComparable<CompositeRadicalRatio>, IEquatable<CompositeRadicalRatio>
    {
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is CompositeRadicalRatio))
                throw new ArgumentException("Invalid type comparison", nameof(obj));
            return CompareTo((CompositeRadicalRatio)obj);
        }

        public int CompareTo(CompositeRadicalRatio other)
        {
            return ToDouble().CompareTo(other.ToDouble());
        }

        public bool Equals(CompositeRadicalRatio other)
        {
            return (numerator == other.numerator
                && denominator == other.denominator);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is CompositeRadicalRatio))
                throw new ArgumentException("Invalid type equality check", nameof(obj));
            return Equals((CompositeRadicalRatio)obj);
        }

        public override int GetHashCode()
        {
            int h1 = numerator.GetHashCode();
            int h2 = denominator.GetHashCode();
            return (((h1 << 5) + h1) ^ h2);
        }
    }
}
