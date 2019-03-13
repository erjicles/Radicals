using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Radicals.Combinatorics
{
    public readonly struct IntegerPartition
        : IFormattable, IEquatable<IntegerPartition>
    {

        private readonly int[] _values;
        private readonly int _n;
        public int[] Values
        {
            get
            {
                if (_values == null)
                    return new int[0];
                return _values;
            }
        }
        public int N
        {
            get
            {
                return _n;
            }
        }

        public IntegerPartition(int n)
            : this(n, null, n)
        {   
        }

        public IntegerPartition(int n, int[] values, int numberOfTerms)
        {
            if (n < 1)
                throw new ArgumentException("n must be a positive integer", nameof(n));
            if (numberOfTerms < 1)
                throw new ArgumentException("numberOfTerms must be a positive integer", nameof(numberOfTerms));
            if (values == null && numberOfTerms < n)
                throw new ArgumentException("Cannot initialize with fewer than n terms without being given values");
            //if (values != null && values.Length > n)
            //    throw new ArgumentException("n less than number of values given");
            if (values != null && values.Length > numberOfTerms)
                throw new ArgumentException("numberOfTerms is less than number of values given");
            if (values != null)
            {
                int sum = 0;
                for (int i = 0; i < values.Length; i++)
                    sum += values[i];
                if (sum != n)
                    throw new ArgumentException("Sum of given values does not equal n: Given: " + sum.ToString() + "; n: " + n.ToString());
            }
            _n = n;
            _values = new int[numberOfTerms];
            if (values == null)
            {
                for (int i = 0; i < n; i++)
                    _values[i] = 1;
            }
            else
                values.CopyTo(_values, 0);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is IntegerPartition))
                throw new ArgumentException("Invalid type equality check", nameof(obj));
            return Equals((IntegerPartition)obj);
        }

        public bool Equals(IntegerPartition other)
        {
            if (N != other.N)
                return false;
            if (Values.Length != other.Values.Length)
                return false;
            for (int i = 0; i < Values.Length; i++)
                if (Values[i] != other.Values[i])
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            int result = Values[0].GetHashCode();
            for (int i = 1; i < Values.Length; i++)
                result = (((result << 5) + result) ^ Values[i].GetHashCode());
            return result;
        }

        public static int GetHashCode(int[] values)
        {
            int result = values[0].GetHashCode();
            for (int i = 1; i < values.Length; i++)
                result = (((result << 5) + result) ^ values[i].GetHashCode());
            return result;
        }

        private static IntegerPartition GetPartition(
            int[] partitionValues, 
            int maxValidIndex, 
            int numberOfTerms,
            int n)
        {
            var values = new int[maxValidIndex + 1];
            for (int i = 0; i <= maxValidIndex; i++)
                values[i] = partitionValues[i];
            return new IntegerPartition(n, values, numberOfTerms);
        }
        public static List<IntegerPartition> GetIntegerPartitions(int n, int maxTerms)
        {
            var result = new List<IntegerPartition>();
            if (n < 1)
                throw new ArgumentException("n must be a positive integer", nameof(n));
            if (maxTerms < 1)
                throw new ArgumentException("maxTerms must be a positive integer", nameof(n));

            // Algorithm found here:
            // https://pdfs.semanticscholar.org/9613/c1666b5e48a5035141c8927ade99a9de450e.pdf

            // Initialize X_i
            var currentPartitionValues = new int[n];
            for (int i = 0; i < n; i++)
                currentPartitionValues[i] = 1;
            currentPartitionValues[0] = n;
            int maxValidIndex = 0;
            int h = 0;
            result.Add(GetPartition(currentPartitionValues, maxValidIndex, maxTerms, n));

            // Main loop
            while (currentPartitionValues[0] != 1)
            {
                if (currentPartitionValues[h] == 2)
                {
                    maxValidIndex += 1;
                    currentPartitionValues[h] = 1;
                    h -= 1;
                }
                else
                {
                    int r = currentPartitionValues[h] - 1;
                    int t = maxValidIndex - h + 1;
                    currentPartitionValues[h] = r;
                    while (t >= r)
                    {
                        h += 1;
                        currentPartitionValues[h] = r;
                        t = t - r;
                    }
                    if (t == 0)
                    {
                        maxValidIndex = h;
                    }
                    else
                    {
                        maxValidIndex = h + 1;
                        if (t > 1)
                        {
                            h += 1;
                            currentPartitionValues[h] = t;
                        }
                    }
                }
                if (maxValidIndex < maxTerms)
                    result.Add(GetPartition(currentPartitionValues, maxValidIndex, maxTerms, n));
            }
            return result;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var result = new StringBuilder();
            for (int i = 0; i < Values.Length; i++)
            {
                if (result.Length == 0)
                    result.Append("(");
                else
                    result.Append(",");
                result.Append(Values[i].ToString(format, formatProvider));
            }
            result.Append(")");
            return result.ToString();
        }

        public override string ToString()
        {
            return ToString(null, CultureInfo.InvariantCulture);
        }
        
    }
}
