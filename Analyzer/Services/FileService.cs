
namespace Analyzer.Services
{
    using System.IO;

    /// <summary>
    /// Represents the ability to perform file reading tasks
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Method to read from a file on the file system and return the stream of data
        /// </summary>
        /// <param name="filePath"> The path to the file to be read </param>
        /// <returns> The stream of data read from the file </returns>
        public Stream Read(string filePath)
        {
            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }
    }
}