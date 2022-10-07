using System;
using Kraftful.Analytics.Core;

namespace Kraftful.Analytics.SDK
{
    public static class KraftfulAnalytics
    {
        public static IEventSender sender = null;

        public static bool IsInitialized {
            get;
            private set;
        }

        static KraftfulAnalytics()
        {
            Reset();
        }

        public static void Reset()
        {
            IsInitialized = false;
        }

        public static void Initialize(string apiKey)
        {
            var sender = new SegmentEventSender(apiKey, null);
            InitializeWith(sender);
        }

        public static void InitializeWith(IEventSender sender)
        {
            KraftfulAnalytics.sender = sender;
            IsInitialized = true;
        }

        public static void TrackFeatureUse(string feature)
        {
            if (sender == null) return;

            sender.Track(feature);
        }

        public static void TrackSignInStart()
        {
            if (sender == null) return;

            sender.Track("Sign In Start");
        }

        public static void TrackSignInSuccess(string userId) 
        {
            if (sender == null) return;

            if (userId != null)
            {
                sender.Identify(userId);
            }

            sender.Track("Sign In Success");
        }

        public static void TrackConnectionStart()
        {
            if (sender == null) return;

            sender.Track("Connection Start");
        }

        public static void TrackConnectionSuccess()
        {
            if (sender == null) return;

            sender.Track("Connection Success");
        }

        public static void TrackAppReturn(string userId)
        {
            if (sender == null) return;

            if (userId != null)
            {
                sender.Identify(userId);
            }

            sender.Track("Return");
        }

    }
}

