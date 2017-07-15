using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using wikia.Api;
using wikia.Configuration;
using wikia.Models.Article.Simple;

namespace wikia.unit.tests.WikiaArticleTest
{
    [TestFixture]
    public class SimpleTests
    {
        private IWikiArticle _wikiArticle;

        [SetUp]
        public void SetUp()
        {
            var wikiaHttpClient = Substitute.For<IWikiaHttpClient>();

            _wikiArticle = new WikiArticle("http://naruto.wikia.com", WikiaSettings.RelativeApiUrl, wikiaHttpClient);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-4958)]
        public void Given_An_Invalid_ArticleId_Should_Throw_ArgumentOutOfRangeException(int articleId)
        {
            // Arrange

            // Act
            Func<Task<ContentResult>> act =() => _wikiArticle.Simple(articleId);

            // Assert
            act
                .ShouldThrow<ArgumentOutOfRangeException>();
        }
    }
}