using System.Collections.Generic;
using NUnit.Framework;

namespace wikia.integration.tests.WikiaArticleTests
{
    public static class WikiaArticleTestData
    {
        public static IEnumerable<TestCaseData> ArticleIdTestUrlData
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

        public static IEnumerable<TestCaseData> ArticleCategoryTestData
        {
            get
            {
                yield return new TestCaseData
                (
                    "http://yugioh.wikia.com",
                    "Card_Tips" // yugioh tips category
                );
                yield return new TestCaseData
                (
                    "http://naruto.wikia.com/",
                    "Characters" // naruto characters
                );
                yield return new TestCaseData
                (
                    "http://elderscrolls.wikia.com/",
                    "Classes" // Skyrim classes
                );
            }
        }

    }
}