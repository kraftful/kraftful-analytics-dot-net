using System;
using System.Collections.Generic;

namespace KraftfulAnalytics.Core
{
    public interface IEventSender
    {
        void Track(string name);
        void Track(string name, IDictionary<string, object> properties);
        void Identify(string userId);
        void Identify(string userId, IDictionary<string, object> properties);
    }
}

