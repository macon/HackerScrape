using Shouldly;
using Xunit;

namespace HackerScrape.App.Tests.HnItemBuilderTests
{
    public class RankTests
    {
        [Theory]
        [InlineData("45", 45)]
        [InlineData("-45", HnItemBuilder.DefaultRank)]
        [InlineData("AZ", HnItemBuilder.DefaultRank)]
        public void rank_is_parsed_correctly(string rankText, int expectedResult)
        {
            var hnItem = HnItemBuilder.Build("", "", "", "", "", rankText);
            hnItem.Rank.ShouldBe(expectedResult);
        }
    }
}