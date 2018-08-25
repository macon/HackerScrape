using System;
using HackerScrape.App;
using Newtonsoft.Json;

namespace HackerScrape.CLI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var scraper = new Scraper();
            var itemsTask = scraper.ScrapeIt();
            itemsTask.Wait();
            var items = itemsTask.Result;

            Console.WriteLine(JsonConvert.SerializeObject(items,Formatting.Indented));
            Console.ReadKey();
        }
    }
}