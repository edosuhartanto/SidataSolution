
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.BaseClasses;
using Sidata.Abstractions.DataContext.Extensions;
using Sidata.Abstractions.Interfaces;
using Sidata.Abstractions.Queryable.Exceptions;
using Sidata.Abstractions.Queryable.Extensions;
using Sidata.Abstractions.Queryable.Models;
using Sidata.Abstractions.WebApi.Attributes;
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.Extensions;
using Sidata.Abstractions.WebApi.Interfaces;
using Sidata.Abstractions.WebApi.ResponseRequest.Extensions;
using Sidata.Abstractions.WebApi.ResponseRequest.Models;
using System.Reflection;

namespace Sidata.Abstractions.WebApi.BaseControllers
{
    /// <summary>
    /// base controller for any Entity, and any DbContext.<br/>
    /// controller utama harus menurunkan dari base controller ini agar 
    /// entity dapat diakses dari/ke database.
    /// Dan entity harus turunan dari PersistentObject.
    /// Controller turunan harus memiliki Attribute "[ControllerObjectId(no)]".<br/>
    /// Dan harus disediakan ICrudDefinition sesuai tipe Entity dan Dto
    /// yang didaftarkan secara singleton. (CrudDefinition adalah class yang melakukan
    /// mapping dari Entity ke Dto dan sebaliknya, sekaligus mencatat cara mendeteksi 
    /// duplikasi data untuk insert dan update)
    /// </summary>
    /// <remarks>
    /// Catatan:
    /// <list type="number">
    /// <item>
    /// Class ini dibuat sebagai Abstract, agar controller ini tidak bisa
    /// digunakan secara langsung.
    /// </item>
    /// <item>
    /// Jika membutuhkan implementasi endpoint utk CRUD standard, 
    /// telah disediakan class <seealso cref="WebApiCrudControllerBase{TDbContext, TEntity, TDto}"/>
    /// </item>
    /// </list>
    /// </remarks>
    public abstract class BaseController<TDbContext, TEntity, TDto>(
                          IDbContextFactory<TDbContext> dbfactory,
                          ICrudDefinition<TEntity, TDto> cruddefinition)
        : ControllerBase
    where TDbContext : DbContext
    where TEntity : PersistentObject
    where TDto : class, IMasterClass
    {
        protected readonly IDbContextFactory<TDbContext> _dbfactory = dbfactory;
        protected readonly ICrudDefinition<TEntity, TDto> _cruddefinition = cruddefinition;  

        #region CONTROLLER OBJECTID
        private int? _tmpobjectid;
        protected int ControllerObjectId => _tmpobjectid ??=
            GetType()  // ✅ GetType() = tipe controller turunan yang sebenarnya
                .GetCustomAttribute<ControllerObjectIdAttribute>()
                ?.Id
                ?? throw new ControllerObjectIdNotDeclaredException(
                    $"{GetType().Name} harus deklarasi [ControllerObjectId(id)]");
        #endregion

        #region PROTECTED CORE ENGINE
        /// <summary>
        /// delete command, Request data bertipe long, yang adalah Id dari entity yang ingin dihapus.
        /// Jika berhasil delete maka ResponseData akan berisi TDto 
        /// sbg entity yang berhasil dihapus.
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<ActionResult<ResponseData<TDto>>>
                        EntityDeleteAsync(
                            RequestData<long> request,
                            Action<TEntity>? beforedelete = null)
        {
            using var db = _dbfactory.CreateDbContext();
            try
            {
                request.ThrowIfContentNullOrMultipleItem();

                var dbentity = db.Set<TEntity>();
                var id = request.Contents[0];
                var currententity = await dbentity.LoadEntityByIdAsync(id);

                beforedelete?.Invoke(currententity);

                dbentity.Remove(currententity); 
                // softdelete is happened inside TDbContext
                await db.SaveChangesAsync();

                return Ok(
                    _cruddefinition.CopyEntityToDto(currententity)
                        .BuildOkResponseData()
                );
            }
            catch (Exception ex)
            {
                return Ok(
                    ex.BuildErrorResponseData(
                        ControllerObjectIdExtension.Builder(
                            ControllerObjectId, 
                            BaseStatementId.Delete)));
            }
        }

        /// <summary>
        /// fungsi mengupdate entity, Request data berisi TDto sbg data yang ingin diupdate,
        /// copyidstatus enenrtukan apakah unique key dari entity akan diedit juga atau tidak,
        /// namun krn unique key biasanya tidak boleh diubah, maka default diset DonotCopy<br/>
        /// beforeupdate adalah action yang dijalankan sebelum entity diupdate oleh dto<br/> 
        /// transformentitybeforesafe adalah action yg dijalankan sebelum entity disimpan ke database
        /// </summary>
        protected virtual async Task<ActionResult<ResponseData<TDto>>> 
                        EntityUpdateAsync(
                            RequestData<TDto> request,
                            CopyIdStatus copyidstatus = CopyIdStatus.DonotCopy,
                            Action<TDto, TEntity>? beforeupdate = null,
                            Action<TEntity>? transformentitybeforesafe = null)
        {
            using var db = _dbfactory.CreateDbContext();
            try
            {
                request.ThrowIfContentNullOrMultipleItem();

                var dbentity = db.Set<TEntity>();
                var requesteddata = request.Contents[0];
                await dbentity.ThrowIfDuplicateEntity(
                    requesteddata,
                    _cruddefinition.UpdateDuplicateChecker);

                var currententity = 
                    await dbentity.LoadEntityByIdAsync(requesteddata.Id);

                beforeupdate?.Invoke(requesteddata, currententity);

                _cruddefinition.UpdateEntityFromDto.Invoke(
                    requesteddata, 
                    currententity, 
                    copyidstatus);

                transformentitybeforesafe?.Invoke(currententity);

                dbentity.Add(currententity);
                await db.SaveChangesAsync();

                return Ok(_cruddefinition.CopyEntityToDto(currententity)
                            .BuildOkResponseData());
            }
            catch (Exception ex)
            {
                return Ok(
                    ex.BuildErrorResponseData(
                        ControllerObjectIdExtension.Builder(ControllerObjectId, BaseStatementId.Update)));
            }
        }

        protected virtual async Task<ActionResult<ResponseData<TDto>>>
            EntityCreateAsync(RequestData<TDto> request, 
                              Action<TDto>? beforecreate = null,
                              Action<TEntity>? transformentitybeforesafe = null)
        {
            using var db = _dbfactory.CreateDbContext();

            try
            {
                request.ThrowIfContentNullOrMultipleItem();

                var dbentity = db.Set<TEntity>();
                var dto = request.Contents[0];
                await dbentity
                        .ThrowIfDuplicateEntity(
                            dto, 
                            _cruddefinition.InsertDuplicateChecker);

                beforecreate?.Invoke(dto);

                var entity = _cruddefinition.CopyDtoToEntity(dto);
                
                transformentitybeforesafe?.Invoke(entity);

                dbentity.Add(entity);
                await db.SaveChangesAsync();

                return Ok(_cruddefinition.CopyEntityToDto(entity)
                            .BuildOkResponseData());
            }
            catch (Exception ex)
            {
                return Ok(
                    ex.BuildErrorResponseData(
                        ControllerObjectIdExtension.Builder(ControllerObjectId, BaseStatementId.Create)));
            }
        }

        protected virtual async Task<ActionResult<ResponseData<TDto>>> 
                        BuildByIdAsync(RequestData<long> request)
        {
            using var _db = _dbfactory.CreateDbContext();

            try
            {
                request.ThrowIfContentNull();
                var dbentity = _db.Set<TEntity>();
                if (request.Contents.Count > 1)
                {
                    var response =
                         await dbentity.LoadEntityByIdAsync(
                             _cruddefinition.LinqExpressionEntityToDto, request.Contents);
                    return Ok(response.BuildOkResponseData());
                }
                else
                {
                    var response =
                       await dbentity.LoadEntityByIdAsync(
                           _cruddefinition.LinqExpressionEntityToDto, request.Contents[0]);
                    return Ok(response.BuildOkResponseData());
                }
            }
            catch (Exception ex)
            {
                return Ok(
                    ex.BuildErrorResponseData(
                            ControllerObjectIdExtension.Builder(
                                ControllerObjectId, 
                                BaseStatementId.GetById)));
            }
        }

        protected virtual async Task<ActionResult<ResponseData<TDto>>>
                        BuildListAsync(
                            RequestData<QueryContent>? request = null)
        {
            using var _db = _dbfactory.CreateDbContext();

            try
            {
                var entity = _db.Set<TEntity>();
                var query = entity.AsNoTracking()
                                  .Select(_cruddefinition.LinqExpressionEntityToDto);
                var querycontent = new QueryContent();
                var pagenumber = 0;
                var pagesize = 0;

                if (request is not null)
                {
                    // querycontent must be available and only have single data
                    request.ThrowIfContentNullOrMultipleItem();

                    // build query based on request in QueryContent
                    querycontent = request.Contents[0];
                    query = query.ApplyQuery(querycontent);

                    // paging mode
                    // try? ... if pagenumber and pagesize = 0,
                    // it will cancel adding page mode
                    pagenumber = querycontent.PageNumber;
                    pagesize = querycontent.PageSize;
                    query = query.TryAddPagingMode(pagenumber, pagesize);
                }
                var response = await query.ToListAsync();
                var totalsize = await entity.DCountAsync(querycontent);
                return Ok(response.BuildOkResponseData(totalsize,
                                                        pagenumber,
                                                        pagesize));
            }
            catch (Exception ex)
            {
                return Ok(
                    ex.BuildErrorResponseData(
                        ControllerObjectIdExtension.Builder(
                            ControllerObjectId, BaseStatementId.GetList)));
            }
        }
        #endregion

    }
}
