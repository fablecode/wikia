using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.Models.Search;

namespace wikia.integration.tests.WikiSearchTests
{
    [TestFixture]
    public class SearchList
    {
        [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.SearchTestData))]
        public async Task Given_A_DomainUrl_And_A_Query_Should_Successfully_Retrieve_SearchResults(string domainUrl, string query)
        {
            // Arrange
            var sut = new WikiSearch(domainUrl);

            // Act
            var result = await sut.SearchList(new SearchListRequestParameter(query));

            // Assert
            result.Should().NotBeNull();
        }

        [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.SearchTestData))]
        public async Task Given_A_DomainUrl_And_A_Query_SearchResults_Should_Contain_Atleast_1_Item(string domainUrl, string query)
        {
            // Arrange
            var sut = new WikiSearch(domainUrl);

            // Act
            var result = await sut.SearchList(new SearchListRequestParameter(query));

            // Assert
            result.Items.Should().NotBeEmpty();
        }

    }
}