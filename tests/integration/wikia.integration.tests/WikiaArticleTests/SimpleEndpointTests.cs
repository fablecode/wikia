using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;

namespace wikia.integration.tests.WikiaArticleTests
{
    [TestFixture]
    public class SimpleEndpointTests
    {
        [TestCaseSource(nameof(SimpleTestUrlData))]
        public async Task Given_A_DomainUrl_And_ArticleId_Should_Successfully_Retrieve_Domain_ArticleInfo(string domainUrl, int articleId)
        {
            // Arrange
            var sut = new WikiArticle(domainUrl);

            // Act
            var result = await sut.Simple(articleId);

            // Assert
            result.Should().NotBeNull();
        }

        #region Test Data

        private static IEnumerable<TestCaseData> SimpleTestUrlData
        {
            get
            {
                yield return new TestCaseData
                (
                    "http://yugioh.wikia.com",
                    300400 // Eclipse  Wyvern card page
                );
                yield return new TestCaseData
                (
                    "http://naruto.wikia.com/",
                    1612 // Rock character page
                );
                yield return new TestCaseData
                (
                    "http://elderscrolls.wikia.com/",
                    41277 // Orc page
                );
            }
        }

        #endregion
    }
}