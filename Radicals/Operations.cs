using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct Radical
    {
        public static Radical Negate(Radical value)
        {
            BasicRadical[] result = new BasicRadical[value.radicals.Length];
            for (int i = 0; i < value.radicals.Length; i++)
                result[i] = -value.radicals[i];
            return new Radical(result);
        }

        public static Radical Add(Radical left, Radical right)
        {
            var z = new BasicRadical[left.radicals.Length + right.radicals.Length];
            for (int i = 0; i < left.radicals.Length; i++)
                z[i] = left.radicals[i];
            for (int i = 0; i < right.radicals.Length; i++)
                z[i + left.radicals.Length] = right.radicals[i];
            return new Radical(z);
        }

        public static Radical Subtract(Radical left, Radical right)
        {
            return Add(left, -right);
        }

        public static Radical Multiply(Radical left, Radical right)
        {
            var z = new BasicRadical[left.radicals.Length * right.radicals.Length];
            for (int i = 0; i < left.radicals.Length; i++)
                for (int j = 0; j < right.radicals.Length; j++)
                    z[(i * right.radicals.Length) + j] = left.radicals[i] * right.radicals[j];
            return new Radical(z);
        }
    }
}
