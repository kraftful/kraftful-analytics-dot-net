using System;
using Xunit;
using Kraftful.Analytics.Core;
using Kraftful.Analytics.SDK;
using Moq;

namespace Kraftful.Analytics.Tests
{
    [Collection("KraftulAnalytics Static")]
    public class KraftfulAnalytics_Should
    {
        private Mock<IEventSender> mockSender;
        private string mockAnonId = Guid.NewGuid().ToString();

        public KraftfulAnalytics_Should()
        {
            mockSender = new Mock<IEventSender>();
            mockSender.SetupGet(sender => sender.AnonymousUserId).Returns(mockAnonId);
            KraftfulAnalytics.Reset();
        }

        [Fact]
        public void InitializeWithMockEventSender()
        {
            Assert.False(KraftfulAnalytics.IsInitialized, "IsInitialized should be false");

            KraftfulAnalytics.InitializeWith(mockSender.Object);

            Assert.True(KraftfulAnalytics.IsInitialized, "IsInitialized should be true");
        }

        [Fact]
        public void TrackFeatureUse()
        {
            KraftfulAnalytics.InitializeWith(mockSender.Object);

            KraftfulAnalytics.TrackFeatureUse("Test Feature A");

            mockSender.Verify(sender => sender.Track("Test Feature A"), Times.Once(), "Track should be called with correct feature name");
        }

        [Fact]
        public void TrackSignInStart()
        {
            KraftfulAnalytics.InitializeWith(mockSender.Object);

            KraftfulAnalytics.TrackSignInStart();

            mockSender.Verify(sender => sender.Track("Sign In Start"), Times.Once(), "Track should be called with Sign In Start");
        }

        [Fact]
        public void TrackSignInSuccessWithNullUserId()
        {
            KraftfulAnalytics.InitializeWith(mockSender.Object);

            KraftfulAnalytics.TrackSignInSuccess(null);

            mockSender.Verify(sender => sender.Track("Sign In Success"), Times.Once(), "Track should be called with Sign In Success");
            mockSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void TrackSignInSuccessWithUserId()
        {
            KraftfulAnalytics.InitializeWith(mockSender.Object);

            var newUserId = "test-user-id-1";
            KraftfulAnalytics.TrackSignInSuccess(newUserId);

            mockSender.Verify(sender => sender.Identify(newUserId), "Should call Identify with new user id");
            mockSender.Verify(sender => sender.Track("Sign In Success"), Times.Once(), "Track should be called with Sign In Success");
            mockSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void TrackConnectionStart()
        {
            KraftfulAnalytics.InitializeWith(mockSender.Object);

            KraftfulAnalytics.TrackConnectionStart();

            mockSender.Verify(sender => sender.Track("Connection Start"), Times.Once(), "Track should be called with Connection Start");
        }

        [Fact]
        public void TrackConnectionSuccess()
        {
            KraftfulAnalytics.InitializeWith(mockSender.Object);
            
            KraftfulAnalytics.TrackConnectionSuccess();

            mockSender.Verify(sender => sender.Track("Connection Success"), Times.Once(), "Track should be called with Connection Success");
        }

        [Fact]
        public void TrackAppReturnWithNullUserId()
        {
            KraftfulAnalytics.InitializeWith(mockSender.Object);

            KraftfulAnalytics.TrackAppReturn(null);

            mockSender.Verify(sender => sender.Track("Return"), Times.Once(), "Track should be called with Return");
            mockSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void TrackAppReturnWithUserId()
        {
            KraftfulAnalytics.InitializeWith(mockSender.Object);

            var newUserId = "test-user-id-2";
            KraftfulAnalytics.TrackAppReturn(newUserId);

            mockSender.Verify(sender => sender.Identify(newUserId), "Should call Identify with new user id");
            mockSender.Verify(sender => sender.Track("Return"), Times.Once(), "Track should be called with Return");
            mockSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void TrackScreenView()
        {
            KraftfulAnalytics.InitializeWith(mockSender.Object);

            KraftfulAnalytics.TrackScreenView("Test Screen A");

            mockSender.Verify(sender => sender.Screen("Test Screen A"), Times.Once(), "Screen should be called with correct screen name");
        }
    }
}

