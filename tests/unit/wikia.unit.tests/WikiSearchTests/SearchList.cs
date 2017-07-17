using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.Models.Search;

namespace wikia.unit.tests.WikiSearchTests
{
    [TestFixture]
    public class SearchList
    {
        private IWikiSearch _sut;

        private string _domainUrl = "http://yugioh.wikia.com";

        [SetUp]
        public void SetUp()
        {
            _sut = new WikiSearch(_domainUrl);
        }

        [Test]
        public void Given_Null_RequestParameters_Should_Throw_ArgumentNullException()
        {
            // Arrange

            // Act
            Func<Task<LocalWikiSearchResultSet>> act = () => _sut.SearchList(null);

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }

    }
}