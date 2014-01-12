
namespace Analyzer.AutofacModules
{
    using Analyzer.Accumulator;
    using Analyzer.Services;
    using Analyzer.Specifications;
    
    using Autofac;

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Accumulator>().As<IAccumulator>();
            builder.RegisterType<FileSpecification>().As<ISpecification>();
            builder.RegisterType<FileService>().As<IFileService>();

            builder.Register(c => new SpecificationService(c.Resolve<ISpecification>()))
                   .As<ISpecificationService>();
            
            builder.RegisterType<StrategyService>().As<IStrategyService>();
          
            base.Load(builder);
        }
    }
}
