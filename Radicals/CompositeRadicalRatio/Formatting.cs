using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio : IFormattable
    {
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (CompositeRadical.One == Denominator)
                return Numerator.ToString(format, formatProvider);
            return "[" + Numerator.ToString(format, formatProvider) + "] / [" + Denominator.ToString(format, formatProvider) + "]";
        }

        public override string ToString()
        {
            return ToString("S", CultureInfo.InvariantCulture);
        }
    }
}
