// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.Queryable.Enums;
using Sidata.Abstractions.Queryable.Exceptions;
using Sidata.Abstractions.Queryable.Interfaces;
using Sidata.Abstractions.Queryable.Models;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;

namespace Sidata.Abstractions.Queryable.Extensions
{
    public static class QueryExtensions
    {
        /// <summary>
        /// build LINQ from QueryContent data type.
        /// Filter atau Sort atau keduanya atau tidak sama sekali.
        /// default adalah membangun kedua expression itu.
        /// </summary>
        public static IQueryable<T> ApplyQuery<T>(
            this IQueryable<T> query,
            QueryContent parameterquerycontent,
            FilterSortSelector selector = FilterSortSelector.All)
        {
            if ((selector & FilterSortSelector.Filter) > 0)
                query = query.ApplyFilters(parameterquerycontent.Filters);
            if ((selector & FilterSortSelector.Sort) > 0)
                query = query.ApplySorting(parameterquerycontent.Sorts);

            return query;
        }

        #region FILTERS

        /// <summary>
        /// membangun LINQ Expression&lt;T&gt; untuk operasi WHERE 
        /// </summary>
        public static IQueryable<T> ApplyFilters<T>(
            this IQueryable<T> query,
            IEnumerable<FilterContent>? filters)
        {
            if (filters == null)
                return query;

            var filterList = filters.ToList();

            if (filterList.Count == 0)
                return query;

            var predicate =
                BuildPredicate<T>(filterList);

            return query.Where(predicate);
        }

        /// <summary>
        /// ekstensi utama ApplyFilter, membangun Expression utk WHERE
        /// </summary>
        private static Expression<Func<T, bool>>
            BuildPredicate<T>(
                IEnumerable<FilterContent> filters)
        {
            var parameter =
                Expression.Parameter(typeof(T), "x");

            Expression? body = null;

            foreach (var filter in filters)
            {
                filter.ThrowIfNotAValidProperty<T>();

                // start build filter LINQ Expression
                var expression =
                    BuildFilterExpression(
                        parameter,
                        filter);

                body = body == null
                    ? expression
                    : Expression.AndAlso(
                        body,
                        expression);
            }

            body ??= Expression.Constant(true);

            return Expression.Lambda<Func<T, bool>>
            (
                body,
                parameter
            );
        }

        /// <summary>
        /// membangun satu operasi filter expression.
        /// jika operator tidak dikenal, maka dicoba dikirim ke 
        /// FilterHandler external.
        /// </summary>
        private static Expression BuildFilterExpression(
            ParameterExpression parameter,
            FilterContent filter)
        {
            var property =
                Expression.Property(
                    parameter,
                    filter.PropertyName);

            var propertyType =
                Nullable.GetUnderlyingType(
                    property.Type)
                ?? property.Type;

            if (filter.Operator == FilterOperator.IsNull)
            {
                return Expression.Equal(
                    property,
                    Expression.Constant(
                        null,
                        property.Type));
            }

            if (filter.Operator == FilterOperator.IsNotNull)
            {
                return Expression.NotEqual(
                    property,
                    Expression.Constant(
                        null,
                        property.Type));
            }

            var value =
                ConvertValue(
                    filter.Value,
                    propertyType);

            var constant =
                Expression.Constant(
                    value,
                    propertyType);

            Expression valueExpression =
                property.Type == propertyType
                    ? constant
                    : Expression.Convert(
                        constant,
                        property.Type);

            return filter.Operator switch
            {
                FilterOperator.Equal =>
                    Expression.Equal(
                        property,
                        valueExpression),

                FilterOperator.NotEqual =>
                    Expression.NotEqual(
                        property,
                        valueExpression),

                FilterOperator.GreaterThan =>
                    Expression.GreaterThan(
                        property,
                        valueExpression),

                FilterOperator.GreaterThanOrEqual =>
                    Expression.GreaterThanOrEqual(
                        property,
                        valueExpression),

                FilterOperator.LessThan =>
                    Expression.LessThan(
                        property,
                        valueExpression),

                FilterOperator.LessThanOrEqual =>
                    Expression.LessThanOrEqual(
                        property,
                        valueExpression),

                FilterOperator.Contains =>
                    BuildStringCall(
                        property,
                        nameof(string.Contains),
                        filter.Value),

                FilterOperator.StartsWith =>
                    BuildStringCall(
                        property,
                        nameof(string.StartsWith),
                        filter.Value),

                FilterOperator.EndsWith =>
                    BuildStringCall(
                        property,
                        nameof(string.EndsWith),
                        filter.Value),

                _ => BuildFromListFilterHandler(property, filter)
            };
        }

