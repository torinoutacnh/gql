using GraphQL.Execution;
using GraphQLParser.AST;

public class StrategySelector : IExecutionStrategySelector
{
    private static IExecutionStrategy ParallelExecutionStrategy = new ParallelExecutionStrategy();
    private static IExecutionStrategy SerialExecutionStrategy = new SerialExecutionStrategy();
    private static IExecutionStrategy SubscriptionExecutionStrategy = new SubscriptionExecutionStrategy();

    public IExecutionStrategy Select(GraphQL.Execution.ExecutionContext context)
    {
        return context.Operation.Operation switch
        {
            OperationType.Query => SerialExecutionStrategy,
            OperationType.Mutation => SerialExecutionStrategy,
            OperationType.Subscription => SubscriptionExecutionStrategy,
            _ => throw new InvalidOperationException($"Unexpected OperationType {context.Operation.Operation}"),
        };
    }
}