using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.Models.SearchSuggestions;

namespace wikia.unit.tests.SearchSuggestionsTests
{
    [TestFixture]
    public class SuggestedPhrasesTests
    {
        private IWikiSearchSuggestions _sut;

        private string _domainUrl = "http://yugioh.wikia.com";

        [SetUp]
        public void SetUp()
        {
            _sut = new WikiSearchSuggestions(_domainUrl);
        }

        [Test]
        public void Given_Null_RequestParameters_Should_Throw_ArgumentNullException()
        {
            // Arrange

            // Act
            Func<Task<SearchSuggestionsPhrases>> act = () => _sut.SuggestedPhrases(null);

            // Assert
            act.ShouldThrow<ArgumentException>();
        }
    }
}