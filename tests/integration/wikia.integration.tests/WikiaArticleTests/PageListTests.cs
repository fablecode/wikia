using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;

namespace wikia.integration.tests.WikiaArticleTests
{
    [TestFixture]
    public class PageListTests
    {
        [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.ArticleCategoryTestData))]
        public async Task Given_A_DomainUrl_And_An_Article_Category_Should_Successfully_Retrieve_PageList(string domainUrl, string category)
        {
            // Arrange
            var sut = new WikiArticle(domainUrl);

            // Act
            var result = await sut.PageList(category);

            // Assert
            result.Should().NotBeNull();
        }

        [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.ArticleCategoryTestData))]
        public async Task Given_A_DomainUrl_And_An_Article_Category_NewArticleResultSet_Should_Contain_Atleast_1_Item(string domainUrl, string category)
        {
            // Arrange
            var sut = new WikiArticle(domainUrl);

            // Act
            var result = await sut.PageList(category);

            // Assert
            result.Items.Should().NotBeEmpty();
        }

    }
}