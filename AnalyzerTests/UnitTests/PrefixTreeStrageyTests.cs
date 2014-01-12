
namespace AnalyzerTests.UnitTests
{
    using System.Collections.Generic;
    using System.IO;

    using Analyzer.Services;
    using Analyzer.Strategy;
    using AnalyzerTests.UnitTests.TestData;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// Represents unit test to be performed on an instance of the <see cref="BasicStrategy"/> class
    /// </summary>
    [TestFixture]
    public class PrefixTreeStrageyTests
    {
        /// <summary>
        /// Private field to hold the mocked IFileService
        /// </summary>
        private Mock<IFileService> mockFileService = new Mock<IFileService>();

        /// <summary>
        /// Private method to hold the <see cref="BasicStrategy"/> instance we are testing
        /// </summary>
        private PrefixTreeStrategy _treeStrategy;

        /// <summary>
        /// Setsup the objects and services common to all tests in this class
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.mockFileService = new Mock<IFileService>();
            this._treeStrategy = new PrefixTreeStrategy(this.mockFileService.Object);
        }

        /// <summary>
        /// String of one word will return one entry with a count of 1
        /// </summary>
        [Test]
        public void StringIncludingPunctuationIgnoresThePunctationCharacters()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream(TestString.ShortStringIncludingPunctuation));

            var expectedResults = new Dictionary<string, int>
                    {
                        { "this", 2 }, { "is", 2 }, { "a", 1 }, { "statement", 1 }, { "and", 1 }, { "so", 1 } 
                    };
            
            var actualResults = this._treeStrategy.Count(It.IsAny<string>());

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(6));
        }

        /// <summary>
        /// String without punctuation accumulates the words accurately
        /// </summary>
        [Test]
        public void StringWithoutPunctuationAccumulatesTheWordsAccurately()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream(TestString.ShortStringWithoutPunctuation));

            var expectedResults = new Dictionary<string, int>
                    {
                        { "this", 2 }, { "is", 2 }, { "a", 1 }, { "statement", 1 }, { "and", 1 }, { "so", 1 } 
                    };

            var actualResults = this._treeStrategy.Count(It.IsAny<string>());
            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(6));
        }

        /// <summary>
        /// String containing only punctuation does not return any results
        /// </summary>
        [Test]
        public void StringContainingOnlyPunctuationReturnsNoResults()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream(";"));

            var expectedResults = new Dictionary<string, int>();
            var actualResults = this._treeStrategy.Count(It.IsAny<string>());

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// String containing one word made up entirely of numbers returns one word
        /// </summary>
        [Test]
        public void StringOfOneWordContainingOnlyNumbersReturnsOneWord()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream("123"));

            var expectedResults = new Dictionary<string, int> { { "123", 1 } };
            var actualResults = this._treeStrategy.Count(It.IsAny<string>());

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// String containing two words with one made up entirely of numbers returns two words
        /// </summary>
        [Test]
        public void StringOfTwoWordsOneOfWhichIsEntirelyNumbericReturnsBothWords()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream("123 sentence"));

            var expectedResults = new Dictionary<string, int> { { "123", 1 }, { "sentence", 1 } };
            var actualResults = this._treeStrategy.Count(It.IsAny<string>());

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// String containing two words both made up entirely of numbers returns two words
        /// </summary>
        [Test]
        public void StringOfTwoWordsBothOfWhichAreEntirelyNumbericReturnsBothWords()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream("123 456"));

            var expectedResults = new Dictionary<string, int> { { "123", 1 }, { "456", 1 } };
            var actualResults = this._treeStrategy.Count(It.IsAny<string>());

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// String containing one word built of numbers, letters and symbols returns the one word
        /// </summary>
        [Test]
        public void StringOfOneWordBuiltOfMixedAlphaNumericAndSymbolsReturnsTheWord()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream("p$55word"));

            var expectedResults = new Dictionary<string, int> { { "p$55word", 1 } };
            var actualResults = this._treeStrategy.Count(It.IsAny<string>());

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// String containing two words built of numbers, letters and symbols returns both words
        /// </summary>
        [Test]
        public void StringOfTwoWordsBuiltOfMixedAlphaNumericAndSymbolsReturnsBothWords()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream("p$55word $ec0nd"));

            var expectedResults = new Dictionary<string, int> { { "p$55word", 1 }, { "$ec0nd", 1 } };
            var actualResults = this._treeStrategy.Count(It.IsAny<string>());

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// String containing only symbols returns one word
        /// </summary>
        [Test]
        public void StringContainingOnlySymbolsReturnsOneWord()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream("^£$"));

            var expectedResults = new Dictionary<string, int> { { "^£$", 1 } };
            var actualResults = this._treeStrategy.Count(It.IsAny<string>());

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// String containing two words built of symbols returns both words
        /// </summary>
        [Test]
        public void StringOfTwoWordsBuiltOfOnlySymbolsReturnsBothWords()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream("^£$ $$£^"));

            var expectedResults = new Dictionary<string, int> { { "^£$", 1 }, { "$$£^", 1 } };
            var actualResults = this._treeStrategy.Count(It.IsAny<string>());

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// String of the same word repeated will return one entry with a count of 2
        /// </summary>
        [Test]
        public void StringOfTwoWordsTheSameReturnsOneWordWithCountOfTwo()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream("sentence sentence"));

            var expectedResults = new Dictionary<string, int> { { "sentence", 2 } };
            var actualResults = this._treeStrategy.Count(It.IsAny<string>());

            CollectionAssert.AreEquivalent(expectedResults, actualResults);
            Assert.That(actualResults.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// String of the same word repeated, but separated by a comma, will return one entry with a count of 2
        /// </summary>
        [Test]
        public void StringOfTwoWordsTheSameSeparatedByaComma()
        {
            this.mockFileService.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(ConvertToStream("sentence, sentence"));

            var expectedResults = new Dictionary<string, int> { { "sentence", 2 } };
            var actualResults = this._treeStrategy.Count(It.IsAny<string>());

            Assert.That(actualResults.Count, Is.EqualTo(1));
            CollectionAssert.AreEquivalent(expectedResults, actualResults);
        }

        /// <summary>
        /// Private helper method to convert from a string to a stream
        /// </summary>
        /// <param name="textData"> The string to be converted </param>
        /// <returns> The stream itself </returns>
        private static Stream ConvertToStream(string textData)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(textData);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
