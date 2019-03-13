using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Radicals.Test
{
    public class CombinatoricsTests
    {
        [Fact]
        public void PartitionAndPermutationsTests()
        {
            //5
            //4 1
            //3 2
            //3 1 1
            //2 2 1
            //2 1 1 1
            //1 1 1 1 1
            var partitions1 = Combinatorics.IntegerPartition.GetIntegerPartitions(5, 5);
            var expected1 = 7;
            //5
            //4 1
            //3 2
            //3 1 1
            //2 2 1
            var partitions2 = Combinatorics.IntegerPartition.GetIntegerPartitions(5, 3);
            var expected2 = 5;
            //8
            //7 1
            //6 2
            //6 1 1
            //5 3
            //5 2 1
            //5 1 1 1
            //4 4
            //4 3 1
            //4 2 2
            //4 2 1 1
            //4 1 1 1 1
            //3 3 2
            //3 3 1 1
            //3 2 2 1
            //3 2 1 1 1
            //3 1 1 1 1 1
            //2 2 2 2
            //2 2 2 1 1
            //2 2 1 1 1 1
            //2 1 1 1 1 1 1
            //1 1 1 1 1 1 1 1
            var partitions3 = Combinatorics.IntegerPartition.GetIntegerPartitions(8, 8);
            var expected3 = 22;
            //8
            //7 1
            //6 2
            //6 1 1
            //5 3
            //5 2 1
            //5 1 1 1
            //4 4
            //4 3 1
            //4 2 2
            //4 2 1 1
            //4 1 1 1 1
            //3 3 2
            //3 3 1 1
            //3 2 2 1
            //3 2 1 1 1
            //2 2 2 2
            //2 2 2 1 1
            var partitions4 = Combinatorics.IntegerPartition.GetIntegerPartitions(8, 5);
            var expected4 = 18;
            //5 0 0
            //0 5 0
            //0 0 5
            //4 1 0
            //4 0 1
            //1 4 0
            //0 4 1
            //1 0 4
            //0 1 4
            //3 2 0
            //3 0 2
            //2 3 0
            //0 3 2
            //2 0 3
            //0 2 3
            //3 1 1
            //1 3 1
            //1 1 3
            //2 2 1
            //2 1 2
            //1 2 2
            var permutations5 = new List<Combinatorics.Permutation>();
            foreach (Combinatorics.IntegerPartition partition in partitions2)
            {
                var permutations = 
                    Combinatorics.Permutation.GetPermutations(
                        new Combinatorics.Permutation(partition.Values), 
                        false);
                foreach (Combinatorics.Permutation permutation in permutations)
                    permutations5.Add(permutation);
            }
            var expected5 = 21;
            //5 0 0
            //5 0 0
            //0 5 0
            //0 5 0
            //0 0 5
            //0 0 5
            //4 1 0
            //4 0 1
            //1 4 0
            //0 4 1
            //1 0 4
            //0 1 4
            //3 2 0
            //3 0 2
            //2 3 0
            //0 3 2
            //2 0 3
            //0 2 3
            //3 1 1
            //3 1 1
            //1 3 1
            //1 3 1
            //1 1 3
            //1 1 3
            //2 2 1
            //2 2 1
            //2 1 2
            //2 1 2
            //1 2 2
            //1 2 2
            var permutations6 = new List<Combinatorics.Permutation>();
            foreach (Combinatorics.IntegerPartition partition in partitions2)
            {
                var permutations =
                    Combinatorics.Permutation.GetPermutations(
                        new Combinatorics.Permutation(partition.Values),
                        true);
                foreach (Combinatorics.Permutation permutation in permutations)
                    permutations6.Add(permutation);
            }
            var expected6 = 30;
            //2 1 0 0 0
            //2 0 1 0 0
            //2 0 0 1 0
            //2 0 0 0 1
            //1 2 0 0 0
            //0 2 1 0 0
            //0 2 0 1 0
            //0 2 0 0 1
            //1 0 2 0 0
            //0 1 2 0 0
            //0 0 2 1 0
            //0 0 2 0 1
            //1 0 0 2 0
            //0 1 0 2 0
            //0 0 1 2 0
            //0 0 0 2 1
            //1 0 0 0 2
            //0 1 0 0 2
            //0 0 1 0 2
            //0 0 0 1 2
            var values7 = new int[2] { 2, 1 };
            var permutations7 = Combinatorics.Permutation.ArrangeInSlots(values7, 5, false);
            var expected7 = 20;
            //1 1 0 0 0
            //1 0 1 0 0
            //1 0 0 1 0
            //1 0 0 0 1
            //0 1 1 0 0
            //0 1 0 1 0
            //0 1 0 0 1
            //0 0 1 1 0
            //0 0 1 0 1
            //0 0 0 1 1
            var values8 = new int[2] { 1, 1 };
            var permutations8 = Combinatorics.Permutation.ArrangeInSlots(values8, 5, false);
            var expected8 = 10;
            //1 1 0 0 0
            //1 0 1 0 0
            //1 0 0 1 0
            //1 0 0 0 1
            //1 1 0 0 0
            //0 1 1 0 0
            //0 1 0 1 0
            //0 1 0 0 1
            //1 0 1 0 0
            //0 1 1 0 0
            //0 0 1 1 0
            //0 0 1 0 1
            //1 0 0 1 0
            //0 1 0 1 0
            //0 0 1 1 0
            //0 0 0 1 1
            //1 0 0 0 1
            //0 1 0 0 1
            //0 0 1 0 1
            //0 0 0 1 1
            var values9 = new int[2] { 1, 1 };
            var permutations9 = Combinatorics.Permutation.ArrangeInSlots(values9, 5, true);
            var expected9 = 20;
            // Arrange 3 2 1 into 2 slots
            // 3 2
            // 2 3
            // 3 1
            // 1 3
            // 2 1
            // 1 2
            var values10 = new int[3] { 3, 2, 1 };
            var permutations10 = Combinatorics.Permutation.ArrangeInSlots(values10, 2, false);
            var expected10 = 6;
            // Arrange 1 1 1 into 2 slots (no duplicates)
            // 1 1
            var values11 = new int[3] { 1, 1, 1 };
            var permutations11 = Combinatorics.Permutation.ArrangeInSlots(values11, 2, false);
            var expected11 = 1;
            // Arrange 1 1 1 into 2 slots (keeping duplicates)
            // 1 1
            // 1 1
            // 1 1
            // 1 1
            // 1 1
            // 1 1
            var values12 = new int[3] { 1, 1, 1 };
            var permutations12 = Combinatorics.Permutation.ArrangeInSlots(values12, 2, true);
            var expected12 = 6;


            Assert.Equal(expected1, partitions1.Count);
            Assert.Equal(expected2, partitions2.Count);
            Assert.Equal(expected3, partitions3.Count);
            Assert.Equal(expected4, partitions4.Count);
            Assert.Equal(expected5, permutations5.Count);
            Assert.Equal(expected6, permutations6.Count);
            Assert.Equal(expected7, permutations7.Count);
            Assert.Equal(expected8, permutations8.Count);
            Assert.Equal(expected9, permutations9.Count);
            Assert.Equal(expected10, permutations10.Count);
            Assert.Equal(expected11, permutations11.Count);
            Assert.Equal(expected12, permutations12.Count);
        }
    }
}
