
namespace AnalyzerTests.AcceptanceTests.AcceptanceCriteriaTextFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Analyzer;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    /// <summary>
    /// Class containing the criteria for a system level acceptance test. Tests are run using SpecFlow
    /// </summary>
    [Binding]
    public class AcceptanceCriteriaFromFile
    {
        /// <summary>
        /// Private string to contain the file name
        /// </summary>
        private string _file;
        
        /// <summary>
        /// Set a text file to be used in the test
        /// </summary>
        [Given(@"A text file")]
        public void GivenATextFile()
        {
            this._file = @"Bible.txt";
        }

        /// <summary>
        /// Call the analyzers' accumulator method code
        /// </summary>
        [When(@"the program is run against the contents of the file")]
        public void WhenTheProgramIsRun()
        {
            var analyzer = new TextAnalyzer();
            ScenarioContext.Current.Add("Results", analyzer.AccumulateWords(this._file, true));
        }

        /// <summary>
        /// Ensure the correct list of words and their grouped counts are returned
        /// </summary>
        /// <param name="expectedResults"> A table of the expexted results declared in the Feature file </param>
        [Then(@"the correct list of distinct words in the sentence, and the number of times they occurred is returned")]
        public void ThenTheCorrectListOfDistinctWordsInTheSentenceAndTheNumberOfTimesTheyOccurredIsReturned(Table expectedResults)
        {
            var results = ScenarioContext.Current["Results"] as Dictionary<string, int>;
            expectedResults.CompareToSet(results);
        }
    }
}
