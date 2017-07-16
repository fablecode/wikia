using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.integration.tests.WikiaArticleTests;

namespace wikia.integration.tests.Navigation
{
    [TestFixture]
    public class NavigationLinksTests
    {
        [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.WikiUrlsTestData))]
        public async Task Given_A_DomainUrl__Should_Retrieve_WikiVariables(string domainUrl)
        {
            // Arrange
            var sut = new WikiNavigation(domainUrl);

            // Act
            var result = await sut.NavigationLinks();

            // Assert
            result.Should().NotBeNull();
        }
    }
}