using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;

namespace wikia.integration.tests.SearchSuggestionsTests
{
    [TestFixture]
    public class SuggestedPhrasesTests
    {
        [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.SearchTestData))]
        public async Task Given_A_DomainUrl__Should_Retrieve_Search_Results(string domainUrl, string query)
        {
            // Arrange
            var sut = new WikiSearchSuggestions(domainUrl);

            // Act
            var result = await sut.SuggestedPhrases(query);

            // Assert
            result.Should().NotBeNull();
        }
    }
}