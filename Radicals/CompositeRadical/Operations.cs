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
            BasicRadical[] radicals = new BasicRadical[value.Radicals.Length];
            for (int i = 0; i < value.Radicals.Length; i++)
                radicals[i] = -value.Radicals[i];
            var result = new CompositeRadical(radicals);
            return result;
        }

        public static CompositeRadical Add(CompositeRadical left, CompositeRadical right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            var z = new BasicRadical[left.Radicals.Length + right.Radicals.Length];
            for (int i = 0; i < left.Radicals.Length; i++)
                z[i] = left.Radicals[i];
            for (int i = 0; i < right.Radicals.Length; i++)
                z[i + left.Radicals.Length] = right.Radicals[i];
            return new CompositeRadical(z);
        }

        public static CompositeRadical Subtract(CompositeRadical left, CompositeRadical right)
        {
            return Add(left, -right);
        }

        public static CompositeRadical Multiply(CompositeRadical left, CompositeRadical right)
        {
            var z = new BasicRadical[left.Radicals.Length * right.Radicals.Length];
            for (int i = 0; i < left.Radicals.Length; i++)
                for (int j = 0; j < right.Radicals.Length; j++)
                    z[(i * right.Radicals.Length) + j] = left.Radicals[i] * right.Radicals[j];
            return new CompositeRadical(z);
        }

        public static CompositeRadical Divide(CompositeRadical left, BasicRadical right)
        {
            var z = new BasicRadical[left.Radicals.Length];
            for (int i = 0; i < left.Radicals.Length; i++)
                z[i] = left.Radicals[i] / right;
            return new CompositeRadical(z);
        }
    }
}
