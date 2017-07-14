using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using wikia.Api;
using wikia.Enums;
using wikia.Models.Activity;

namespace wikia.unit.tests
{
    public class WikiActivityTests
    {
        private IWikiActivity _wikiActivity;
        private string _domainUrl = "http://yugioh.wikia.com";

        [SetUp]
        public void SetUp()
        {
            _wikiActivity = new WikiActivity(_domainUrl);
        }

        [Test]
        public void Given_Null_RequestParameters_Should_Throw_ArgumentNullException()
        {
            // Arrange

            // Act
            Func<Task<ActivityResponseResult>> act = () => _wikiActivity.Activity(null, Arg.Any<ActivityEndpoint>());

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}