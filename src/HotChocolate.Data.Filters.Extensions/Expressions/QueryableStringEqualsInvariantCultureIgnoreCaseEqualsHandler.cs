using System.Linq.Expressions;
using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters.Expressions
{
    internal class QueryableStringEqualsInvariantCultureIgnoreCaseEqualsHandler : ExtendedQueryableStringOperationHandler
    {
        protected override int Operation { get; }

        public QueryableStringEqualsInvariantCultureIgnoreCaseEqualsHandler(ExtendedFilterOperations operations, InputParser inputParser) : base(inputParser)
        {
            Operation = operations.StringEqualsIgnoreCase;
        }

        public override Expression HandleOperation(
            QueryableFilterContext context,
            IFilterOperationField field,
            IValueNode value,
            object? parsedValue)
        {
            return CallExpression(context, parsedValue, (property, str) => ExtendedFilterExpressionBuilder.Equals(property, str));
        }
    }
}