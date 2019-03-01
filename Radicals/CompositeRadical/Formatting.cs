using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadical : IFormattable
    {
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var result = new StringBuilder();
            for (int i = 0; i < Radicals.Length; i++)
            {
                if (i > 0)
                    result.Append(" + ");
                result.Append(Radicals[i].ToString(format, formatProvider));
            }
            return result.ToString();
        }

        public override string ToString()
        {
            return ToString("S", CultureInfo.InvariantCulture);
        }
    }
}
