
namespace Analyzer.Specifications
{
    public interface ISpecification
    {
        bool IsSatisfiedBy(string filePath);
    }
}
