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
            if (radicals.Length != other.radicals.Length)
                return false;

            for (int i = 0; i < radicals.Length; i++)
                if (radicals[i] != other.radicals[i])
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
            int h = radicals[0].GetHashCode();
            for (int i = 1; i < radicals.Length; i++)
            {
                h = (((h << 5) + h) ^ radicals[i].GetHashCode());
            }
            return h;
        }
    }
}
