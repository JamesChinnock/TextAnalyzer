
namespace AnalyzerTests.UnitTests
{
    using System.Collections.Generic;

    using Analyzer.Strategy;
    using AnalyzerTests.UnitTests.TestData;
    using NUnit.Framework;

    /// <summary>
    /// Represents unit test to be performed on an instance of the <see cref="BasicStrategy"/> class
    /// </summary>
    [TestFixture]
    public class WhenCallingCountOnABasicStrategyThenA
    {
        /// <summary>
        /// Private method to hold the <see cref="BasicStrategy"/> instance we are testing
        /// </summary>
        private BasicStrategy _basicStrategy;

        /// <summary>
        /// Setsup the objects and services common to all tests in this class
        /// </summary>
        [SetUp]
        public void Init()
        {
            this._basicStrategy = new BasicStrategy();
        }

        /// <summary>
        /// String of one word will return one entry with a count of 1
        /// </summary>
        [Test]
        public void StringContainingOneWordReturnsOneWordWithCountOfOne()
        {
            var expectedResults = new Dictionary<string, int> { { "sentence", 1 } };
            var actualResults = this._basicStrategy.Count("sentence");

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// String including punctuation ignores the punctation characters and accumulates the words accurately
        /// </summary>
        [Test]
        public void StringIncludingPunctuationIgnoresThePunctationCharacters()
        {
            var expectedResults = new Dictionary<string, int>
                    {
                        { "this", 2 }, { "is", 2 }, { "a", 1 }, { "statement", 1 }, { "and", 1 }, { "so", 1 } 
                    };

            var actualResults = this._basicStrategy.Count(TestString.ShortStringIncludingPunctuation);
            Assert.That(actualResults.Count, Is.EqualTo(6));
            CollectionAssert.AreEquivalent(expectedResults, actualResults);
        }

        /// <summary>
        /// String without punctuation accumulates the words accurately
        /// </summary>
        [Test]
        public void StringWithoutPunctuationAccumulatesTheWordsAccurately()
        {
            var expectedResults = new Dictionary<string, int>
                    {
                        { "this", 2 }, { "is", 2 }, { "a", 1 }, { "statement", 1 }, { "and", 1 }, { "so", 1 } 
                    };

            var actualResults = this._basicStrategy.Count(TestString.ShortStringWithoutPunctuation);
            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(6));
        }

        /// <summary>
        /// String containing only punctuation does not return any results
        /// </summary>
        [Test]
        public void StringContainingOnlyPunctuationReturnsNoResults()
        {
            var expectedResults = new Dictionary<string, int>();
            var actualResults = this._basicStrategy.Count(";");

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// String containing one word made up entirely of numbers returns one word
        /// </summary>
        [Test]
        public void StringOfOneWordContainingOnlyNumbersReturnsOneWord()
        {
            var expectedResults = new Dictionary<string, int> { { "123", 1 } };
            var actualResults = this._basicStrategy.Count("123");

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// String containing two words with one made up entirely of numbers returns two words
        /// </summary>
        [Test]
        public void StringOfTwoWordsOneOfWhichIsEntirelyNumbericReturnsBothWords()
        {
            var expectedResults = new Dictionary<string, int> { { "123", 1 }, { "sentence", 1 } };
            var actualResults = this._basicStrategy.Count("123 sentence");

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// String containing two words both made up entirely of numbers returns two words
        /// </summary>
        [Test]
        public void StringOfTwoWordsBothOfWhichAreEntirelyNumbericReturnsBothWords()
        {
            var expectedResults = new Dictionary<string, int> { { "123", 1 }, { "456", 1 } };
            var actualResults = this._basicStrategy.Count("123 456");

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// String containing one word built of numbers, letters and symbols returns the one word
        /// </summary>
        [Test]
        public void StringOfOneWordBuiltOfMixedAlphaNumericAndSymbolsReturnsTheWord()
        {
            var expectedResults = new Dictionary<string, int> { { "p$55word", 1 } };
            var actualResults = this._basicStrategy.Count("P$55word");
            
            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// String containing two words built of numbers, letters and symbols returns both words
        /// </summary>
        [Test]
        public void StringOfTwoWordsBuiltOfMixedAlphaNumericAndSymbolsReturnsBothWords()
        {
            var expectedResults = new Dictionary<string, int> { { "p$55word", 1 }, { "$ec0nd", 1 } };
            var actualResults = this._basicStrategy.Count("P$55word $ec0nd");

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// String containing only symbols returns one word
        /// </summary>
        [Test]
        public void StringContainingOnlySymbolsReturnsOneWord()
        {
            var expectedResults = new Dictionary<string, int> { { "^£$", 1 } };
            var actualResults = this._basicStrategy.Count("^£$");

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// String containing two words built of symbols returns both words
        /// </summary>
        [Test]
        public void StringOfTwoWordsBuiltOfOnlySymbolsReturnsBothWords()
        {
            var expectedResults = new Dictionary<string, int> { { "^£$", 1 }, { "$$£^", 1 } };
            var actualResults = this._basicStrategy.Count("^£$ $$£^");

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// String of the same word repeated will return one entry with a count of 2
        /// </summary>
        [Test]
        public void StringOfTwoWordsTheSameReturnsOneWordWithCountOfTwo()
        {
            var expectedResults = new Dictionary<string, int> { { "sentence", 2 } };
            var actualResults = this._basicStrategy.Count("sentence sentence");

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// String of the same word repeated, but separated by a comma, will return one entry with a count of 2
        /// </summary>
        [Test]
        public void StringOfTwoWordsTheSameSeparatedByaComma()
        {
            var expectedResults = new Dictionary<string, int> { { "sentence", 2 } };
            var actualResults = this._basicStrategy.Count("sentence, sentence");
            
            Assert.That(actualResults.Count, Is.EqualTo(1));
            CollectionAssert.AreEquivalent(expectedResults, actualResults);
        }
    }
}
