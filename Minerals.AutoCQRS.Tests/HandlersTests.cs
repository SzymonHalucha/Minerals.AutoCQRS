using HandlersTestEnvironment;

namespace Minerals.AutoCQRS.Tests
{
    [TestClass]
    public class HandlersTests
    {
        // Command Handlers
        [TestMethod]
        public async Task SingleCommandHandler_OneComand_ShouldOutputString()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoCQRSHandlers();
            IServiceProvider provider = services.BuildServiceProvider();

            var dispatcher = provider.GetRequiredService<ICommandDispatcher>();
            string result = await dispatcher.Dispatch<TestCommand1, string>(new TestCommand1(), CancellationToken.None);

            result.Should().Be("TestCommand1Handler1 - 1");
        }

        [TestMethod]
        public async Task SingleCommandHandler_OneComand_ShouldOutputInt()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoCQRSHandlers();
            IServiceProvider provider = services.BuildServiceProvider();

            var dispatcher = provider.GetRequiredService<ICommandDispatcher>();
            int result = await dispatcher.Dispatch<TestCommand1, int>(new TestCommand1(), CancellationToken.None);

            result.Should().Be(1);
        }

        [TestMethod]
        public async Task MultipleCommandHandler_OneCommand_ShouldOutputLastHandlerString()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoCQRSHandlers();
            IServiceProvider provider = services.BuildServiceProvider();

            var dispatcher = provider.GetRequiredService<ICommandDispatcher>();
            string result = await dispatcher.Dispatch<TestCommand2, string>(new TestCommand2(), CancellationToken.None);

            result.Should().Be("TestCommand2Handler2 - 1");
        }

        // Query Handlers
        [TestMethod]
        public async Task SingleQueryHandler_OneQueryShouldOutputString()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoCQRSHandlers();
            IServiceProvider provider = services.BuildServiceProvider();

            var dispatcher = provider.GetRequiredService<IQueryDispatcher>();
            string result = await dispatcher.Dispatch<TestQuery1, string>(new TestQuery1(), CancellationToken.None);

            result.Should().Be("TestQuery1Handler1 - 1");
        }

        [TestMethod]
        public async Task SingleQueryHandler_OneQueryShouldOutputInt()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoCQRSHandlers();
            IServiceProvider provider = services.BuildServiceProvider();

            var dispatcher = provider.GetRequiredService<IQueryDispatcher>();
            int result = await dispatcher.Dispatch<TestQuery1, int>(new TestQuery1(), CancellationToken.None);

            result.Should().Be(1);
        }

        [TestMethod]
        public async Task MultipleQueryHandler_OneQuery_ShouldOutputLastHandlerString()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoCQRSHandlers();
            IServiceProvider provider = services.BuildServiceProvider();

            var dispatcher = provider.GetRequiredService<IQueryDispatcher>();
            string result = await dispatcher.Dispatch<TestQuery2, string>(new TestQuery2(), CancellationToken.None);

            result.Should().Be("TestQuery2Handler2 - 1");
        }
    }
}