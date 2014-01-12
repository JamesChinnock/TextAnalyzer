
namespace WordCounter
{
    using System;

    using Analyzer;

    /// <summary>
    /// Test program for the TextAnalyzer
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point for the test application
        /// </summary>
        /// <param name="args"> Unused by test app </param>
        public static void Main(string[] args)
        {
            var analyzer = new TextAnalyzer();
            var results = analyzer.AccumulateWords("This is a statement, and so is this.");

            foreach (var result in results)
            {
                Console.WriteLine(result.Key + " - " + result.Value);
            }
        }
    }
}
