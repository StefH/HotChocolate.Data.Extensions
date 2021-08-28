using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters.Utils;
using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters.Expressions
{
    internal class QueryableStringContainsInvariantCultureIgnoreCaseHandler : QueryableStringOperationHandler
    {
        // public bool Contains(string value, StringComparison comparisonType)
        private static readonly MethodInfo _contains = MethodFinder.GetPublicMethodsBySignature(
            typeof(string),
            nameof(string.Contains),
            typeof(bool),
            null,
            false,
            typeof(string), typeof(StringComparison)).Single();

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
            // We get the instance of the context. This is the expression path to the property
            // e.g. ~> y.Street
            Expression property = context.GetInstance();

            // The parsed value is what was specified in the query
            // e.g. ~> eq: "221B Baker Street"
            if (parsedValue is string str)
            {
                // Creates and returnes the operation
                // e.g. ~> y.Contains(str, StringComparison.InvariantCultureIgnoreCase);
                return Expression.Call(property, _contains, Expression.Constant(str), Expression.Constant(StringComparison.InvariantCultureIgnoreCase));
            }

            // Something went wrong 😱
            throw new InvalidOperationException();
        }
    }
}