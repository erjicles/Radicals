using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        public static CompositeRadicalRatio operator -(CompositeRadicalRatio value)
        {
            return Negate(value);
        }
    }
}
