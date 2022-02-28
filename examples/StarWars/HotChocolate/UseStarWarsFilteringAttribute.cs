using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace StarWars.HotChocolate
{
    /// <summary>
    /// Registers the middleware and adds the arguments for filtering
    /// </summary>
    public sealed class UseStarWarsFilteringAttribute : ObjectFieldDescriptorAttribute
    {
        private static readonly MethodInfo _generic = typeof(FilterObjectFieldDescriptorExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(
                m => m.Name.Equals(
                         nameof(FilterObjectFieldDescriptorExtensions.UseFiltering),
                         StringComparison.Ordinal) &&
                     m.GetGenericArguments().Length == 1 &&
                     m.GetParameters().Length == 2 &&
                     m.GetParameters()[0].ParameterType == typeof(IObjectFieldDescriptor));

        public UseStarWarsFilteringAttribute(Type? filterType = null, [CallerLineNumber] int order = 0)
        {
            Type = filterType;
            // Order = order;
        }

        /// <summary>
        /// Gets or sets the filter type which specifies the filter object structure.
        /// </summary>
        /// <value>The filter type</value>
        public Type? Type { get; set; }

        /// <summary>
        /// Sets the scope for the convention
        /// </summary>
        /// <value>The name of the scope</value>
        public string? Scope { get; set; }

        /// <inheritdoc />
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            if (Type is null)
            {
                descriptor.UseFiltering(Scope);
            }
            else
            {
                var gt = _generic.MakeGenericMethod(Type);
                gt.Invoke(null, new object?[] { descriptor, Scope });
            }
        }
    }
}