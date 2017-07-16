using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;

namespace wikia.integration.tests.WikiaArticleTests
{
    [TestFixture]
    public class AlphabeticalListTests
    {
        [TestCaseSource(typeof(WikiaArticleTestData), nameof(WikiaArticleTestData.ArticleCategoryTestData))]
        public async Task Given_A_DomainUrl_And_An_Article_Category_Should_Successfully_Retrieve_ArticleList(string domainUrl, string category)
        {
            // Arrange
            var sut = new WikiArticle(domainUrl);

            // Act
            var result = await sut.AlphabeticalList(category);

            // Assert
            result.Should().NotBeNull();
        }
    }
}