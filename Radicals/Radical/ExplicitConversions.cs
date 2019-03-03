using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct Radical
    {
        public double ToDouble()
        {
            return (double)Coefficient * Math.Sqrt((double)Radicand);
        }

        public static explicit operator double(Radical basicRadical)
        {
            return basicRadical.ToDouble();
        }
    }
}
