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
        [TestCaseSource(nameof(InvalidUrlTestData))]
        public void Given_An_Invalid_Url_Should_Throw_ArgumentException(string url)
        {
            // Arrange
            
            // Act
            Action act = () => UrlHelper.GenerateUrl(url, default(Dictionary<string, string>));

            // Assert
            act
                .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Given_Null_Parameters_Should_Not_Throw_ArgumentNullException()
        {
            // Arrange 
            var url = "http://www.google.com";

            // Act
            Action act = () => UrlHelper.GenerateUrl(url, default(Dictionary<string, string>));

            // Assert
            act
                .ShouldNotThrow<ArgumentNullException>();
        }

        [TestCaseSource(nameof(InvalidUrlTestData))]
        public void Given_An_Invalid_Url_Should_Return_False(string url)
        {
            // Arrange
            const bool expected = false;

            // Act
           var result = UrlHelper.IsValidUrl(url);

            // Assert
            result.Should().Be(expected);
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

        #region Test Data

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

        private static IEnumerable<TestCaseData> InvalidUrlTestData
        {
            get
            {
                yield return new TestCaseData(null);
                yield return new TestCaseData("");
                yield return new TestCaseData("      ");
                yield return new TestCaseData("dfsfs/dsfsfsf");
                yield return new TestCaseData("file://awesomefile.exe");
                yield return new TestCaseData("ldap://lightweightdirectoryaccess/user");

            }
        }

        #endregion
    }
}