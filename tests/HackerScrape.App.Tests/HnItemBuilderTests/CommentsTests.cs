using Shouldly;
using Xunit;

namespace HackerScrape.App.Tests.HnItemBuilderTests
{
    public class CommentsTests
    {
        [Theory]
        [InlineData("12", 12)]
        [InlineData("-12", HnItemBuilder.DefaultComments)]
        [InlineData("AA", HnItemBuilder.DefaultComments)]
        public void comments_are_parsed_correctly(string text, int expectedResult)
        {
            var hnItem = HnItemBuilder.Build("", "", "", "", text, "");
            hnItem.Comments.ShouldBe(expectedResult);
        }
    }
}