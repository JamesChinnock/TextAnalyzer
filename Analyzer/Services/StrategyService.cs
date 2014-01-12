
namespace Analyzer.Services
{
    using System.Collections.Generic;

    using Analyzer.Algorithms;
    using Analyzer.Strategy;

    /// <summary>
    /// Represents the ability to select a strategy to employ when accumulating word data
    /// </summary>
    public class StrategyService : IStrategyService
    {
        /// <summary>
        /// Private readonly field <see cref="IFileService"/> to hold the constructor fileService parameter
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyService"/> class
        /// </summary>
        /// <param name="fileService"> The file sevice we will use to read from the file </param>
        public StrategyService(IFileService fileService)
        {
            this._fileService = fileService;
        }

        /// <summary>
        /// Method to run the strategy specified in the strategyType parameter against the string (or path)
        /// in the sentence parameter
        /// </summary>
        /// <param name="strategyType"> The strategy to be employed </param>
        /// <param name="sentence"> The string (or path) from which accumulated data will be retrieved </param>
        /// <returns> Dictionary of words and their respective counts </returns>
        public Dictionary<string, int> RunStrategy(StrategyType strategyType, string sentence)
        {
            IStrategy strategy;
            
            switch (strategyType)
            {
                case StrategyType.PrefixTree:
                    strategy = new PrefixTreeStrategy(this._fileService);
                    break;
                default:
                    strategy = new BasicStrategy();
                    break;
            }

            return strategy.Count(sentence);
        }
    }
}