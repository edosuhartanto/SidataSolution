
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.DataContext.Services;
using Sidata.Abstractions.Interfaces;
using Sidata.Abstractions.Queryable.Extensions;
using Sidata.Abstractions.Queryable.Models;
using System.Linq.Expressions;

namespace Sidata.Abstractions.DataContext.Extensions
{
    /// <summary>
    /// kumpulan modul utk mengatur Queryable type
    /// </summary>
    public static class QueryableExtensions
    {

        /// <summary>
        /// menghitung total record
        /// </summary>
        public static async Task<long> DCountAsync<TEntity>(
            this IQueryable<TEntity> dbentity,
            QueryContent querycontent)
        where TEntity : class
        {
            return await dbentity.AsNoTracking()
                                 .ApplyFilters(querycontent.Filters)
                                 .CountAsync();
        }

        /// <summary>
        /// modul to check if entity is double.
        /// double criteria is push by caller.
        /// </summary>
        public static async Task ThrowIfDuplicateEntity<TEntity, TData>(
                this IQueryable<TEntity> dbentity,
                TData request,
                Func<TData, Expression<Func<TEntity, bool>>> duplicatechecker
            )
        where TEntity : class
        where TData : class
        {
            var duplicateExpression = duplicatechecker(request);
            bool exists = await dbentity.AnyAsync(duplicateExpression);
            if (exists)
            {
                var (PropertyName, Value) =
                    ExpressionExtractor.ExtractMemberInfo(duplicateExpression);
                throw new ArgumentException(
                    $"Entity with {PropertyName}='{Value}' already exists",
                    nameof(request));
            }
        }

        /// <summary>        
        /// Load Entity using Id, and Directly return as Dto
        /// default for tracking for this mode (entity => dto) always notracking
        /// to return TEntity ... send 'x => x' to selectproperties
        /// or used the second overload
        /// </summary>
        public static async Task<TData?> LoadEntityByIdAsync<TEntity, TData>(
            this IQueryable<TEntity> queryentity,
            Expression<Func<TEntity, TData>> selectproperties,
            long id,
            bool allowtracking = false)
        where TEntity : class, IMasterClass
        where TData : class
        {
            if (allowtracking) queryentity = queryentity.AsNoTracking();
            // have selector for only some properties you want to?
            // default you select it all and return TEntity
            return await queryentity.Where(x => x.Id == id)
                                    .Select(selectproperties)
                                    .FirstOrDefaultAsync();
            //        ?? throw new ArgumentException(
            //            $"{typeof(TEntity).Name} Id={id} Not Found", nameof(id));
            //return x;
        }

        /// <summary>
        /// second overload loading an entity based by its Id
        /// this will return TEntity ... and default is always tracking by EF 
        /// for this object
        /// </summary>
        public static async Task<TEntity?> LoadEntityByIdAsync<TEntity>(
            this IQueryable<TEntity> queryentity,
            long id,
            bool allowtracking = true)
        where TEntity: class, IMasterClass
        {
            return await LoadEntityByIdAsync(
                            queryentity, x => x, 
                            id, allowtracking);
        }

        /// <summary>
        /// modul utk menambahkan LINQ Paging Mode 
        /// jika DataContent meminta adanya Page
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryentity"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> TryAddPagingMode<TEntity>(
                this IQueryable<TEntity> queryentity,
                int PageNumber,
                int PageSize)
        where TEntity : class
        {
            if (PageNumber != 0 && PageSize != 0)
                return queryentity
                        .Skip((PageNumber - 1) * PageSize)
                        .Take(PageSize);
            else
                return queryentity;
        }
    }
}
