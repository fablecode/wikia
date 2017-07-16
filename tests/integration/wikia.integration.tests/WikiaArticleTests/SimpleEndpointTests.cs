using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;

namespace wikia.integration.tests.WikiaArticleTests
{
    [TestFixture]
    public class SimpleEndpointTests
    {
        [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.ArticleIdTestUrlData))]
        public async Task Given_A_DomainUrl_And_ArticleId_Should_Successfully_Retrieve_Domain_Simple_ArticleInfo(string domainUrl, int articleId)
        {
            // Arrange
            var sut = new WikiArticle(domainUrl);

            // Act
            var result = await sut.Simple(articleId);

            // Assert
            result.Should().NotBeNull();
        }
    }
}