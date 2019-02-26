using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadical
    {
        public static CompositeRadical Negate(CompositeRadical value)
        {
            BasicRadical[] result = new BasicRadical[value.radicals.Length];
            for (int i = 0; i < value.radicals.Length; i++)
                result[i] = -value.radicals[i];
            return new CompositeRadical(result);
        }

        public static CompositeRadical Add(CompositeRadical left, CompositeRadical right)
        {
            var z = new BasicRadical[left.radicals.Length + right.radicals.Length];
            for (int i = 0; i < left.radicals.Length; i++)
                z[i] = left.radicals[i];
            for (int i = 0; i < right.radicals.Length; i++)
                z[i + left.radicals.Length] = right.radicals[i];
            return new CompositeRadical(z);
        }

        public static CompositeRadical Subtract(CompositeRadical left, CompositeRadical right)
        {
            return Add(left, -right);
        }

        public static CompositeRadical Multiply(CompositeRadical left, CompositeRadical right)
        {
            var z = new BasicRadical[left.radicals.Length * right.radicals.Length];
            for (int i = 0; i < left.radicals.Length; i++)
                for (int j = 0; j < right.radicals.Length; j++)
                    z[(i * right.radicals.Length) + j] = left.radicals[i] * right.radicals[j];
            return new CompositeRadical(z);
        }

        public static CompositeRadical Divide(CompositeRadical left, Rational right)
        {
            var z = new BasicRadical[left.radicals.Length];
            for (int i = 0; i < left.radicals.Length; i++)
                z[i] = left.radicals[i] / right;
            return new CompositeRadical(z);
        }
    }
}
