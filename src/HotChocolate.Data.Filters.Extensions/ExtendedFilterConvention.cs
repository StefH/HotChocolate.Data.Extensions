using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters;

internal class ExtendedFilterConvention : FilterConvention
{
    private readonly ExtendedFilterOperationHelper _operationHelper;

    public ExtendedFilterConvention(ExtendedFilterOperationHelper operationHelper)
    {
        _operationHelper = operationHelper;
    }

    protected override void Configure(IFilterConventionDescriptor descriptor)
    {
        descriptor.AddDefaults();

        // Operation is required:
        // `Operation with identifier 2000000 has no name defined. Add a name to the filter convention (HotChocolate.Data.Filters.StringOperationFilterInputType)`

        foreach (var operationDetail in _operationHelper.Operations)
        {
            descriptor.Operation(operationDetail.Value.Id).Name(operationDetail.Value.Name);
        }

        descriptor.Configure<StringOperationFilterInputType>(filterInputTypeDescriptor =>
        {
            // Type<StringType> is required:
            // `For the operation containsIgnoreCase of type StringOperationFilterInput was no type specified found.`

            /*
             * Generates:
             * 
             * """
             * string.Contains with InvariantCultureIgnoreCase
             * """
             * containsIgnoreCase: String
             */
            foreach (var operation in _operationHelper.Operations)
            {
                filterInputTypeDescriptor.Operation(operation.Value.Id).Type<StringType>();
            }
        });

        descriptor.Provider(new QueryableFilterProvider(configure => configure
            .AddDefaultFieldHandlers()

            .AddFieldHandler<ExtendedQueryableStringOperationHandler>()
        ));
    }
}