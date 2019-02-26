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
            if (other == null)
                return 1;

            if (C == other.C && R == other.R)
                return 0;

            return (C * C * R).CompareTo(other.C * other.C * other.R);
        }

        public bool Equals(BasicRadical other)
        {
            return C == other.C && R == other.R;
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
            int h1 = C.GetHashCode();
            int h2 = R.GetHashCode();
            return (((h1 << 5) + h1) ^ h2);
        }

        public bool IsCompatibleRadical(BasicRadical b)
        {
            return R == b.R;
        }

    }
}
