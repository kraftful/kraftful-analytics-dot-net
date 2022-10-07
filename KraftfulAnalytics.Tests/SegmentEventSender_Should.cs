using System;
using System.Collections.Generic;
using Kraftful.Analytics.Core;
using Moq;
using Segment;
using Segment.Model;
using Xamarin.Essentials;
using Xunit;

namespace Kraftful.Analytics.Tests
{
    public class SegmentEventSender_Should
    {
        private Mock<IAnalyticsClient> mockClient;
        private Mock<IAppDeviceInfo> mockDeviceInfo;
        private string mockAnonymousId = Guid.NewGuid().ToString();
        private AppDeviceInfoData mockDeviceInfoData;

        public SegmentEventSender_Should()
        {
            mockClient = new Mock<IAnalyticsClient>();
            mockDeviceInfoData = new AppDeviceInfoData();
            mockDeviceInfoData.AppVersion = "test-version";
            mockDeviceInfoData.DeviceId = "test-device-id";
            mockDeviceInfoData.DeviceModel = "test-device-model";
            mockDeviceInfoData.DeviceType = "test-device-type";
            mockDeviceInfoData.OsName = "test-os-name";
            mockDeviceInfo = new Mock<IAppDeviceInfo>();
            mockDeviceInfo.Setup(d => d.GetAppDeviceInfo()).Returns(mockDeviceInfoData);
        }

        [Fact]
        public void IdentifyAndKeepUserId()
        {
            var sender = new SegmentEventSender(mockClient.Object, mockDeviceInfo.Object, mockAnonymousId);

            var newUserId = "test-user-id-3";
            sender.Identify(newUserId);
            mockClient.Verify(client => client.Identify(newUserId, null, It.Is<Options>(opt => opt.AnonymousId == mockAnonymousId)), Times.Once(), "Should call Identify with new user id");

            sender.Track("Test Feature A");
            mockClient.Verify(client => client.Track(newUserId, "Test Feature A", null as IDictionary<string, object>, It.Is<Options>(opt => opt.AnonymousId == mockAnonymousId)), Times.Once(), "Should call Track with new user id");
            mockClient.VerifyNoOtherCalls();
        }

        [Fact]
        public void AddAppDeviceContextToEvents()
        {
            var sender = new SegmentEventSender(mockClient.Object, mockDeviceInfo.Object, mockAnonymousId);

            var newUserId = "test-user-id-3";
            sender.Identify(newUserId);

            sender.Track("Test Feature A");
            mockClient.Verify(client =>
                client.Track(
                    newUserId,
                    "Test Feature A",
                    null as IDictionary<string, object>,
                    It.Is<Options>(opt =>
                        opt.AnonymousId == mockAnonymousId
                        && (string)((Dict)opt.Context["app"])["version"] == mockDeviceInfoData.AppVersion
                        && (string)((Dict)opt.Context["device"])["id"] == mockDeviceInfoData.DeviceId
                        && (string)((Dict)opt.Context["device"])["model"] == mockDeviceInfoData.DeviceModel
                        && (string)((Dict)opt.Context["device"])["type"] == mockDeviceInfoData.DeviceType
                        && (string)((Dict)opt.Context["os"])["name"] == mockDeviceInfoData.OsName

                    )
                ),
                Times.Once(),
                "Should call Track with app device context"
            );
        }
    }
}

