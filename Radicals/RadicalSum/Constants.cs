using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSum
    {
        /// <summary>
        /// Zero
        /// </summary>
        public static readonly RadicalSum Zero = new RadicalSum(0, 0);

        /// <summary>
        /// One
        /// </summary>
        public static readonly RadicalSum One = new RadicalSum(1, 1);
    }
}
