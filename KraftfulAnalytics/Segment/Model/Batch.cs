using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace Segment.Model
{
    public class Batch
    {
        [JsonProperty(PropertyName = "writeKey")]
        internal string WriteKey { get; set; }

        [JsonProperty(PropertyName="messageId")]
        internal string MessageId { get; private set; }

        [JsonProperty(PropertyName="sentAt")]
        internal string SentAt { get; set; }

        [JsonProperty(PropertyName = "batch")]
        internal List<BaseAction> batch { get; set; }

        public Batch()
        {
            this.MessageId = Guid.NewGuid ().ToString ();
        }

        public Batch(string writeKey, List<BaseAction> batch) : this()
        {
            this.WriteKey = writeKey;
            this.batch = batch;
        }
    }
}
