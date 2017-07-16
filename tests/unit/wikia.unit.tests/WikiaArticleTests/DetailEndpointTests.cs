using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.Models.Article.Details;

namespace wikia.unit.tests.WikiaArticleTests
{
    [TestFixture]
    public class DetailEndpointTests
    {
        private IWikiArticle _sut;

        private string _domainUrl = "http://yugioh.wikia.com";

        [SetUp]
        public void SetUp()
        {
            _sut = new WikiArticle(_domainUrl);
        }

        [Test]
        public void Given_Null_RequestParameters_Should_Throw_ArgumentNullException()
        {
            // Arrange

            // Act
            Func<Task<ExpandedArticleResultSet>> act = () => _sut.Details(null);

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}