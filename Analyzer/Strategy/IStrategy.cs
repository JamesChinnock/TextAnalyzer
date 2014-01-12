
namespace Analyzer.Strategy
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the ability to group instances of words and the number of times they occur in a string
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// Method to group words and count occurrences of them
        /// </summary>
        /// <param name="sentence"> The text to grouped and counted </param>
        /// <returns> Dictionary of words and corresponding count  </returns>
        Dictionary<string, int> Count(string sentence);
    }
}