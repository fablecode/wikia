using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using wikia.Helper;

namespace wikia.unit.tests
{
    [TestFixture]
    public class UrlHelperTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("      ")]
        [TestCase("dfsfs/dsfsfsf")]
        [TestCase("file://awesomefile.exe")]
        [TestCase("ldap://lightweightdirectoryaccess/user")]
        public void Given_An_Invalid_Url_Should_Throw_ArgumentException(string url)
        {
            // Arrange
            
            // Act
            Action act = () => UrlHelper.GenerateUrl(url, default(Dictionary<string, string>));

            // Assert
            act
                .ShouldThrow<ArgumentException>();
        }

        [TestCaseSource(nameof(GenerateUrlTestData))]
        public void Given_A_Valid_Url_And_QueryStringParameters_Should_Generate_Url(string url, Dictionary<string, string> parameters, string expected)
        {
            // Arrange

            // Act
            var result = UrlHelper.GenerateUrl(url, parameters);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        private static IEnumerable<TestCaseData> GenerateUrlTestData
        {
            get
            {
                yield return new TestCaseData
                (
                    "http://yugioh.wikia.com/api/v1/Articles/List", new Dictionary<string, string>
                    {
                        {"category", "Card_Tips"},
                        { "limit", "200"}
                    }, 
                    "http://yugioh.wikia.com/api/v1/Articles/List?category=Card_Tips&limit=200"
                );
                yield return new TestCaseData
                (
                    "http://yugioh.wikia.com/api/v1/Articles/List", new Dictionary<string, string>
                    {
                        {"category", "Card_Tips"},
                        { "limit", "200"},
                        {"offset", "page|414d415a4f4e455353205350454c4c434153544552|93782"}
                    },
                    "http://yugioh.wikia.com/api/v1/Articles/List?category=Card_Tips&limit=200&offset=page%7C414d415a4f4e455353205350454c4c434153544552%7C93782"
                );
            }
        }
    }
}