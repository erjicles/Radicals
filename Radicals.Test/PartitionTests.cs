using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Radicals.Test
{
    public class PartitionTests
    {
        [Fact]
        public void GetPartitionsTest()
        {
            //5
            //4 1
            //3 2
            //3 1 1
            //2 2 1
            //2 1 1 1
            //1 1 1 1 1
            var partitions1 = Partitions.Partition.GetPartitions(5, 5);
            var expected1 = 7;
            //5
            //4 1
            //3 2
            //3 1 1
            //2 2 1
            var partitions2 = Partitions.Partition.GetPartitions(5, 3);
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
            var partitions3 = Partitions.Partition.GetPartitions(8, 8);
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
            var partitions4 = Partitions.Partition.GetPartitions(8, 5);
            var expected4 = 18;
            


            Assert.Equal(expected1, partitions1.Count);
            Assert.Equal(expected2, partitions2.Count);
            Assert.Equal(expected3, partitions3.Count);
            Assert.Equal(expected4, partitions4.Count);
        }
    }
}
