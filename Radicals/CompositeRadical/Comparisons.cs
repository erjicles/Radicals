using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadical
        : IComparable, IComparable<CompositeRadical>, IEquatable<CompositeRadical>
    {
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is CompositeRadical))
                throw new ArgumentException("Invalid type comparison", nameof(obj));
            return CompareTo((CompositeRadical)obj);
        }

        public int CompareTo(CompositeRadical other)
        {
            return ToDouble().CompareTo(other.ToDouble());
        }

        public bool Equals(CompositeRadical other)
        {
            if (other == null)
                return false;

            if (Radicals.Length != other.Radicals.Length)
                return false;

            for (int i = 0; i < Radicals.Length; i++)
                if (Radicals[i] != other.Radicals[i])
                    return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is CompositeRadical))
                throw new ArgumentException("Invalid type equality check", nameof(obj));
            return Equals((CompositeRadical)obj);
        }

        public override int GetHashCode()
        {
            if (Radicals.Length == 0)
                return 0;
            int h = Radicals[0].GetHashCode();
            for (int i = 1; i < Radicals.Length; i++)
            {
                h = (((h << 5) + h) ^ Radicals[i].GetHashCode());
            }
            return h;
        }
    }
}
