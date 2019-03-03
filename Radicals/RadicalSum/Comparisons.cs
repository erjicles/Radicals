using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSum
        : IComparable, IComparable<RadicalSum>, IEquatable<RadicalSum>
    {
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is RadicalSum))
                throw new ArgumentException("Invalid type comparison", nameof(obj));
            return CompareTo((RadicalSum)obj);
        }

        public int CompareTo(RadicalSum other)
        {
            return ToDouble().CompareTo(other.ToDouble());
        }

        public bool Equals(RadicalSum other)
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
            if (!(obj is RadicalSum))
                throw new ArgumentException("Invalid type equality check", nameof(obj));
            return Equals((RadicalSum)obj);
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
