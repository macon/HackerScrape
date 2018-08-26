using Shouldly;
using Xunit;

namespace HackerScrape.App.Tests.HnItemBuilderTests
{
    public class TitleTests
    {
        [Fact]
        public void normal_title_is_unchanged()
        {
            var expectedValue = new string('A', 256);
            var hnItem = HnItemBuilder.Build(expectedValue, "", "", "", "", "");

            hnItem.Title.Length.ShouldBe(256);
            hnItem.Title.ShouldBe(expectedValue);
        }

        [Fact]
        public void long_title_is_trimmed_to_256()
        {
            var expectedValue = new string('A', 256);
            var hnItem = HnItemBuilder.Build(new string('A', 257), "", "", "", "", "");

            hnItem.Title.Length.ShouldBe(256);
            hnItem.Title.ShouldBe(expectedValue);
        }

        [Fact]
        public void null_title_is_defaulted()
        {
            const string expectedValue = HnItemBuilder.UnknownTitle;
            var hnItem = HnItemBuilder.Build("", "", "", "", "", "");

            hnItem.Title.ShouldBe(expectedValue);
        }
    }
}