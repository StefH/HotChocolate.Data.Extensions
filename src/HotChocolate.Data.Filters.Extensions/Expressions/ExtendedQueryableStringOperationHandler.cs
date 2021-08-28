using System;
using System.Linq.Expressions;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters.Expressions
{
    internal abstract class ExtendedQueryableStringOperationHandler : QueryableStringOperationHandler
    {
        protected ExtendedQueryableStringOperationHandler(InputParser inputParser) : base(inputParser)
        {
        }

        protected Expression CallExpression(QueryableFilterContext context, object? parsedValue, Func<Expression, string, Expression> action)
        {
            // We get the instance of the context. This is the expression path to the property
            // e.g. ~> y.Street
            Expression property = context.GetInstance();

            // The parsed value is what was specified in the query
            // e.g. ~> eq: "221B Baker Street"
            if (parsedValue is string str)
            {
                // Creates and returns the operation
                // e.g. ~> y.XXX(str, ...);
                return action(property, str);
            }

            // Something went wrong 😱
            throw new InvalidOperationException();
        }
    }
}