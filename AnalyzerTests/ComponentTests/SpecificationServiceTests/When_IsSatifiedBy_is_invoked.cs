
namespace AnalyzerTests.ComponentTests.SpecificationServiceTests
{
    using Analyzer.Services;
    using Analyzer.Specifications;

    using Machine.Specifications;

    using Moq;


    using It = Machine.Specifications.It;

    /// <summary>
    /// Initializes a new instance of the <see cref="When_IsSatifiedBy_is_invoked"/> class.
    /// </summary>
    [Subject(typeof(SpecificationService))]
    public class When_IsSatifiedBy_is_invoked
    {
        /// <summary>
        /// Mock ISpecification to be set up in the Establish delegate and passed as constructor parameter
        /// </summary>
        private static Mock<ISpecification> _mockSpecification;

        /// <summary>
        /// The SpecificationService instance to be tested
        /// </summary>
        private static SpecificationService _sut;

        /// <summary>
        /// Setup the mocks and the SUT 
        /// </summary>
        private Establish that = () =>
        {
            _mockSpecification = new Mock<ISpecification>();

            _sut = new SpecificationService(_mockSpecification.Object);
        };


        /// <summary>
        /// Invoke the AccumulateFromFile method on the Accumulator 
        /// </summary>
        private Because of = () => _sut.IsSatisfiedBy(Moq.It.IsAny<string>());


        /// <summary>
        /// Assertion that  RunStrategy is invoked on the ISpecificationService implementation
        /// </summary>
        private It the_IsSatisfiedBy_method_is_invoked_on_the_ISpecificationService = () =>
            _mockSpecification.Verify(s => s.IsSatisfiedBy(Moq.It.IsAny<string>()), Times.AtLeastOnce);
    }
}
