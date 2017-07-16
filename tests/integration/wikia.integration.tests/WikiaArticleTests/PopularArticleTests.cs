using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.Models.Article.NewArticles;
using wikia.Models.Article.Popular;

namespace wikia.integration.tests.WikiaArticleTests
{
    [TestFixture]
    public class PopularArticleTests
    {
        [TestCaseSource(typeof(WikiaArticleTestData), nameof(WikiaArticleTestData.WikiUrlsTestData))]
        public async Task Given_A_DomainUrl_Should_Successfully_Retrieve_Simple_Popular_Articles(string domainUrl)
        {
            // Arrange
            var sut = new WikiArticle(domainUrl);

            // Act
            var result = await sut.PopularArticleSimple(new PopularArticleRequestParameters());

            // Assert
            result.Should().NotBeNull();
        }

        [TestCaseSource(typeof(WikiaArticleTestData), nameof(WikiaArticleTestData.WikiUrlsTestData))]
        public async Task Given_A_DomainUrl_Should_Successfully_Retrieve_Detailed_Popular_Articles(string domainUrl)
        {
            // Arrange
            var sut = new WikiArticle(domainUrl);

            // Act
            var result = await sut.PopularArticleDetail(new PopularArticleRequestParameters());

            // Assert
            result.Should().NotBeNull();
        }


    }
}