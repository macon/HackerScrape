using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IronWebScraper;
using Newtonsoft.Json;

namespace HackerScrape.App
{
    public class HnScraper : WebScraper
    {
        private string _uri = "https://news.ycombinator.com";
        public List<HnItem> Items { get; }
        private readonly int numPosts;

        public HnScraper()
        {
            Items = new List<HnItem>();
            numPosts = 100;
        }
        
        public HnScraper(string uri) : base()
        {
            _uri = uri;
        }
        
        public override void Init()
        {
            LoggingLevel = LogLevel.All;
            ObeyRobotsDotTxt = false;
//            Identities.Add(new HttpIdentity(){UserAgent = "*"});
            _sw.Start();
            Request(_uri, Parse, new HttpIdentity(){UserAgent = "*"});
//            Request(_uri, Parse);
            Console.WriteLine("Init");
        }
        
        private Stopwatch _sw= new Stopwatch();
        public override void Parse(Response response)
        {
//            Items.Clear();
            Console.WriteLine($"Parse being called after {_sw.ElapsedMilliseconds}ms");

            foreach (var tr in response.Css("table.itemlist tr.athing"))
            {
                var rank = tr.Css("td.title span.rank").First();
                var anchor = tr.Css("td.title a").First();
                var strTitle = anchor.TextContentClean;
//                Console.WriteLine(strTitle);
                var d = new HnItem
                {
                    Title = strTitle, 
                    Uri = anchor.GetAttribute("href"),
                    Rank = Convert.ToInt32(rank.InnerText.TrimEnd('.'))
                };
                Items.Add(d);
                if (Items.Count == numPosts) return;
            }

            var moreLink = response.Css("table.itemlist tr td.title a.morelink");
            
            if (moreLink.Any() && Items.Count < numPosts)
            {
                var nextPage = moreLink[0].Attributes["href"];
//                Request(nextPage, Parse);
                Request(nextPage, Parse, new HttpIdentity(){UserAgent = "*"});
                _sw.Restart();

                Console.WriteLine("New request submitted");
            }
        }
    }
}