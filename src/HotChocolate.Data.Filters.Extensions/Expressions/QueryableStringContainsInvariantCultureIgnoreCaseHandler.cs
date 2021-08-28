using System.Linq.Expressions;
using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters.Expressions
{
    internal class QueryableStringContainsInvariantCultureIgnoreCaseHandler : ExtendedQueryableStringOperationHandler
    {
        protected override int Operation { get; }

        public QueryableStringContainsInvariantCultureIgnoreCaseHandler(ExtendedFilterOperations operations, InputParser inputParser) : base(inputParser)
        {
            Operation = operations.StringContainsIgnoreCase;
        }

        public override Expression HandleOperation(
            QueryableFilterContext context,
            IFilterOperationField field,
            IValueNode value,
            object? parsedValue)
        {
            return CallExpression(context, parsedValue, (property, str) => ExtendedFilterExpressionBuilder.Contains(property, str));
        }
    }
}