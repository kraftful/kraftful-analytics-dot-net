using System;
using System.Collections.Generic;
using System.Linq;
using Segment;

namespace Kraftful.Analytics.Core
{
    public class SegmentEventSender : IEventSender
    {
        protected static string KRAFTFUL_INGESTION_PROD_URL = "https://analytics-ingestion.kraftful.com";
        protected static string KRAFTFUL_INGESTION_STAGING_URL = "https://analytics-ingestion-staging.kraftful.com";
        protected IAnalyticsClient client;

        protected string lastUserId;

        public SegmentEventSender(string apiKey)
            : this(
                  new Client(apiKey, new Config(
                    host: KRAFTFUL_INGESTION_STAGING_URL
                  ))
              )
        {

        }

        public SegmentEventSender(IAnalyticsClient client)
        {
            this.client = client;

            Segment.Logger.Handlers += SegmentLoggerHandler;
        }

        protected void SegmentLoggerHandler(Logger.Level level, string message, IDictionary<string, object> args)
        {
            if (args != null)
                message = args.Keys.Aggregate(message,
                    (current, key) => current + $" {"" + key}: {"" + args[key]},");

            Console.WriteLine($"[Segment-{level}] {message}");
        }

        public void Identify(string userId)
        {
            Identify(userId, null);
        }

        public void Identify(string userId, IDictionary<string, object> properties)
        {
            client.Identify(userId, properties);
            lastUserId = userId;
        }

        public void Track(string name)
        {
            Track(name, null);
        }

        public void Track(string name, IDictionary<string, object> properties)
        {
            client.Track(lastUserId, name, properties);
        }
    }
}

