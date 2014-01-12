
namespace AnalyzerTests.ComponentTests.AccumulatorTests
{
    using Analyzer.Accumulator;
    using Analyzer.Algorithms;
    using Analyzer.Services;

    using Machine.Specifications;

    using Moq;

    using It = Machine.Specifications.It;

    /// <summary>
    /// Initializes a new instance of the <see cref="When_AccumulateFromString_is_invoked"/> class.
    /// </summary>
    [Subject(typeof(Accumulator))]
    public class When_AccumulateFromString_is_invoked
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

                _sut = new Accumulator(_mockstrategyService.Object, _mockSpecificationService.Object);
            };

        /// <summary>
        /// Invoke the AccumulateFromString method on the Accumulator 
        /// </summary>
        private Because of = () => _sut.AccumulateFromString(Moq.It.IsAny<string>());

        /// <summary>
        /// Assertion that  RunStrategy is invoked on the IStrategyService implementation
        /// </summary>
        private It the_RunStrategy_method_is_invoked_on_the_IStrategyService = () => 
            _mockstrategyService.Verify(s => s.RunStrategy(Moq.It.IsAny<StrategyType>(), Moq.It.IsAny<string>()), Times.AtLeastOnce);
    }
}
