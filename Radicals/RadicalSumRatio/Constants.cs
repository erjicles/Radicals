using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSumRatio
    {
        /// <summary>
        /// Zero
        /// </summary>
        public static readonly RadicalSumRatio Zero = new RadicalSumRatio(RadicalSum.Zero, RadicalSum.One);

        /// <summary>
        /// One
        /// </summary>
        public static readonly RadicalSumRatio One = new RadicalSumRatio(RadicalSum.One, RadicalSum.One);
    }
}
