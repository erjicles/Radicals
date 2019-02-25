﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct Radical
    {
        public double ToDouble()
        {
            double result = 0;
            for (int i = 0; i < radicals.Length; i++)
            {
                result += radicals[i].ToDouble();
            }
            return result;
        }

        public static explicit operator double(Radical radical)
        {
            return radical.ToDouble();
        }
    }
}
