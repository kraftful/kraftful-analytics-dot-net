using System;
using System.Collections.Generic;
using System.Linq;
using Kraftful.Analytics.Core;
using Segment;
using Segment.Model;
using Xamarin.Essentials;

namespace Kraftful.Analytics.Core
{
    public class SegmentEventSender : IEventSender
    {
        protected static string KRAFTFUL_INGESTION_PROD_URL = "https://analytics-ingestion.kraftful.com";
        protected static string KRAFTFUL_INGESTION_STAGING_URL = "https://analytics-ingestion-staging.kraftful.com";
        protected IAnalyticsClient client;
        protected IAppDeviceInfo deviceInfo;


        public string AnonymousUserId { get; set; }
        public string UserId { get; set; }

        public SegmentEventSender(string apiKey) : this(apiKey, null) { }

        public SegmentEventSender(string apiKey, string anonymousId)
            : this(
                  new Client(apiKey, new Config(
                    host: KRAFTFUL_INGESTION_STAGING_URL
                  )),
                  new XamarinAppDeviceInfo(),
                  anonymousId
              )
        {

        }

        public SegmentEventSender(IAnalyticsClient client, IAppDeviceInfo deviceInfo, string anonymousId)
        {
            this.client = client;
            this.deviceInfo = deviceInfo;
            this.AnonymousUserId = anonymousId ?? Guid.NewGuid().ToString();

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
            UserId = userId;
            client.Identify(userId, null, getDefaultOptions());
        }

        public void Track(string name)
        {
            Track(name, null);
        }

        public void Track(string name, IDictionary<string, object> properties)
        {
            client.Track(UserId, name, properties, getDefaultOptions());
        }

        public void Screen(string name)
        {
            Screen(name, null);
        }

        public void Screen(string name, IDictionary<string, object> properties)
        {
            client.Screen(UserId, name, properties, getDefaultOptions());
        }

        public Options getDefaultOptions()
        {
            var options = new Options();

            // Add anonymousId
            options.SetAnonymousId(AnonymousUserId);

            // Add app/device/os context
            options.SetContext(getDefaultContext());

            return options;
        }

        public Context getDefaultContext()
        {
            var context = new Context();

            var info = deviceInfo.GetAppDeviceInfo();

            // context.app
            context.Add("app", new Dict() {
                { "version", info.AppVersion },
            });
            // context.device
            context.Add("device", new Dict()
            {
                { "id", info.DeviceId },
                { "model", info.DeviceModel },
                { "type", info.DeviceType},
            });
            // context.os
            context.Add("os", new Dict()
            {
                { "name", info.OsName },
            });

            return context;
        }
    }
}

