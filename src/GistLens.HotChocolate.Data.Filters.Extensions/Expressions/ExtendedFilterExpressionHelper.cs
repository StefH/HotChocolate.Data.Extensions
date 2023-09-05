using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters.Utils;
using FO = HotChocolate.Data.Filters.Enums.ExtendedFilterOperation;

namespace HotChocolate.Data.Filters.Expressions
{
    /// <summary>
    /// I've to use the string.ToUpper() because SQL Server and SQLite do not support string.XXX(string, StringComparison)
    /// - SQL Server https://docs.microsoft.com/en-us/ef/core/providers/sql-server/functions
    /// - SQLite https://docs.microsoft.com/en-us/ef/core/providers/sqlite/functions
    /// </summary>
    internal class ExtendedFilterExpressionHelper
    {
        private static readonly MethodInfo _toUpper = MethodFinder.GetPublicMethodBySignature(
            typeof(string),
            nameof(string.ToUpper),
            typeof(string),
            null,
            false);

        private static readonly MethodInfo _contains = MethodFinder.GetPublicMethodBySignature(
            typeof(string),
            nameof(string.Contains),
            typeof(bool),
            null,
            false,
            typeof(string));

        private static readonly MethodInfo _equals = MethodFinder.GetPublicMethodBySignature(
           typeof(string),
           nameof(string.Equals),
           typeof(bool),
           null,
           false,
           typeof(string));

        private static readonly MethodInfo _endsWith = MethodFinder.GetPublicMethodBySignature(
            typeof(string),
            nameof(string.EndsWith),
            typeof(bool),
            null,
            false,
            typeof(string));

        private static readonly MethodInfo _startsWith = MethodFinder.GetPublicMethodBySignature(
            typeof(string),
            nameof(string.StartsWith),
            typeof(bool),
            null,
            false,
            typeof(string));

        private readonly IDictionary<FO, Func<Expression, string, Expression>> Expressions = new Dictionary<FO, Func<Expression, string, Expression>>();

        public ExtendedFilterExpressionHelper()
        {
            Expressions.Add(FO.StringContainsIgnoreCase, (property, str) => Expression.Call(property, _contains, Expression.Constant(str)));
            Expressions.Add(FO.StringNotContainsIgnoreCase, (property, str) => Expression.Not(Expressions[FO.StringContainsIgnoreCase](property, str)));

            Expressions.Add(FO.StringEqualsIgnoreCase, (property, str) => Expression.Call(property, _equals, Expression.Constant(str)));
            Expressions.Add(FO.StringNotEqualsIgnoreCase, (property, str) => Expression.Not(Expressions[FO.StringEqualsIgnoreCase](property, str)));

            Expressions.Add(FO.StringEndsWithIgnoreCase, (property, str) => Expression.Call(property, _endsWith, Expression.Constant(str)));
            Expressions.Add(FO.StringNotEndsWithIgnoreCase, (property, str) => Expression.Not(Expressions[FO.StringEndsWithIgnoreCase](property, str)));

            Expressions.Add(FO.StringStartsWithIgnoreCase, (property, str) => Expression.Call(property, _startsWith, Expression.Constant(str)));
            Expressions.Add(FO.StringNotStartsWithIgnoreCase, (property, str) => Expression.Not(Expressions[FO.StringStartsWithIgnoreCase](property, str)));
        }

        public Expression Call(FO operation, Expression propertyExpression, string str)
        {
            var propertyExpressionToUpper = Expression.Call(propertyExpression, _toUpper);
            return Expressions[operation](propertyExpressionToUpper, str.ToUpper());
        }
    }
}