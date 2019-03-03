using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSum
    {
        public double ToDouble()
        {
            double result = 0;
            for (int i = 0; i < Radicals.Length; i++)
            {
                result += Radicals[i].ToDouble();
            }
            return result;
        }

        public static explicit operator double(RadicalSum radical)
        {
            return radical.ToDouble();
        }
    }
}
