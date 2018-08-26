using Shouldly;
using Xunit;

namespace HackerScrape.App.Tests.HnItemBuilderTests
{
    public class AuthorTests
    {
        [Fact]
        public void normal_author_is_unchanged()
        {
            var expectedValue = new string('A', 256);
            var hnItem = HnItemBuilder.Build("", "", expectedValue, "", "", "");

            hnItem.Author.Length.ShouldBe(256);
            hnItem.Author.ShouldBe(expectedValue);
        }

        [Fact]
        public void long_author_is_trimmed_to_256()
        {
            var expectedValue = new string('A', 256);
            var hnItem = HnItemBuilder.Build("", "", new string('A', 257), "", "", "");

            hnItem.Author.Length.ShouldBe(256);
            hnItem.Author.ShouldBe(expectedValue);
        }

        [Fact]
        public void null_author_is_defaulted()
        {
            const string expectedValue = HnItemBuilder.UnknownAuthor;
            var hnItem = HnItemBuilder.Build("", "", "", "", "", "");

            hnItem.Author.ShouldBe(expectedValue);
        }
    }
}