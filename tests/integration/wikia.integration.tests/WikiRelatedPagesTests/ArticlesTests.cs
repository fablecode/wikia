using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.integration.tests.WikiaArticleTests;

namespace wikia.integration.tests.WikiRelatedPagesTests
{
    [TestFixture]
    public class ArticlesTests
    {
        [TestFixture]
        public class WikiVariablesTests
        {
            [TestCaseSource(typeof(WikiaArticleTestData), nameof(WikiaArticleTestData.ArticleIdTestUrlData))]
            public async Task Given_A_DomainUrl__Should_Retrieve_Related_Articles(string domainUrl, int articleId)
            {
                // Arrange
                var sut = new WikiRelatedPages(domainUrl);

                // Act
                var result = await sut.Articles(articleId);

                // Assert
                result.Should().NotBeNull();
            }

        }
    }
}