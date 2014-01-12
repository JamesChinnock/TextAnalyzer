
namespace Analyzer.Accumulator
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the ability to accumulate word data
    /// </summary>
    public interface IAccumulator
    {
        /// <summary>
        /// Accumulates words and their count from an input string
        /// </summary>
        /// <param name="sentence"> The sentence to be acculumated into word data </param>
        /// <returns> A Dictionary containing worda and their respective numerical occurence </returns>
        Dictionary<string, int> AccumulateFromString(string sentence);

        /// <summary>
        /// Accumulates words and their count from a file represented by the filePath parameter
        /// </summary>
        /// <param name="filePath"> The path of the file from which we will acculumate word data </param>
        /// <returns> A Dictionary containing worda and their respective numerical occurence </returns>
        Dictionary<string, int> AccumulateFromFile(string filePath);
    }
}