
namespace Analyzer.Specifications
{
    using System.IO;

    /// <summary>
    /// Represents the ability to check that a file path meets a certain specification. In this case simply whether it exists,
    /// but this could be extended to check naming conventions etc.
    /// </summary>
    public class FileSpecification : ISpecification
    {
        /// <summary>
        /// Method to determine whether a file path exists
        /// </summary>
        /// <param name="filePath"> The path to the file to be opened </param>
        /// <returns> True if file exists, else false </returns>
        public bool IsSatisfiedBy(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}