using System;
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters.Utils;

namespace HotChocolate.Data.Filters.Expressions
{
    internal static class ExtendedFilterExpressionBuilder
    {
        private static readonly Expression ExpressionInvariantCultureIgnoreCase = Expression.Constant(StringComparison.InvariantCultureIgnoreCase);

        private static readonly MethodInfo _contains = MethodFinder.GetPublicMethodBySignature(
            typeof(string),
            nameof(string.Contains),
            typeof(bool),
            null,
            false,
            typeof(string), typeof(StringComparison));

        private static readonly MethodInfo _equals = MethodFinder.GetPublicMethodBySignature(
           typeof(string),
           nameof(string.Equals),
           typeof(bool),
           null,
           false,
           typeof(string), typeof(StringComparison));

        private static readonly MethodInfo _endsWith = MethodFinder.GetPublicMethodBySignature(
            typeof(string),
            nameof(string.EndsWith),
            typeof(bool),
            null,
            false,
            typeof(string), typeof(StringComparison));

        private static readonly MethodInfo _startsWith = MethodFinder.GetPublicMethodBySignature(
            typeof(string),
            nameof(string.StartsWith),
            typeof(bool),
            null,
            false,
            typeof(string), typeof(StringComparison));

        public static Expression Contains(Expression property, string str) =>
            Expression.Call(property, _contains, Expression.Constant(str), ExpressionInvariantCultureIgnoreCase);

        public static Expression Equals(Expression property, string str) =>
            Expression.Call(property, _equals, Expression.Constant(str), ExpressionInvariantCultureIgnoreCase);

        public static Expression EndsWith(Expression property, string str) =>
            Expression.Call(property, _endsWith, Expression.Constant(str), ExpressionInvariantCultureIgnoreCase);

        public static Expression StartsWith(Expression property, string str) =>
            Expression.Call(property, _startsWith, Expression.Constant(str), ExpressionInvariantCultureIgnoreCase);
    }
}
