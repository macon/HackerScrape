using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HackerScrape.App;
using Newtonsoft.Json;

namespace HackerScrape.CLI
{
    public class Program
    {
        private static readonly int _timeoutSecs = 10;
        
        private static void Main(string[] args)
        {
            var numItems = 30;
            
            if (args.Length != 2 || args[0].ToLower() != "--posts" || !int.TryParse(args[1], out numItems))
            {
                Console.WriteLine("Usage: hackernews --posts n");
                Console.WriteLine("Where: --posts n........how many posts to print. A positive integer <= 100.");
                Environment.Exit(0);
            }
            
            var scraper = new HackerNewsScraper();
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(_timeoutSecs));
            IList<HnItem> itemResults = new List<HnItem>();
            
            var scrapeItemsAsync = scraper.ScrapeItemsAsync(numItems, cts);

            try
            {
                scrapeItemsAsync.Wait(cts.Token);
                itemResults = scrapeItemsAsync.Result;
            }
            catch (OperationCanceledException)
            {
                Console.Error.WriteLine("The scrape operation timed out.");
                Environment.Exit(1);
            }
            catch (AggregateException e)
            {
                Console.Error.WriteLine(e.InnerException.Message);
                Environment.Exit(2);
            }

            Console.WriteLine(JsonConvert.SerializeObject(itemResults.OrderBy(x => x.Rank).Take(numItems).ToList(), Formatting.Indented));
        }
    }
}