        /// <summary>
        /// membangun filter expression, jika menggunakan FilterHandler
        /// </summary>
        private static Expression BuildFromListFilterHandler(
            Expression property,
            FilterContent filter)
        {
            foreach (var handler in _filterhandlers)
            {
                var result = handler(property, filter);
                // not null, means it is recognize as valid operator
                if (result != null) return result;
            }
            throw new EntityPropertyBuilderException(
                  $"Operator {filter.Operator} tidak didukung");
        }

        /// <summary>
        /// khusus utk membangun Expression utk type string
        /// </summary>
        private static MethodCallExpression BuildStringCall(
            MemberExpression property,
            string methodName,
            string? value)
        {
            return Expression.Call(
                property,
                typeof(string)
                    .GetMethod(
                        methodName,
                        [typeof(string)])!,
                Expression.Constant(value));
        }
        #endregion

        #region SORTING
        /// <summary>
        /// membuat LINQ Expression&lt;T&gt; utk Sorting
        /// </summary>
        public static IQueryable<T> ApplySorting<T>(
            this IQueryable<T> query,
            IEnumerable<SortContent>? sorts)
        {
            if (sorts == null)
                return query;

            var sortList = sorts.ToList();

            if (sortList.Count == 0)
                return query;

            bool first = true;

            foreach (var sort in sortList)
            {
                sort.ThrowIfNotAValidProperty<T>();

                query =
                    ApplySingleSort(
                        query,
                        sort,
                        first);

                first = false;
            }

            return query;
        }

        /// <summary>
        /// membuat sebuah expression Sort
        /// </summary>
        private static IQueryable<T> ApplySingleSort<T>(
            IQueryable<T> query,
            SortContent sort,
            bool first)
        {
            var parameter =
                Expression.Parameter(
                    typeof(T),
                    "x");

            var property =
                Expression.Property(
                    parameter,
                    sort.PropertyName);

            var lambda =
                Expression.Lambda(
                    property,
                    parameter);

            string methodName;

            if (first)
            {
                methodName =
                    sort.Direction ==
                    SortDirection.Ascending
                        ? nameof(System.Linq.Queryable.OrderBy)
                        : nameof(System.Linq.Queryable.OrderByDescending);
            }
            else
            {
                methodName =
                    sort.Direction ==
                    SortDirection.Ascending
                        ? nameof(System.Linq.Queryable.ThenBy)
                        : nameof(System.Linq.Queryable.ThenByDescending);
            }

            var method =
                typeof(System.Linq.Queryable)
                    .GetMethods()
                    .First(x =>
                        x.Name == methodName
                        &&
                        x.GetParameters().Length == 2);

            var genericMethod =
                method.MakeGenericMethod(
                    typeof(T),
                    property.Type);

            return (IQueryable<T>)
                genericMethod.Invoke(
                    null,
                    [ query, lambda ])!;
        }

        #endregion

        #region HELPERS
        /// <summary>
        /// fungsi utk check apakah property valid utk diakses QueryContent builder.
        /// </summary>
        public static void ThrowIfNotAValidProperty<T>(this IPropertyOperator content)
        {
            // check property name available in T
            _ = typeof(T).GetProperty(
                    content.PropertyName,
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.IgnoreCase)
                ?? throw new EntityPropertyBuilderException(
                    $"Property '{content.PropertyName}' tidak ditemukan pada entity '{typeof(T).Name}'.");
        }

        /// <summary>
        /// fungsi konversi value dari string ke type yang dituju
        /// </summary>
        private static object? ConvertValue(
            string? value,
            Type targetType)
        {
            if (value == null)
                return null;

            if (targetType == typeof(Guid))
                return Guid.Parse(value);

            if (targetType.IsEnum)
                return Enum.Parse(
                    targetType,
                    value,
                    true);

            if (targetType == typeof(DateTime))
                return DateTime.Parse(value);

            if (targetType == typeof(DateOnly))
                return DateOnly.Parse(value);

            if (targetType == typeof(TimeOnly))
                return TimeOnly.Parse(value);

            if (targetType == typeof(bool))
                return bool.Parse(value);

            return Convert.ChangeType(
                value,
                targetType);
        }
        #endregion

        #region EXTERNAL FILTER HANDLER
        private static readonly List<FilterHandler> _filterhandlers = [];

        /// <summary>
        /// sebuah type delegate, sbg kontrak koneksi dengan filter handler external
        /// </summary>
        public delegate Expression? FilterHandler(
                                        Expression parameter,
                                        FilterContent filter);

        /// <summary>
        /// daftar semua Filter Handler Eksternal yang telah didaftarkan.
        /// dalam bentuk ImmutableList
        /// </summary>
        public static ImmutableArray<FilterHandler> FilterHandlers => [.. _filterhandlers];

        /// <summary>
        /// register filter handler eksternal
        /// </summary>
        public static void RegisterNewFilterHandler(FilterHandler newfilterhandler)
        {
            _filterhandlers.Add(newfilterhandler);
        }
        #endregion

    }
}
