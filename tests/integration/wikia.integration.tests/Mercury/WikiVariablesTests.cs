using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.integration.tests.WikiaArticleTests;

namespace wikia.integration.tests.Mercury
{
    [TestFixture]
    public class WikiVariablesTests
    {
        [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.WikiUrlsTestData))]
        public async Task Given_A_DomainUrl__Should_Retrieve_WikiVariables(string domainUrl)
        {
            // Arrange
            var sut = new WikiMercury(domainUrl);

            // Act
            var result = await sut.WikiVariables();

            // Assert
            result.Should().NotBeNull();
        }
    }
}