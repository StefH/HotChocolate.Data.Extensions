using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Data.Filters.Models;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters
{
    internal class ExtendedFilterConvention : FilterConvention
    {
        private readonly ExtendedFilterConfiguration _configuration;
        private readonly ExtendedFilterOperations _operations;

        public ExtendedFilterConvention(ExtendedFilterConfiguration configuration, ExtendedFilterOperations operations)
        {
            _configuration = configuration;
            _operations = operations;
        }

        protected override void Configure(IFilterConventionDescriptor descriptor)
        {
            descriptor.AddDefaults();

            // Operation is required:
            // `Operation with identifier 2000 has no name defined. Add a name to the filter convention (HotChocolate.Data.Filters.StringOperationFilterInputType)`

            if (!_configuration.OverwriteStringContains)
            {
                descriptor.Operation(_operations.StringContainsIgnoreCase).Name("containsIgnoreCase").Description("string.Contains with InvariantCultureIgnoreCase");
            }

            if (!_configuration.OverwriteStringEquals)
            {
                descriptor.Operation(_operations.StringEqualsIgnoreCase).Name("eqIgnoreCase").Description("string.Equals with InvariantCultureIgnoreCase");
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
                filterInputTypeDescriptor.Operation(_operations.StringContainsIgnoreCase).Type<StringType>();

                /*
                 * Generates:
                 * 
                 * """
                 * string.Equals with InvariantCultureIgnoreCase
                 * """
                 * eqIgnoreCase: String
                 */
                filterInputTypeDescriptor.Operation(_operations.StringEqualsIgnoreCase).Type<StringType>();
            });

            descriptor.Provider(new QueryableFilterProvider(configure => configure
                .AddDefaultFieldHandlers()

                .AddFieldHandler<QueryableStringContainsInvariantCultureIgnoreCaseHandler>()
                .AddFieldHandler<QueryableStringEqualsInvariantCultureIgnoreCaseEqualsHandler>()
            ));
        }
    }
}