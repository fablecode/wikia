using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.Models.Article;
using wikia.Models.Article.NewArticles;

namespace wikia.unit.tests.WikiaArticleTests
{
    [TestFixture]
    public class NewArticleTests
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
        [TestCase(101)]
        public void Given_An_Invalid_Limit_Should_Throw_ArgumentOutOfRangeException(int limit)
        {
            // Arrange

            // Act
            Func<Task<NewArticleResultSet>> act = () => _sut.NewArticles(new NewArticleRequestParameters{ Limit = limit});

            // Assert
            act.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestCase(-1)]
        [TestCase(100)]
        public void Given_An_Invalid_MinArticleQuality_Should_Throw_ArgumentOutOfRangeException(int minArticleQuality)
        {
            // Arrange

            // Act
            Func<Task<NewArticleResultSet>> act = () => _sut.NewArticles(new NewArticleRequestParameters { MinArticleQuality = minArticleQuality });

            // Assert
            act.ShouldThrow<ArgumentOutOfRangeException>();
        }

    }
}