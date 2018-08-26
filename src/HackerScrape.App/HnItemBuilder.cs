using System;
using HackerScrape.App.Helpers;
using ScrapySharp.Extensions;

namespace HackerScrape.App
{
    public static class HnItemBuilder
    {
        public const string UnknownTitle = "untitled";
        public const string UnknownAuthor = "unknown";
        public const int DefaultComments = 0;
        public const int DefaultPoints = 0;
        public const int DefaultRank = 0;

        public static HnItem Build(string title, string uri, string author, string points, string comments, string rank)
        {
            return new HnItem(
                ParseTitle(title),
                ParseUri(uri),
                ParseAuthor(author),
                ParsePoints(points),
                ParseComments(comments),
                ParseRank(rank)
            );
        }

        private static int ParseRank(string rankText)
        {
            if (string.IsNullOrWhiteSpace(rankText)) return DefaultRank;

            var rank = int.TryParse(rankText, out var rankValue) ? rankValue : DefaultRank;
            return rank < 0 ? DefaultRank : rank;
        }

        private static string ParseUri(string uri)
        {
            return Uri.IsWellFormedUriString(uri, UriKind.Absolute)
                ? uri
                : HackerNewsConstants.Uri.Combine(uri).ToString();
        }
        
        private static int ParseComments(string commentsText)
        {
            if (string.IsNullOrWhiteSpace(commentsText)) return DefaultComments;
            
            var comments = int.TryParse(commentsText, out var commentsValue) ? commentsValue : DefaultComments;
            return comments < 0 ? DefaultComments : comments;
        }

        private static int ParsePoints(string pointsText)
        {
            if (string.IsNullOrWhiteSpace(pointsText)) return DefaultPoints;

            var points = int.TryParse(pointsText, out var pointsValue) ? pointsValue : DefaultPoints;
            return points < 0 ? DefaultPoints : points;
        }

        private static string ParseAuthor(string author, int maxLen = 256)
        {
            return string.IsNullOrWhiteSpace(author) ? UnknownAuthor.Left(maxLen) : author.Trim().Left(maxLen);
        }
        
        private static string ParseTitle(string title, int maxLen = 256)
        {
            return string.IsNullOrWhiteSpace(title) ? UnknownTitle.Left(maxLen) : title.Trim().Left(maxLen);
        }
    }
}