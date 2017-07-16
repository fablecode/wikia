using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Models.RelatedPages;

namespace wikia.unit.tests.WikiRelatedPages
{
    public class ArticlesTests
    {
        private Api.WikiRelatedPages _sut;

        private string _domainUrl = "http://yugioh.wikia.com";

        [SetUp]
        public void SetUp()
        {
            _sut = new Api.WikiRelatedPages(_domainUrl);
        }

        [Test]
        public void Given_Null_RequestParameters_Should_Throw_ArgumentNullException()
        {
            // Arrange

            // Act
            Func<Task<RelatedPages>> act = () => _sut.Articles((RelatedArticlesRequestParameters)null);

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Given_Null_ArticleIds_List_Should_Throw_ArgumentException()
        {
            // Arrange

            // Act
            Func<Task<RelatedPages>> act = () => _sut.Articles((int[])null);

            // Assert
            act.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void An_Given_Empty_ArticleIds_List_Should_Throw_ArgumentException()
        {
            // Arrange

            // Act
            Func<Task<RelatedPages>> act = () => _sut.Articles(new int[0]);

            // Assert
            act.ShouldThrow<ArgumentException>();
        }

    }
}