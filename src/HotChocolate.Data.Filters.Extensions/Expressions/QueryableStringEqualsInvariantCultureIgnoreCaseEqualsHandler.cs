using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters.Utils;
using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters.Expressions
{
    internal class QueryableStringEqualsInvariantCultureIgnoreCaseEqualsHandler : QueryableStringOperationHandler
    {
        // public bool Equals(string value, StringComparison comparisonType)
        private static readonly MethodInfo _equals = MethodFinder.GetPublicMethodsBySignature(
            typeof(string),
            nameof(string.Equals),
            typeof(bool),
            null,
            false,
            typeof(string), typeof(StringComparison)).Single();

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
            Expression property = context.GetInstance();
            if (parsedValue is string str)
            {
                return Expression.Call(property, _equals, Expression.Constant(str), Expression.Constant(StringComparison.InvariantCultureIgnoreCase));
            }

            throw new InvalidOperationException();
        }
    }
}