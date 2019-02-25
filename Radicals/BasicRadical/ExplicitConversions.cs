using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    internal readonly partial struct BasicRadical
    {
        public double ToDouble()
        {
            return (double)c * Math.Sqrt((double)r);
        }

        public static explicit operator double(BasicRadical basicRadical)
        {
            return basicRadical.ToDouble();
        }
    }
}
