using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.Models.Article.NewArticles;

namespace wikia.integration.tests.WikiaArticleTests
{
    [TestFixture]
    public class NewArticleTests
    {
        [TestCaseSource(typeof(WikiaArticleTestData), nameof(WikiaArticleTestData.WikiUrlsTestData))]
        public async Task Given_A_DomainUrl_Should_Successfully_Retrieve_NewArticles(string domainUrl)
        {
            // Arrange
            var sut = new WikiArticle(domainUrl);

            // Act
            var result = await sut.NewArticles(new NewArticleRequestParameters());

            // Assert
            result.Should().NotBeNull();
        }
    }
}