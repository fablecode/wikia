using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;

namespace wikia.integration.tests.WikiaArticleTests
{
    [TestFixture]
    public class DetailEndpointTests
    {
        [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.ArticleIdTestUrlData))]
        public async Task Given_A_DomainUrl_And_ArticleId_Should_Successfully_Deserialize_Article_Details_Json(string domainUrl, int articleId)
        {
            // Arrange
            var sut = new WikiArticle(domainUrl);

            // Act
            var result = await sut.Details(articleId);

            // Assert
            result.Should().NotBeNull();

            //Note: Details returns a Dictionary instead of an array as per documentation
            // http://yugioh.wikia.com/api/v1/#!/Articles/getDetails_get_1
        }
    }
}