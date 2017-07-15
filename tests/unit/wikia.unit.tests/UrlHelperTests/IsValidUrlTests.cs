using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using wikia.Helper;

namespace wikia.unit.tests.UrlHelperTests
{
    [TestFixture]
    public class IsValidUrlTests
    {
        [TestCaseSource(nameof(InvalidUrlTestData))]
        public void Given_An_Invalid_Url_Should_Return_False(string url)
        {
            // Arrange
            const bool expected = false;

            // Act
            var result = UrlHelper.IsValidUrl(url);

            // Assert
            result.Should().Be(expected);
        }

        #region Test Data

        private static IEnumerable<TestCaseData> InvalidUrlTestData
        {
            get
            {
                yield return new TestCaseData(null);
                yield return new TestCaseData("");
                yield return new TestCaseData("      ");
                yield return new TestCaseData("dfsfs/dsfsfsf");
                yield return new TestCaseData("file://awesomefile.exe");
                yield return new TestCaseData("ldap://lightweightdirectoryaccess/user");

            }
        }

        #endregion
    }
}