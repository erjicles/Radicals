using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct BasicRadical
    {
        public double ToDouble()
        {
            return (double)C * Math.Sqrt((double)R);
        }

        public static explicit operator double(BasicRadical basicRadical)
        {
            return basicRadical.ToDouble();
        }
    }
}
