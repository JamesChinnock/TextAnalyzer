
namespace Analyzer.Services
{
    using System.Linq;

    using Analyzer.Specifications;

    /// <summary>
    /// Represents the ability to check that sentences/paths satisfy certain criteria
    /// </summary>
    public class SpecificationService : ISpecificationService
    {
        /// <summary>
        /// Private readonly field containing the specifications constructor parameter
        /// </summary>
        private readonly ISpecification[] _specifications; 

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationService"/> class
        /// </summary>
        /// <param name="specifications"> Array of specifications </param>
        public SpecificationService(params ISpecification[] specifications)
        {
            this._specifications = specifications;
        }

        /// <summary>
        /// Method to check that the filepath (or sentence) string meets certain criteris
        /// </summary>
        /// <param name="sentenceorPath"> Path to the file, or the sentence to be checked </param>
        /// <returns> True if the filePath satisfies the specification, else false </returns>
        public bool IsSatisfiedBy(string sentenceorPath)
        {
            return this._specifications.All(specification => specification.IsSatisfiedBy(sentenceorPath));
        }
    }
}