using HotChocolate.Data.Filters.Models;

namespace HotChocolate.Data.Filters
{
    internal class ExtendedFilterOperations
    {
        public const int _stringContainsIgnoreCase = 2000;
        public const int _stringEqualsIgnoreCase = 2001;
        public const int _stringEndWithsIgnoreCase = 2002;
        public const int _stringStartsWithIgnoreCase = 2003;

        public int StringEqualsIgnoreCase { get; }

        public int StringContainsIgnoreCase { get; }

        public int StringEndsWithIgnoreCase { get; }

        public int StringStartsWithIgnoreCase { get; }

        public ExtendedFilterOperations(ExtendedFilterConfiguration configuration)
        {
            StringContainsIgnoreCase = configuration.OverwriteStringContains ? DefaultFilterOperations.Contains : _stringContainsIgnoreCase;
            StringEqualsIgnoreCase = configuration.OverwriteStringEquals ? DefaultFilterOperations.Equals : _stringEqualsIgnoreCase;
            StringEndsWithIgnoreCase = configuration.OverwriteStringEndsWith ? DefaultFilterOperations.EndsWith : _stringEndWithsIgnoreCase;
            StringStartsWithIgnoreCase = configuration.OverwriteStringStartsWith ? DefaultFilterOperations.StartsWith : _stringStartsWithIgnoreCase;
        }
    }
}