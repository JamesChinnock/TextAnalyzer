
namespace AnalyzerTests.AcceptanceTests.AcceptanceCriteria
{
    using System.Collections.Generic;

    using Analyzer;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    /// <summary>
    /// Class containing the criteria for a system level acceptance test. Tests are run using SpecFlow
    /// </summary>
    [Binding]
    public class AcceptanceCriteria
    {
        /// <summary>
        /// Private string to contain the tests sentence
        /// </summary>
        private string _sentence = string.Empty;

        /// <summary>
        /// Create a sentence to use as test data
        /// </summary>
        [Given(@"A sentence")]
        public void GivenASentence()
        {
            this._sentence = "This is a statement, and so is this.";
        }

        /// <summary>
        /// Call the analyzers' accumulator method code
        /// </summary>
        [When(@"the program is run")]
        public void WhenTheProgramIsRun()
        {
            var analyzer = new TextAnalyzer();
            ScenarioContext.Current.Add("Results", analyzer.AccumulateWords(this._sentence));
        }

        /// <summary>
        /// Ensure the correct list of words and their grouped counts are returned
        /// </summary>
        /// <param name="expectedResults"> A table of the expexted results declared in the Feature file </param>
        [Then(@"I am returned a distinct list of words in the sentence and the number of times they have occurred")]
        public void ThenIAmReturnedADistinctListOfWordsInTheSentenceAndTheNumberOfTimesTheyHaveOccurred(Table expectedResults)
        {
            var results = ScenarioContext.Current["Results"] as Dictionary<string, int>;
            expectedResults.CompareToSet(results);
        }
    }
}
