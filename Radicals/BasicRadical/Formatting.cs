using System;

namespace Radicals
{
    public readonly partial struct BasicRadical : IFormattable
    {
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public override string ToString()
        {
            if (C.IsZero)
                return "0";
            if (R.IsZero)
                return "0";

            string cPart = "";
            string rPart = "";
            if (!C.IsOne)
                cPart = C.ToString();
            if (!R.IsOne)
                rPart = "Sqrt(" + R.ToString() + ")";
            if (cPart.Length > 0 && rPart.Length > 0)
            {
                if (C.Denominator == 1)
                    cPart = cPart + " * ";
                else
                    cPart = "(" + cPart + ") * ";
            }
            return cPart + rPart;
        }
    }
}
