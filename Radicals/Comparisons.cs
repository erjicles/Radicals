using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct Radical
        : IFormattable, IComparable, IComparable<BigInteger>, IEquatable<BigInteger>
    {
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(BigInteger other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(BigInteger other)
        {
            throw new NotImplementedException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}
