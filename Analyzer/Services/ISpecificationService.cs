
namespace Analyzer.Services
{
    /// <summary>
    /// Represents the ability to check that sentences/paths satisfy certain criteria
    /// </summary>
    public interface ISpecificationService
    {
        /// <summary>
        /// Method to check that the filepath (or sentence) string meets certain criteris
        /// </summary>
        /// <param name="filePath"> Path to the file </param>
        /// <returns> True if the filePath satisfies the specification, else false </returns>
        bool IsSatisfiedBy(string filePath);
    }
}