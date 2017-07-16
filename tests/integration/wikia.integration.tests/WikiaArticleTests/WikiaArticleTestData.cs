using System.Collections.Generic;
using NUnit.Framework;

namespace wikia.integration.tests.WikiaArticleTests
{
    public static class WikiaArticleTestData
    {
        public static IEnumerable<TestCaseData> SimpleTestUrlData
        {
            get
            {
                yield return new TestCaseData
                (
                    "http://yugioh.wikia.com",
                    300400 // Eclipse  Wyvern card page
                );
                yield return new TestCaseData
                (
                    "http://naruto.wikia.com/",
                    1612 // Rock character page
                );
                yield return new TestCaseData
                (
                    "http://elderscrolls.wikia.com/",
                    41277 // Orc page
                );
            }
        }
    }
}