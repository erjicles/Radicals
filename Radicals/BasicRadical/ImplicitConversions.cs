using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct BasicRadical
    { 

        public static implicit operator BasicRadical(Rational value)
        {
            return new BasicRadical(value, 1);
        }

        public static implicit operator BasicRadical(byte value)
        {
            return new BasicRadical(new Rational(value), 1);
        }

        public static implicit operator BasicRadical(sbyte value)
        {
            return new BasicRadical(new Rational(value), 1);
        }

        public static implicit operator BasicRadical(short value)
        {
            return new BasicRadical(new Rational(value), 1);
        }

        public static implicit operator BasicRadical(ushort value)
        {
            return new BasicRadical(new Rational(value), 1);
        }

        public static implicit operator BasicRadical(int value)
        {
            return new BasicRadical(new Rational(value), 1);
        }

        public static implicit operator BasicRadical(uint value)
        {
            return new BasicRadical(new Rational(value), 1);
        }

        public static implicit operator BasicRadical(long value)
        {
            return new BasicRadical(new Rational(value), 1);
        }

        public static implicit operator BasicRadical(ulong value)
        {
            return new BasicRadical(new Rational(value), 1);
        }
    }
}
