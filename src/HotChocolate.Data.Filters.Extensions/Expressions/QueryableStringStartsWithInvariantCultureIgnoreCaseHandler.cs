using System.Linq.Expressions;
using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters.Expressions
{
    internal class QueryableStringStartsWithInvariantCultureIgnoreCaseHandler : ExtendedQueryableStringOperationHandler
    {
        protected override int Operation { get; }

        public QueryableStringStartsWithInvariantCultureIgnoreCaseHandler(ExtendedFilterOperations operations, InputParser inputParser) : base(inputParser)
        {
            Operation = operations.StringStartsWithIgnoreCase;
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