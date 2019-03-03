using System;

namespace Radicals
{
    public readonly partial struct Radical : IComparable, IComparable<Radical>, IEquatable<Radical>
    {
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is Radical))
                throw new ArgumentException("Invalid type comparison", nameof(obj));
            return CompareTo((Radical)obj);
        }

        public int CompareTo(Radical other)
        {
            if (other == null)
                return 1;

            if (Coefficient == other.Coefficient && Radicand == other.Radicand)
                return 0;

            return (Coefficient * Coefficient * Radicand).CompareTo(other.Coefficient * other.Coefficient * other.Radicand);
        }

        public bool Equals(Radical other)
        {
            return Coefficient == other.Coefficient && Radicand == other.Radicand;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Radical))
                throw new ArgumentException("Invalid type equality check", nameof(obj));
            return Equals((Radical)obj);
        }

        public override int GetHashCode()
        {
            int h1 = Coefficient.GetHashCode();
            int h2 = Radicand.GetHashCode();
            return (((h1 << 5) + h1) ^ h2);
        }

        public bool IsCompatibleRadical(Radical b)
        {
            return Radicand == b.Radicand;
        }

    }
}
