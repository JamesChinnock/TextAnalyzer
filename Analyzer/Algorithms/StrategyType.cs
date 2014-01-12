
namespace Analyzer.Algorithms
{
    /// <summary>
    /// Enum of possible Strategy types - to remove the 'need' for magic strings
    /// </summary>
    public enum StrategyType
    {
        /// <summary>
        /// Basic default strategy and algorithm
        /// </summary>
        Basic,

        /// <summary>
        /// Much faster strategy to be used with larger files
        /// </summary>
        PrefixTree
    }
}