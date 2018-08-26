using System;
using Newtonsoft.Json;

namespace HackerScrape.App
{
    public class HnItem
    {
        internal HnItem(string title, string uri, string author, int points, int comments, int rank)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(author));
            if (comments < 0) throw new ArgumentOutOfRangeException(nameof(comments));
            if (points < 0) throw new ArgumentOutOfRangeException(nameof(points));
            if (rank < 0) throw new ArgumentOutOfRangeException(nameof(rank));

            Title = title;
            Uri = uri;
            Author = author;
            Points = points;
            Comments = comments;
            Rank = rank;
        }
        
        [JsonProperty("title")]
        public string Title { get; }
        
        [JsonProperty("uri")]
        public string Uri { get; }
        
        [JsonProperty("author")]
        public string Author { get; }
        
        [JsonProperty("points")]
        public int Points { get; }
        
        [JsonProperty("comments")]
        public int Comments { get; }
        
        [JsonProperty("rank")]
        public int Rank { get; }
    }
}