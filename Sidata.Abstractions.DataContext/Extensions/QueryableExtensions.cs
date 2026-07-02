
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.DataContext.Services;
using Sidata.Abstractions.Exceptions;
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
        /// the criteria of duplication is defined by caller through duplicatechecker expression
        /// </summary>
        /// <exception cref="EntityDuplicateException"></exception>
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
                // get the property name and its value from expression
                var (PropertyName, Value) =
                    ExpressionExtractor.ExtractMemberInfo(duplicateExpression);
                // and throw exception with that information
                throw new EntityDuplicateException(
                    $"Entity dengan {PropertyName}={Value} sudah ada dalam tabel {typeof(TEntity).Name}");
            }
        }

        /// <summary>        
        /// Load Entity using Id, and Directly return as Dto
        /// default for tracking for this mode (entity => dto) always notracking
        /// to return TEntity ... send 'x => x' to selectproperties
        /// or used the second overload
        /// </summary>
        public static async Task<TData> LoadEntityByIdAsync<TEntity, TData>(
            this IQueryable<TEntity> queryentity,
            Expression<Func<TEntity, TData>> selectproperties,
            long id,
            bool allowtracking = false)
        where TEntity : class, IMasterClass
        where TData : class
        {
            if (!allowtracking) queryentity = queryentity.AsNoTracking();
            // have selector for only some properties you want to?
            // default you select it all and return TEntity
            return await queryentity.Where(x => x.Id == id)
                                    .Select(selectproperties)
                                    .FirstOrDefaultAsync()
                    ?? throw new EntityNotFoundException(
                        $"Entity {typeof(TEntity).Name} dengan Id={id} tidak ditemukan");
        }

        public static async Task<IEnumerable<TData>> LoadEntityByIdAsync<TEntity, TData>(
            this IQueryable<TEntity> queryentity,
            Expression<Func<TEntity, TData>> selectproperties,
            IEnumerable<long> ids,
            bool allowtracking = false)
        where TEntity : class, IMasterClass
        where TData : class
        {
            if (!allowtracking) queryentity = queryentity.AsNoTracking();
            // have selector for only some properties you want to?
            // default you select it all and return TEntity
            var r = await queryentity.Where(x => ids.Contains(x.Id))
                                    .Select(selectproperties)
                                    .ToListAsync();
            if ((r is null) || (r.Count == 0))
                throw new EntityNotFoundException(
                        $"Daftar Id berikut:[{string.Join(",", ids)}] tidak ditemukan dalam tabel {typeof(TEntity).Name}");
            return r;
        }

        /// <summary>
        /// second overload loading an entity based by its Id
        /// this will return TEntity ... and default is always tracking by EF 
        /// for this object
        /// </summary>
        /// <exception cref="NullReferenceException">
        /// jika Id tidak ditemukan, akan dihasilkan exception ini
        /// </exception>
        public static async Task<TEntity> LoadEntityByIdAsync<TEntity>(
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
