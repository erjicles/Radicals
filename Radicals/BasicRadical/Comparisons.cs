using System;

namespace Radicals
{
    internal readonly partial struct BasicRadical : IComparable, IComparable<BasicRadical>, IEquatable<BasicRadical>
    {
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is BasicRadical))
                throw new ArgumentException("Invalid type comparison", nameof(obj));
            return CompareTo((BasicRadical)obj);
        }

        public int CompareTo(BasicRadical other)
        {
            if (c == other.c && r == other.r)
                return 0;

            return (c * c * r).CompareTo(other.c * other.c * other.r);
        }

        public bool Equals(BasicRadical other)
        {
            return c == other.c && r == other.r;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is BasicRadical))
                throw new ArgumentException("Invalid type equality check", nameof(obj));
            return Equals((BasicRadical)obj);
        }

        public override int GetHashCode()
        {
            int h1 = c.GetHashCode();
            int h2 = r.GetHashCode();
            return (((h1 << 5) + h1) ^ h2);
        }

        public bool IsCompatibleRadical(BasicRadical b)
        {
            return r == b.r;
        }

    }
}
