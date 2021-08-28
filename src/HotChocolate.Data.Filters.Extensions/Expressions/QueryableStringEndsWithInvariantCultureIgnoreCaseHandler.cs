using System.Linq.Expressions;
using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters.Expressions
{
    internal class QueryableStringEndsWithInvariantCultureIgnoreCaseHandler : ExtendedQueryableStringOperationHandler
    {
        protected override int Operation { get; }

        public QueryableStringEndsWithInvariantCultureIgnoreCaseHandler(ExtendedFilterOperations operations, InputParser inputParser) : base(inputParser)
        {
            Operation = operations.StringEndsWithIgnoreCase;
        }

        public override Expression HandleOperation(
            QueryableFilterContext context,
            IFilterOperationField field,
            IValueNode value,
            object? parsedValue)
        {
            return CallExpression(context, parsedValue, (property, str) => ExtendedFilterExpressionBuilder.EndsWith(property, str));
        }
    }
}