using System;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Data.Filters.Models;
using HotChocolate.Execution.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class HotChocolateDataRequestBuilderExtensions
    {
        public static IRequestExecutorBuilder AddExtendedFiltering(this IRequestExecutorBuilder builder, Action<ExtendedFilterConfiguration>? configure = null)
        {
            var configuration = new ExtendedFilterConfiguration();
            configure?.Invoke(configuration);

            builder.Services.AddSingleton<ExtendedFilterConfiguration>();
            builder.Services.AddSingleton<ExtendedFilterExpressionHelper>();
            builder.Services.AddSingleton<ExtendedFilterOperationHelper>();

            return builder.AddFiltering<ExtendedFilterConvention>();
        }
    }
}