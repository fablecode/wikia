using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;
using wikia.Api;
using wikia.Models.User;

namespace wikia.unit.tests.UserTests
{
    [TestFixture]
    public class DetailstTests
    {
        private IWikiUser _sut;

        private string _domainUrl = "http://yugioh.wikia.com";

        [SetUp]
        public void SetUp()
        {
            _sut = new WikiUser(_domainUrl);
        }

        [Test]
        public void Given_Null_RequestParameters_Should_Throw_ArgumentNullException()
        {
            // Arrange

            // Act
            Func<Task<UserResultSet>> act = () => _sut.Details(null);

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Given_RequestParameters_With_Empty_Ids_Should_Throw_ArgumentException()
        {
            // Arrange

            // Act
            Func<Task<UserResultSet>> act = () => _sut.Details(new UserRequestParameters());

            // Assert
            act.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Given_RequestParameters_With_Ids_Count_More_Than_100_Should_Throw_ArgumentException()
        {
            // Arrange

            // Fixture setup
            var fixture = new Fixture{RepeatCount = 101};
            var ids = fixture.Create<HashSet<string>>();

            // Act
            Func<Task<UserResultSet>> act = () => _sut.Details(new UserRequestParameters{ Ids = ids });

            // Assert
            act.ShouldThrow<ArgumentException>();
        }

    }
}