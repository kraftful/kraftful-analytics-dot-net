using System;
using System.Collections.Generic;

namespace Kraftful.Analytics.Core
{
    public interface IEventSender
    {
        string AnonymousUserId { get; set; }
        string UserId { get; set; }

        void Track(string name);
        void Track(string name, IDictionary<string, object> properties);
        void Identify(string userId);
        void Identify(string userId, IDictionary<string, object> properties);
        void Screen(string name);
        void Screen(string name, IDictionary<string, object> properties);
    }
}

