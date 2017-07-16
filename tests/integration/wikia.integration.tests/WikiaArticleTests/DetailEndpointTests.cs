using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;

namespace wikia.integration.tests.WikiaArticleTests
{
    [TestFixture]
    public class DetailEndpointTests
    {
        [TestCaseSource(typeof(WikiaArticleTestData), nameof(WikiaArticleTestData.SimpleTestUrlData))]
        public async Task Given_A_DomainUrl_And_ArticleId_Should_Successfully_Retrieve_Domain_Article_Details(string domainUrl, int articleId)
        {
            // Arrange
            var sut = new WikiArticle(domainUrl);

            // Act
            var result = await sut.Details(articleId);

            // Assert
            result.Should().NotBeNull();
        }
    }
}