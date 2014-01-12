
namespace Analyzer.Services
{
    using System.Collections.Generic;

    using Analyzer.Algorithms;
    
    /// <summary>
    /// Represents the ability to select a strategy to employ when accumulating word data
    /// </summary>
    public interface IStrategyService
    {
        /// <summary>
        /// Method to run the strategy specified in the strategyType parameter against the string (or path)
        /// in the sentence parameter
        /// </summary>
        /// <param name="strategyType"> The strategy to be employed </param>
        /// <param name="sentence"> The string (or path) from which accumulated data will be retrieved </param>
        /// <returns> Dictionary of words and their respective counts </returns>
        Dictionary<string, int> RunStrategy(StrategyType strategyType, string sentence);
    }
}