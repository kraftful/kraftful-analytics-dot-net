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

            var sentAt = DateTime.Now.ToUniversalTime().ToString(Constants.UTCFormat);
            batch.SentAt = sentAt;

            string json = JsonConvert.SerializeObject(batch);

            // messageId
            Assert.Contains($"\"messageId\":\"{batch.MessageId}\"", json);
            // writeKey
            Assert.Contains($"\"writeKey\":\"{testWriteKey}\"", json);
            // sentAt
            Assert.Contains($"\"sentAt\":\"{sentAt}\"", json);
        }

        [Fact]
        public void SerializeBatchActions()
        {
            var anonId = "anon-user";
            var userId = "test-user";
            var appVersion = "1.0.123";
            var appDeviceContext = new Context();
            appDeviceContext.Add("app", new Dict() {
                { "version", appVersion },
            });
            appDeviceContext.Add("device", new Dict()
            {
                { "id", "some-id" },
                { "model", "Android SDK built for x86" },
                { "type", "android" },
            });
            appDeviceContext.Add("os", new Dict()
            {
                { "name", "Android" },
            });
            var trackAction = new Track(
                userId,
                "test-track",
                null,
                new Options()
                    .SetAnonymousId(anonId)
                    .SetContext(appDeviceContext)
            );
            var batch = new Batch(this.testWriteKey, new List<BaseAction>()
            {
                trackAction,
            });

            var sentAt = DateTime.Now.ToUniversalTime().ToString(Constants.UTCFormat);
            batch.SentAt = sentAt;

            string json = JsonConvert.SerializeObject(batch);

            // app
            Assert.Contains($"\"version\":\"{appVersion}\"", json);
            // device
            Assert.Contains($"\"id\":\"some-id\"", json);
            Assert.Contains($"\"model\":\"Android SDK built for x86\"", json);
            Assert.Contains($"\"type\":\"android\"", json);
            // os
            Assert.Contains($"\"name\":\"Android\"", json);
        }
    }
}

