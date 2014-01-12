
namespace Analyzer.Strategy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Analyzer.Algorithms;
    using Analyzer.Services;

    /// <summary>
    /// Represents the MapReduce strategy to group and count words in a sentence. 
    /// </summary>
    public class PrefixTreeStrategy : IStrategy
    {
        /// <summary>
        /// Private readonly <see cref="IFileService"/> passed as constructor parameter
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefixTreeStrategy"/> class
        /// </summary>
        /// <param name="fileService"> The fileservice to be used to read from a file on the dile system </param>
        public PrefixTreeStrategy(IFileService fileService)
        {
            this._fileService = fileService;
        }

        /// <summary>
        /// Method to group words and count occurrences of them
        /// </summary>
        /// <param name="filePath"> The text to grouped and counted </param>
        /// <returns> Dictionary of words and corresponding count  </returns>
        public Dictionary<string, int> Count(string filePath)
        {
            var streamfile = this._fileService.Read(filePath);
            var algo = new PrefixTreeAlgorithm();

            var task = Task.Factory.StartNew(() => algo.Run(streamfile));
            return task.Result;
        }
    }
}