using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerScrape.App.Helpers
{
    internal class ScrapeTaskWrapper
    {
        public ScrapeTaskWrapper(string uri, Task<IList<HnItem>> task)
        {
            PageUri = uri;
            Task = task;
        }
        
        public string PageUri { get; }
        public Task<IList<HnItem>> Task { get; }
    }
}