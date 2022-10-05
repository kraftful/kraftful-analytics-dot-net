using System;
using System.Collections.Generic;
using Segment;

namespace KraftfulAnalytics.Core
{
    public class SegmentEventSender : IEventSender
    {
        protected static string KRAFTFUL_INGESTION_URL = "https://analytics-ingestion.kraftful.com";
        protected IAnalyticsClient client;

        protected string lastUserId;

        public SegmentEventSender(string apiKey)
            : this(
                  new Client(apiKey, new Config(
                    host: KRAFTFUL_INGESTION_URL
                  ))
              )
        {

        }

        public SegmentEventSender(IAnalyticsClient client)
        {
            this.client = client;
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

