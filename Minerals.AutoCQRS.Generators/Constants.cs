namespace Minerals.AutoCQRS.Generators
{
    public static class Constants
    {
        public const string IServiceCollectionExtensions = nameof(IServiceCollectionExtensions);
        public const string ICommandPipeline = nameof(ICommandPipeline);
        public const string ICommandHandler = nameof(ICommandHandler);
        public const string IQueryPipeline = nameof(IQueryPipeline);
        public const string IQueryHandler = nameof(IQueryHandler);
        public const string AddAutoCQRSHandlers = nameof(AddAutoCQRSHandlers);
        public const string AddAutoCQRSPipelines = nameof(AddAutoCQRSPipelines);

        public static readonly string[] HandlerDispatchers =
        [
            "global::Minerals.AutoCQRS.ICommandDispatcher, global::Minerals.AutoCQRS.CommandDispatcher",
            "global::Minerals.AutoCQRS.IQueryDispatcher, global::Minerals.AutoCQRS.QueryDispatcher"
        ];

        public static readonly string[] PipelineDispatchers =
        [
            "global::Minerals.AutoCQRS.ICommandPipelineDispatcher, global::Minerals.AutoCQRS.CommandPipelineDispatcher",
            "global::Minerals.AutoCQRS.IQueryPipelineDispatcher, global::Minerals.AutoCQRS.QueryPipelineDispatcher"
        ];
    }
}