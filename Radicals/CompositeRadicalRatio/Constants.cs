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
        public static readonly CompositeRadicalRatio Zero = new CompositeRadicalRatio(CompositeRadical.Zero, CompositeRadical.One);

        /// <summary>
        /// One
        /// </summary>
        public static readonly CompositeRadicalRatio One = new CompositeRadicalRatio(CompositeRadical.One, CompositeRadical.One);
    }
}
