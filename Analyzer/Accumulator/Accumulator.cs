
namespace Analyzer.Accumulator
{
    using System;
    using System.Collections.Generic;

    using Analyzer.Algorithms;
    using Analyzer.Services;

    /// <summary>
    /// Represents the ability to accumulate word data
    /// </summary>
    public class Accumulator : IAccumulator
    {
        /// <summary>
        /// Private readonly field containing the strategyService constructor parameter
        /// </summary>
        private readonly IStrategyService _strategyService;

        /// <summary>
        /// Private readonly field containing the specificationService constructor parameter
        /// </summary>
        private readonly ISpecificationService _specificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Accumulator"/> class
        /// </summary>
        /// <param name="strategyService"> A service from which the algorithmic strategies are retrieved </param>
        /// <param name="specificationService"> A service of specifications that the sentence (or path) must satisfy </param>
        public Accumulator(IStrategyService strategyService, ISpecificationService specificationService)
        {
            if (strategyService == null)
            {
                throw new ArgumentNullException("strategyService");
            }

            if (specificationService == null)
            {
                throw new ArgumentNullException("specificationService");
            }

            this._strategyService = strategyService;
            this._specificationService = specificationService;
        }

        /// <summary>
        /// Accumulates words and their count from an input string
        /// </summary>
        /// <param name="sentence"> The sentence to be acculumated into word data </param>
        /// <returns> A Dictionary containing worda and their respective numerical occurence </returns>
        public Dictionary<string, int> AccumulateFromString(string sentence)
        {
            return this._strategyService.RunStrategy(StrategyType.Basic, sentence);
        }

        /// <summary>
        /// Accumulates words and their count from a file represented by the filePath parameter
        /// </summary>
        /// <param name="filePath"> The path of the file from which we will acculumate word data </param>
        /// <returns> A Dictionary containing worda and their respective numerical occurence </returns>
        public Dictionary<string, int> AccumulateFromFile(string filePath)
        {
            if (!this._specificationService.IsSatisfiedBy(filePath))
            {
                throw new Exception();
            }

            return this._strategyService.RunStrategy(StrategyType.PrefixTree, filePath);
        }
    }
}