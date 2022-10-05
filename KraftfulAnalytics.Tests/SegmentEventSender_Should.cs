using System;
using System.Collections.Generic;
using KraftfulAnalytics.Core;
using Moq;
using Segment;
using Xunit;

namespace KraftfulAnalytics.Tests
{
    public class SegmentEventSender_Should
    {
        private Mock<IAnalyticsClient> mockClient;

        public SegmentEventSender_Should()
        {
            mockClient = new Mock<IAnalyticsClient>();
        }

        [Fact]
        public void IdentifyAndKeepUserId()
        {
            var sender = new SegmentEventSender(mockClient.Object);

            var newUserId = "test-user-id-3";
            sender.Identify(newUserId);
            mockClient.Verify(client => client.Identify(newUserId, null), Times.Once(), "Should call Identify with new user id");

            sender.Track("Test Feature A");
            mockClient.Verify(client => client.Track(newUserId, "Test Feature A", null as IDictionary<string, object>), Times.Once(), "Should call Track with new user id");
            mockClient.VerifyNoOtherCalls();
        }
    }
}

