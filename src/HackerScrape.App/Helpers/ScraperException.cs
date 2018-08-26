using System;
using System.Runtime.Serialization;

namespace HackerScrape.App.Helpers
{
    public class ScraperException : Exception
    {
        public ScraperException()
        {
        }

        public ScraperException(string message) : base(message)
        {
        }

        public ScraperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ScraperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}