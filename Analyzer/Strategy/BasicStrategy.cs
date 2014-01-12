
namespace Analyzer.Strategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a basic strategy to group and count words in a sentence. To be used for relatively short sentences
    /// </summary>
    public class BasicStrategy : IStrategy
    {
        /// <summary>
        /// Method to group words and count occurrences of them
        /// </summary>
        /// <param name="sentence"> The text to grouped and counted </param>
        /// <returns> Dictionary of words and corresponding count  </returns>
        public Dictionary<string, int> Count(string sentence)
        {
            return RemovePunctuation(sentence)
                .Split(new[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(s => s.ToLower())
                .Select(NewWordCount)
                .ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);
        }

        /// <summary>
        /// Private method to remove punctuation marks from the string
        /// </summary>
        /// <param name="sentence"> The string from which to remove the punctuation </param>
        /// <returns> A new string without Punctuation marks </returns>
        private static string RemovePunctuation(string sentence)
        {
            return new string(sentence.Where(c => !char.IsPunctuation(c)).ToArray());
        }
        
        /// <summary>
        /// Private method to be passed as linqs Select parameter. Lambda was too cumbersom
        /// </summary>
        /// <param name="wordCount"> The grouping result of the GroupBy method </param>
        /// <returns> A new KeyValuePair of count values </returns>
        private static KeyValuePair<string, int> NewWordCount(IGrouping<string, string> wordCount)
        {
            return new KeyValuePair<string, int>(wordCount.Key, wordCount.Count());
        }
    }
}