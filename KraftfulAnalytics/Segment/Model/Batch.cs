using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace Segment.Model
{
    public class Batch
    {
        [JsonProperty(PropertyName = "writeKey")]
        public string WriteKey { get; set; }

        [JsonProperty(PropertyName="messageId")]
        public string MessageId { get; private set; }

        [JsonProperty(PropertyName="sentAt")]
        public string SentAt { get; set; }

        [JsonProperty(PropertyName = "batch")]
        public List<BaseAction> batch { get; set; }

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
