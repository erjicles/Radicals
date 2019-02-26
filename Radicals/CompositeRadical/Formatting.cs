using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadical : IFormattable
    {
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (int i = 0; i < radicals.Length; i++)
            {
                if (i > 0)
                    result.Append(" + ");
                result.Append(radicals[i].ToString());
            }
            return result.ToString();
        }
    }
}
