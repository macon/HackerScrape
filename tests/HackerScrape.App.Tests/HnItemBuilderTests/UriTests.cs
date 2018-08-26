using System;
using HackerScrape.App.Helpers;
using ScrapySharp.Extensions;
using Shouldly;
using Xunit;

namespace HackerScrape.App.Tests.HnItemBuilderTests
{
    public class UriTests
    {
        [Fact]
        public void relative_uri_is_converted_to_absolute()
        {
            const string relativeUri = "item?id=17841832";
            var expectedValue = HackerNewsConstants.Uri.Combine(relativeUri).ToString();

            var hnItem = HnItemBuilder.Build("", relativeUri, "", "", "", "");

            Uri.IsWellFormedUriString(hnItem.Uri, UriKind.Absolute).ShouldBe(true);
            hnItem.Uri.ShouldBe(expectedValue);
        }
        
        [Fact]
        public void absolute_uri_is_unchanged()
        {
            const string expectedValue = "https://www.bbc.co.uk/somePage?x=y";

            var hnItem = HnItemBuilder.Build("", expectedValue, "", "", "", "");

            Uri.IsWellFormedUriString(hnItem.Uri, UriKind.Absolute).ShouldBe(true);
            hnItem.Uri.ShouldBe(expectedValue);
        }

        [Fact]
        public void empty_uri_is_defaulted()
        {
            var expectedValue = HackerNewsConstants.Uri.ToString();

            var hnItem = HnItemBuilder.Build("", "", "", "", "", "");
            
            hnItem.Uri.ShouldBe(expectedValue);
        }
    }
}