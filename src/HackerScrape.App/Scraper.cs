using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerScrape.App
{
    public class Scraper
    {
        public async Task<IList<HnItem>> ScrapeIt()
        {
            var scraper = new HnScraper();
            await scraper.StartAsync();
            Console.WriteLine("post Start");
            return scraper.Items;
        }
    }
}