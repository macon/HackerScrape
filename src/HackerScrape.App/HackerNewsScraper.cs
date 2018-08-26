using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HackerScrape.App.Helpers;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ScrapySharp.Extensions;

namespace HackerScrape.App
{
    public class HackerNewsScraper
    {
        public async Task<IList<HnItem>> ScrapeItemsAsync(int numItems, CancellationTokenSource cts = null)
        {
            if (numItems <= 0) throw new ArgumentOutOfRangeException(nameof(numItems));
            if (cts == null) cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            
            var scrapeTasks = MakeScrapeTasks(numItems, cts);

            var hnItems = new List<HnItem>();
            
            while (scrapeTasks.Count > 0)
            {
                var finishedTask = await Task.WhenAny(scrapeTasks.Values.Select(x => x.Task));
                var pageUri = scrapeTasks[finishedTask.Id].PageUri;
                scrapeTasks.Remove(finishedTask.Id);

                try
                {
                    hnItems.AddRange(await finishedTask);
                }
                catch (Exception e)
                {
                    throw new ScraperException($"Scraping error(s) for page: {pageUri}", e);
                }
            }
            return hnItems;
        }

        private IDictionary<int, ScrapeTaskWrapper> MakeScrapeTasks(int numItems, CancellationTokenSource cts)
        {
            var numPagesToFetch = Math.Ceiling((double) numItems / HackerNewsConstants.PageSize);
            var scrapeTasks = new Dictionary<int, ScrapeTaskWrapper>();
            
            for (var i = 0; i < numPagesToFetch; i++)
            {
                var pageUri = HackerNewsConstants.Uri.Combine($"news?p={i + 1}").ToString();
                var scrapeTask = new ScrapeTaskWrapper(pageUri, ScrapePageAsync(pageUri, cts.Token));

                scrapeTasks.Add(scrapeTask.Task.Id, scrapeTask);
            }

            return scrapeTasks;
        }

        private async Task<IList<HnItem>> ScrapePageAsync(string uri, CancellationToken token)
        {
            var webGet = new HtmlWeb();
            var htmlDoc = await webGet.LoadFromWebAsync(uri, token);
            var rows = htmlDoc.DocumentNode.CssSelect("table.itemlist tr.athing");
            
            var hnItems = new List<HnItem>();
            
            foreach (var row in rows)
            {
                var anchor = row.CssSelect("td.title a").First();
                var anchorUri = anchor.GetAttributeValue("href");
                var rank = row.CssSelect("td.title span.rank").First();
                var trSubtext = row.NextSibling;
                var tdSubtext = trSubtext.CssSelect("td.subtext").First();
                var author = tdSubtext.CssSelect("a.hnuser").FirstOrDefault();
                var points = tdSubtext.CssSelect("span.score").FirstOrDefault();
                var subtextAnchors = trSubtext.CssSelect("td.subtext > a").ToList();
                
                var commentsAnchor = subtextAnchors.SingleOrDefault(x =>
                    x.InnerText.Contains("comment", StringComparison.InvariantCultureIgnoreCase));

                var hnItem = HnItemBuilder.Build(
                    anchor?.InnerText,
                    anchorUri,
                    author?.InnerText,
                    ExtractPoints(points),
                    ExtractComments(commentsAnchor),
                    ExtractRank(rank));
                
                hnItems.Add(hnItem);
            }

            return hnItems;
        }

        private static string ExtractComments(HtmlNode commentsNode)
        {
            var commentsText = commentsNode?.InnerText;
            if (string.IsNullOrWhiteSpace(commentsText)) return "";
            
            var commentsParts = commentsText.Split(new []{"&nbsp;", " "}, StringSplitOptions.RemoveEmptyEntries);
            return commentsParts.Length == 0 ? "" : commentsParts[0];
        }
        
        private static string ExtractPoints(HtmlNode pointsNode)
        {
            var pointsText = pointsNode?.InnerText;
            if (string.IsNullOrWhiteSpace(pointsText)) return "";
            
            var pointsParts = pointsText.Split(new []{"&nbsp;", " "}, StringSplitOptions.RemoveEmptyEntries);
            return pointsParts.Length == 0 ? "" : pointsParts[0];
        }
        
        private static string ExtractRank(HtmlNode rankNode)
        {
            var rankText = rankNode?.InnerText;
            return string.IsNullOrWhiteSpace(rankText) ? "" : rankText.TrimEnd('.', ' ');
        }
    }
}