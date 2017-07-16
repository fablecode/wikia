using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.Models.Article.Details;
using wikia.Models.Article.NewArticles;
using wikia.Models.Article.Popular;

namespace wikia.unit.tests.WikiaArticleTests
{
    [TestFixture]
    public class PopularArticlesTests
    {
        private IWikiArticle _sut;

        private string _domainUrl = "http://yugioh.wikia.com";

        [SetUp]
        public void SetUp()
        {
            _sut = new WikiArticle(_domainUrl);
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(11)]
        public void Given_An_Invalid_Limit_Should_Throw_ArgumentOutOfRangeException(int limit)
        {
            // Arrange

            // Act
            Func<Task<ExpandedArticleResultSet>> act = () => _sut.PopularArticle<ExpandedArticleResultSet>(new PopularArticleRequestParameters { Limit = limit }, true);

            // Assert
            act.ShouldThrow<ArgumentOutOfRangeException>();
        }

    }
}