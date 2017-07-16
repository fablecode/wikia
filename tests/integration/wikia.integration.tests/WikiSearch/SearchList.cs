﻿using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using wikia.Models.Search;

namespace wikia.integration.tests.WikiSearch
{
    [TestFixture]
    public class SearchList
    {
        [TestCaseSource(typeof(WikiaTestData), nameof(WikiaTestData.SearchTestData))]
        public async Task Given_A_DomainUrl__Should_Retrieve_Search_Results(string domainUrl, string query)
        {
            // Arrange
            var sut = new Api.WikiSearch(domainUrl);

            // Act
            var result = await sut.SearchList(new SearchListRequestParameter(query));

            // Assert
            result.Should().NotBeNull();
        }

    }
}