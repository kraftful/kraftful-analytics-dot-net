using System;
namespace Segment
{
    public static class Constants
    {
        // Use 3 decimals for seconds to satisfy bigquery formatting
        public static string UTCFormat { get; } = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK";
    }
}

