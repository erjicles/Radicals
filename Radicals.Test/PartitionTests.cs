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
            var partitions1 = Partitions.Partition.GetPartitions(5, 5);
            var partitions2 = Partitions.Partition.GetPartitions(5, 3);

            Assert.Equal(7, partitions1.Count);
            Assert.Equal(5, partitions2.Count);
        }
    }
}
