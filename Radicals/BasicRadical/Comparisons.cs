using System;

namespace Radicals
{
    public readonly partial struct BasicRadical : IComparable, IComparable<BasicRadical>, IEquatable<BasicRadical>
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

            if (Coefficient == other.Coefficient && Radicand == other.Radicand)
                return 0;

            return (Coefficient * Coefficient * Radicand).CompareTo(other.Coefficient * other.Coefficient * other.Radicand);
        }

        public bool Equals(BasicRadical other)
        {
            return Coefficient == other.Coefficient && Radicand == other.Radicand;
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
            int h1 = Coefficient.GetHashCode();
            int h2 = Radicand.GetHashCode();
            return (((h1 << 5) + h1) ^ h2);
        }

        public bool IsCompatibleRadical(BasicRadical b)
        {
            return Radicand == b.Radicand;
        }

    }
}
