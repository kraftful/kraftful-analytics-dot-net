using System;
using KraftfulAnalytics.Core;

namespace KraftfulAnalytics
{
    public static class KraftfulAnalytics
    {
        private static IEventSender sender = null;

        public static bool IsInitialized {
            get;
            private set;
        }

        public static string UserId {
            get;
            private set;
        }

        static KraftfulAnalytics()
        {
            Reset(null);
        }

        public static void Reset(string anonymousUserId)
        {
            if (anonymousUserId == null) anonymousUserId = Guid.NewGuid().ToString();

            IsInitialized = false;
            UserId = anonymousUserId;
        }

        public static void Initialize(string apiKey)
        {
            var sender = new SegmentEventSender(apiKey);
            InitializeWith(sender);
        }

        public static void InitializeWith(IEventSender sender)
        {
            KraftfulAnalytics.sender = sender;
            IsInitialized = true;
            KraftfulAnalytics.sender.Identify(UserId);
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
                UserId = userId;
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
                UserId = userId;
                sender.Identify(userId);
            }

            sender.Track("Return");
        }

    }
}

