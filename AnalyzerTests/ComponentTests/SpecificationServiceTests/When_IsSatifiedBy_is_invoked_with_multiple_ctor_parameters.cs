
namespace AnalyzerTests.ComponentTests.SpecificationServiceTests
{
    using Analyzer.Services;
    using Analyzer.Specifications;

    using Machine.Specifications;

    using Moq;

    using It = Machine.Specifications.It;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class When_IsSatifiedBy_is_invoked_with_multiple_ctor_parameters
    {
        /// <summary>
        /// Mock ISpecification to be set up in the Establish delegate and passed as constructor parameter
        /// </summary>
        private static Mock<ISpecification> _mockSpecificationOne;

        /// <summary>
        /// Mock ISpecification to be set up in the Establish delegate and passed as constructor parameter
        /// </summary>
        private static Mock<ISpecification> _mockSpecificationTwo;

        /// <summary>
        /// The SpecificationService instance to be tested
        /// </summary>
        private static SpecificationService _sut;

        /// <summary>
        /// Setup the mocks and the SUT 
        /// </summary>
        private Establish that = () =>
        {
            _mockSpecificationOne = new Mock<ISpecification>();
            _mockSpecificationOne.Setup(s => s.IsSatisfiedBy(Moq.It.IsAny<string>())).Returns(true);
            _mockSpecificationTwo = new Mock<ISpecification>();
            _mockSpecificationTwo.Setup(s => s.IsSatisfiedBy(Moq.It.IsAny<string>())).Returns(true);

            _sut = new SpecificationService(_mockSpecificationOne.Object, _mockSpecificationTwo.Object);
        };


        /// <summary>
        /// Invoke the AccumulateFromFile method on the Accumulator 
        /// </summary>
        private Because of = () => _sut.IsSatisfiedBy(Moq.It.IsAny<string>());


        /// <summary>
        /// Assertion that  RunStrategy is invoked on the ISpecificationService implementation
        /// </summary>
        private It the_IsSatisfiedBy_method_is_invoked_on_the_first_ISpecificationService = () =>
            _mockSpecificationOne.Verify(s => s.IsSatisfiedBy(Moq.It.IsAny<string>()), Times.AtLeastOnce);

        /// <summary>
        /// Assertion that  RunStrategy is invoked on the ISpecificationService implementation
        /// </summary>
        private It the_IsSatisfiedBy_method_is_invoked_on_the_second_ISpecificationService = () =>
            _mockSpecificationTwo.Verify(s => s.IsSatisfiedBy(Moq.It.IsAny<string>()), Times.AtLeastOnce);
    }
}
