using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Radicals.Combinatorics
{
    public readonly struct Permutation
        : IFormattable, IEquatable<Permutation>
    {
        private readonly int[] _values;
        public int[] Values
        {
            get
            {
                if (_values == null)
                    return new int[0];
                return _values;
            }
        }

        public Permutation(int[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            _values = new int[values.Length];
            values.CopyTo(_values, 0);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Permutation))
                throw new ArgumentException("Invalid type equality check", nameof(obj));
            return Equals((Permutation)obj);
        }

        public bool Equals(Permutation other)
        {
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

        public static IList<Permutation> ArrangeInSlots(
            int[] values, 
            int numberOfSlots,
            bool keepDuplicates)
        {
            // Handle more values than slots
            var valueSets = new List<int[]>();
            if (values.Length > numberOfSlots)
            {
                // Get all subsets of values choose number of slots
                var ones = Enumerable.Repeat(1, numberOfSlots).ToArray();
                var flaggedValuePermutations = ArrangeInSlots(ones, values.Length, false);
                foreach (Permutation flaggedValues in flaggedValuePermutations)
                {
                    var valueSet = new int[numberOfSlots];
                    var currentValueSetIndex = 0;
                    for (int i = 0; i < flaggedValues.Values.Length; i++)
                    {
                        if (flaggedValues.Values[i] == 1)
                        {
                            valueSet[currentValueSetIndex] = values[i];
                            currentValueSetIndex++;
                        }
                    }
                    valueSets.Add(valueSet);
                }
            }
            else
            {
                valueSets.Add(values);
            }

            // Elements:                [3 2 1]
            // x x x x x
            // Perm:                    [3 2 1 0 0]
            // Register:                [0 1 2]
            // Current element index:   2
            // -> Register[current element index]++: 3
            // -> Register[current element index} > 4 false
            // Perm:                    [3 2 0 1 0]
            // Register:                [0 1 3]
            // Current element index:   2
            // -> Register[current element index]++: 4
            // -> Register[current element index] > 4: false
            // Perm:                    [3 2 0 0 1]
            // Register:                [0 1 4]
            // Current element index:   2
            // -> Register[current element index]++: 5
            // -> Register[current element index] > 4: true
            // -> Current element index--: 1
            // -> Register[current element index]++: 2
            // -> Current element index++: 2
            // -> Register[current element index] = Register[current element index - 1] - 1: 1

            // -> Current eleme
            // Perm:                    [3 1 2 0 0]
            // Register:                [0 2 1]
            // Current element index:   2

            // Add initial position
            var result = new List<Permutation>();
            var foundPermutations = new HashSet<int>();
            foreach (int[] valueSet in valueSets)
            {
                var currentPermutation = new int[numberOfSlots];
                int currentElementIndex = 0;
                int[] elementPositionIndexRegister = Enumerable.Repeat(-1, valueSet.Length).ToArray();
                var assignedSlots = new HashSet<int>();
                int numberOfValuesAssignedToSlots = 0;
                while (currentElementIndex >= 0)
                {
                    // Get next open slot
                    bool foundSlot = false;
                    for (int i = elementPositionIndexRegister[currentElementIndex] + 1; i < numberOfSlots; i++)
                    {
                        if (!assignedSlots.Contains(i))
                        {
                            elementPositionIndexRegister[currentElementIndex] = i;
                            currentPermutation[elementPositionIndexRegister[currentElementIndex]] = valueSet[currentElementIndex];
                            foundSlot = true;
                            assignedSlots.Add(i);
                            numberOfValuesAssignedToSlots++;
                            break;
                        }
                    }
                    if (foundSlot)
                    {
                        // Is this a valid permutation?
                        // Aka, are all values assigned to a slot?
                        if (numberOfValuesAssignedToSlots == valueSet.Length)
                        {
                            // Add the permutation
                            var hash = GetHashCode(currentPermutation);
                            if (keepDuplicates || !foundPermutations.Contains(hash))
                            {
                                var perm = new Permutation(currentPermutation);
                                result.Add(perm);
                                foundPermutations.Add(hash);
                            }
                            currentPermutation[elementPositionIndexRegister[currentElementIndex]] = 0;
                            assignedSlots.Remove(elementPositionIndexRegister[currentElementIndex]);
                            numberOfValuesAssignedToSlots--;
                        }
                        else
                        {
                            currentElementIndex++;
                        }
                    }
                    if (!foundSlot || currentElementIndex >= valueSet.Length)
                    {
                        // Reset the current element
                        currentPermutation[elementPositionIndexRegister[currentElementIndex]] = 0;
                        assignedSlots.Remove(elementPositionIndexRegister[currentElementIndex]);
                        elementPositionIndexRegister[currentElementIndex] = -1;

                        // Shift back to the previous element, and mark it to increment
                        currentElementIndex--;
                        if (currentElementIndex >= 0)
                        {
                            currentPermutation[elementPositionIndexRegister[currentElementIndex]] = 0;
                            assignedSlots.Remove(elementPositionIndexRegister[currentElementIndex]);
                            numberOfValuesAssignedToSlots--;
                        }
                    }
                }
            }

            return result;
        }

        public static IList<Permutation> GetPermutations(Permutation permutation, bool keepDuplicates)
        {
            // https://en.wikipedia.org/wiki/Heap%27s_algorithm
            var result = new List<Permutation>();
            var foundPermutations = new HashSet<int>();

            // Add the initial permutation
            result.Add(permutation);
            foundPermutations.Add(permutation.GetHashCode());

            var c = new int[permutation.Values.Length];
            var a = new int[permutation.Values.Length];
            permutation.Values.CopyTo(a, 0);
            int i = 0;
            while (i < permutation.Values.Length)
            {
                if (c[i] < i)
                {
                    if (i % 2 == 0)
                    {
                        // swap a[0], a[i]
                        var temp = a[0];
                        a[0] = a[i];
                        a[i] = temp;
                    }
                    else
                    {
                        // swap a[c[i]], a[i]
                        var temp = a[c[i]];
                        a[c[i]] = a[i];
                        a[i] = temp;
                    }
                    var hash = GetHashCode(a);
                    if (keepDuplicates || !foundPermutations.Contains(hash))
                    {
                        var perm = new Permutation(a);
                        result.Add(perm);
                        foundPermutations.Add(hash);
                    }
                    c[i] += 1;
                    i = 0;
                }
                else
                {
                    c[i] = 0;
                    i += 1;
                }
            }
            return result;
        }

        public IList<Permutation> GetPermutations(bool keepDuplicates)
        {
            return GetPermutations(this, keepDuplicates);
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
