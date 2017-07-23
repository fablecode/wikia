using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;

namespace wikia.integration.tests.WikiRelatedPagesTests
{
    [TestFixture]
    public class ArticlesTests
    {
        [TestFixture]
        public class WikiVariablesTests
        {
            [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.ArticleIdTestUrlData))]
            public async Task Given_A_DomainUrl_And_An_ArticleId_Should_Successfully_Retrieve_SearchResults(string domainUrl, int articleId)
            {
                // Arrange
                var sut = new WikiRelatedPages(domainUrl);

                // Act
                var result = await sut.Articles(articleId);

                // Assert
                result.Items.Should().NotBeEmpty();
            }

            [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.ArticleIdTestUrlData))]
            public async Task Given_A_DomainUrl_An_ArticleId_RelatedPages_Should_Contain_Atleast_1_Item(string domainUrl, int articleId)
            {
                // Arrange
                var sut = new WikiRelatedPages(domainUrl);

                // Act
                var result = await sut.Articles(articleId);

                // Assert
                result.Items.Should().NotBeEmpty();
            }

        }
    }
}