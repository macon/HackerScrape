using Shouldly;
using Xunit;

namespace HackerScrape.App.Tests.HnItemBuilderTests
{
    public class PointsTests
    {
        [Theory]
        [InlineData("12", 12)]
        [InlineData("-12", HnItemBuilder.DefaultPoints)]
        [InlineData("AA", HnItemBuilder.DefaultPoints)]
        public void points_are_parsed_correctly(string pointsText, int expectedResult)
        {
            var hnItem = HnItemBuilder.Build("", "", "", pointsText, "", "");
            hnItem.Points.ShouldBe(expectedResult);
        }
    }
}