using PipelinesTestEnvironment;

namespace Minerals.AutoCQRS.Tests
{
    [TestClass]
    public class PipelinesTests
    {
        // Command Pipeline Handlers
        [TestMethod]
        public async Task MultiCommandHandler_OnePipeline_ShouldOutputString()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoCQRSPipelines();
            IServiceProvider provider = services.BuildServiceProvider();

            var dispatcher = provider.GetRequiredService<ICommandPipelineDispatcher>();
            var results = await dispatcher.Dispatch<TestCommand1, string>(new TestCommand1());

            results.Should().BeEquivalentTo(["TestCommand1Handler1 - 1", "TestCommand1Handler2 - 1"]);
        }

        [TestMethod]
        public async Task MultiCommandHandler_OnePipeline_ShouldNotCallSingleHandler()
        {
            IServiceCollection services = new ServiceCollection();
            ICommandDispatcher commandMockDispatcher = Substitute.For<ICommandDispatcher>();
            services.AddSingleton(commandMockDispatcher);
            services.AddAutoCQRSPipelines();
            IServiceProvider provider = services.BuildServiceProvider();

            var handlerDispatcher = provider.GetRequiredService<ICommandDispatcher>();
            var pipelineDispatcher = provider.GetRequiredService<ICommandPipelineDispatcher>();
            var results = await pipelineDispatcher.Dispatch<TestCommand1, string>(new TestCommand1());

            await commandMockDispatcher.DidNotReceiveWithAnyArgs().Dispatch<TestCommand1, string>(Arg.Any<TestCommand1>());
            results.Should().BeEquivalentTo(["TestCommand1Handler1 - 1", "TestCommand1Handler2 - 1"]);
        }

        // Query Pipeline Handlers
        [TestMethod]
        public async Task MultiQueryHandler_OnePipeline_ShouldOutputString()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoCQRSPipelines();
            IServiceProvider provider = services.BuildServiceProvider();

            var dispatcher = provider.GetRequiredService<IQueryPipelineDispatcher>();
            var results = await dispatcher.Dispatch<TestQuery1, string>(new TestQuery1());

            results.Should().BeEquivalentTo(["TestQuery1Handler1 - 1", "TestQuery1Handler2 - 1"]);
        }

        [TestMethod]
        public async Task MultiQueryHandler_OnePipeline_ShouldNotCallSingleHandler()
        {
            IServiceCollection services = new ServiceCollection();
            IQueryDispatcher queryMockDispatcher = Substitute.For<IQueryDispatcher>();
            services.AddSingleton(queryMockDispatcher);
            services.AddAutoCQRSPipelines();
            IServiceProvider provider = services.BuildServiceProvider();

            var handlerDispatcher = provider.GetRequiredService<IQueryDispatcher>();
            var pipelineDispatcher = provider.GetRequiredService<IQueryPipelineDispatcher>();
            var results = await pipelineDispatcher.Dispatch<TestQuery1, string>(new TestQuery1());

            await queryMockDispatcher.DidNotReceiveWithAnyArgs().Dispatch<TestQuery1, string>(Arg.Any<TestQuery1>());
            results.Should().BeEquivalentTo(["TestQuery1Handler1 - 1", "TestQuery1Handler2 - 1"]);
        }
    }
}