
namespace AnalyzerTests.ComponentTests.AccumulatorTests
{
    using Analyzer.Accumulator;
    using Analyzer.Algorithms;
    using Analyzer.Services;
    
    using Machine.Specifications;

    using Moq;

    using It = Machine.Specifications.It;

    /// <summary>
    /// Initializes a new instance of the <see cref="When_AccumulateFromFile_is_invoked"/> class.
    /// </summary>
    [Subject(typeof(Accumulator))]
    public class When_AccumulateFromFile_is_invoked
    {
        /// <summary>
        /// Mock IStrategyService to be set up in the Establish delegate and passed as constructor parameter
        /// </summary>
        private static Mock<IStrategyService> _mockstrategyService;

        /// <summary>
        /// Mock ISpecificationService to be set up in the Establish delegate and passed as constructor parameter
        /// </summary>
        private static Mock<ISpecificationService> _mockSpecificationService;

        /// <summary>
        /// The Accumulator instance to be tested
        /// </summary>
        private static Accumulator _sut;

        /// <summary>
        /// Setup the mocks and the SUT 
        /// </summary>
        private Establish that = () =>
        {
            _mockstrategyService = new Mock<IStrategyService>();
            _mockSpecificationService = new Mock<ISpecificationService>();
            _mockSpecificationService.Setup(s => s.IsSatisfiedBy(Moq.It.IsAny<string>())).Returns(true);

            _sut = new Accumulator(_mockstrategyService.Object, _mockSpecificationService.Object);
        };

        /// <summary>
        /// Invoke the AccumulateFromFile method on the Accumulator 
        /// </summary>
        private Because of = () => _sut.AccumulateFromFile(Moq.It.IsAny<string>());

        /// <summary>
        /// Assertion that  RunStrategy is invoked on the ISpecificationService implementation
        /// </summary>
        private It the_IsSatisfiedBy_method_is_invoked_on_the_ISpecificationService = () =>
            _mockSpecificationService.Verify(s => s.IsSatisfiedBy(Moq.It.IsAny<string>()), Times.AtLeastOnce);

        /// <summary>
        /// Assertion that  RunStrategy is invoked on the IStrategyService implementation
        /// </summary>
        private It the_RunStrategy_method_is_invoked_on_the_IStrategyService = () =>
            _mockstrategyService.Verify(s => s.RunStrategy(Moq.It.IsAny<StrategyType>(), Moq.It.IsAny<string>()), Times.AtLeastOnce);
    }
}
