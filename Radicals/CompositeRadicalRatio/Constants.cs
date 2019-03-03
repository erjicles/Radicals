using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        /// <summary>
        /// Zero
        /// </summary>
        public static readonly CompositeRadicalRatio Zero = new CompositeRadicalRatio(RadicalSum.Zero, RadicalSum.One);

        /// <summary>
        /// One
        /// </summary>
        public static readonly CompositeRadicalRatio One = new CompositeRadicalRatio(RadicalSum.One, RadicalSum.One);
    }
}
