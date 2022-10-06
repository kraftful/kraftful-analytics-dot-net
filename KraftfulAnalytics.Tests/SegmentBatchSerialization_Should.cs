using System;
using System.Collections.Generic;
using Kraftful.Analytics.Core;
using Moq;
using Newtonsoft.Json;
using Segment;
using Segment.Model;
using Xunit;

namespace Kraftful.Analytics.Tests
{
    public class SegmentBatchSerialization_Should
    {
        private string testWriteKey = "abc-123";

        public SegmentBatchSerialization_Should()
        {
        }

        [Fact]
        public void SerializeBatchFieldsWithEmptyActions()
        {
            var batch = new Batch(this.testWriteKey, new List<BaseAction>());

            string json = JsonConvert.SerializeObject(batch);

            Assert.Contains($"\"writeKey\": \"{testWriteKey}\"", json);
        }
    }
}

