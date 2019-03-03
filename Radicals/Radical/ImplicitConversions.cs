using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct Radical
    { 

        public static implicit operator Radical(Rational value)
        {
            return new Radical(value, 1);
        }

        public static implicit operator Radical(byte value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(sbyte value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(short value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(ushort value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(int value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(uint value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(long value)
        {
            return new Radical(new Rational(value), 1);
        }

        public static implicit operator Radical(ulong value)
        {
            return new Radical(new Rational(value), 1);
        }
    }
}
