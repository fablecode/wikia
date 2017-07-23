using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Api;
using wikia.Models.User;

namespace wikia.integration.tests.UserTests
{
    [TestFixture]
    public class DetailstTests
    {
        [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.UserTestData))]
        public async Task Given_A_DomainUrl__Should_Retrieve_User_Details(string domainUrl, string id)
        {
            // Arrange
            var sut = new WikiUser(domainUrl);

            // Act
            var result = await sut.Details(new UserRequestParameters {Ids = new HashSet<string> {id}});

            // Assert
            result.Should().NotBeNull();
        }

    }
}