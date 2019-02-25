using System;

namespace Radicals
{
    internal readonly partial struct BasicRadical : IFormattable
    {
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public override string ToString()
        {
            if (c.IsZero)
                return "0";
            if (r.IsZero)
                return "0";

            string cPart = "";
            string rPart = "";
            if (!c.IsOne)
                cPart = c.ToString();
            if (!r.IsOne)
                rPart = "Sqrt(" + r.ToString() + ")";
            if (cPart.Length > 0 && rPart.Length > 0)
            {
                if (c.Denominator == 1)
                    cPart = cPart + " * ";
                else
                    cPart = "(" + cPart + ") * ";
            }
            return cPart + rPart;
        }
    }
}
