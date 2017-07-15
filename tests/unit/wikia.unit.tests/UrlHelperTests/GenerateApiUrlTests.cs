using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using wikia.Helper;

namespace wikia.unit.tests.UrlHelperTests
{
    [TestFixture]
    public class GenerateApiUrlTests
    {
        /// <summary>
        /// Its's in the slashes.
        /// https://stackoverflow.com/questions/372865/path-combine-for-urls
        /// </summary>
        [TestCaseSource(nameof(ApiUrlTestData))]
        public void Given_An_Invalid_Url_Should_Throw_ArgumentException(string absoluteUrl, string relativeUrl, string expected)
        {
            // Arrange

            // Act
            var result = UrlHelper.GenerateApiUrl(absoluteUrl, relativeUrl);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        #region Test Data

        private static IEnumerable<TestCaseData> ApiUrlTestData
        {
            get
            {
                yield return new TestCaseData
                (
                    "http://yugioh.wikia.com",
                    "/api/v1",
                    "http://yugioh.wikia.com/api/v1"
                );

                yield return new TestCaseData
                (
                    "http://yugioh.wikia.com/api/v1",
                    "/Articles/List",
                    "http://yugioh.wikia.com/api/v1/Articles/List"
                );
                yield return new TestCaseData
                (
                    "http://yugioh.wikia.com/api/v1/",
                    "/Articles/List",
                    "http://yugioh.wikia.com/api/v1/Articles/List"
                );
                yield return new TestCaseData
                (
                    "http://yugioh.wikia.com/api/v1/",
                    "Articles/List",
                    "http://yugioh.wikia.com/api/v1/Articles/List"
                );
            }
        }

        #endregion
    }
}