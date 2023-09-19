using System;
using System.Linq;
using System.Linq.Expressions;
using HotChocolate.Configuration;
using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters.Expressions;

internal class ExtendedQueryableStringOperationHandler : QueryableOperationHandlerBase
{
    private readonly ExtendedFilterOperationHelper _operationHelper;
    private readonly ExtendedFilterExpressionHelper _expressionHelper;

    public ExtendedQueryableStringOperationHandler(
        ExtendedFilterOperationHelper operationHelper,
        ExtendedFilterExpressionHelper builder,
        InputParser inputParser) : base(inputParser)
    {
        _operationHelper = operationHelper;
        _expressionHelper = builder;
    }

    public override bool CanHandle(ITypeCompletionContext context, IFilterInputTypeDefinition typeDefinition, IFilterFieldDefinition fieldDefinition)
    {
        if (context.Type is StringOperationFilterInputType)
        {
            if (fieldDefinition is FilterOperationFieldDefinition filterOperationFieldDefinition)
            {
                return _operationHelper.Operations.Values.Any(fd => fd.Id == filterOperationFieldDefinition.Id);
            }
        }

        return false;
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
            // Creates and returns the operation
            // e.g. ~> y.XXX(str, ...);
            var operation = _operationHelper.Operations.Single(o => o.Value.Id == field.Id).Key;
            return _expressionHelper.Call(operation, property, str);
        }

        // Something went wrong 😱
        throw new InvalidOperationException();
    }
}