
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
    /// base controller for any Entity, and any DbContext.
    /// controller utama harus menurunkan dari base controller ini agar 
    /// entity dapat diakses dari/ke database.
    /// Dan entity harus turunan dari PersistentObject.
    /// Controller turunan harus memiliki Attribute "[ControllerObjectId(no)]".
    /// Dan harus disediakan ICrudDefinition sesuai tipe Entity dan Dto
    /// yang didaftarkan secara singleton.
    /// </summary>
    /// <remarks>
    /// Class ini dibuat sebagai Abstract, agar controller ini tidak bisa
    /// digunakan secara langsung.
    /// </remarks>
    public abstract class WebApiCrudControllerBase<TDbContext, TEntity, TDto>(
                          IDbContextFactory<TDbContext> dbfactory,
                          ICrudDefinition<TEntity, TDto> cruddefinition)
        : ControllerBase
    where TDbContext : DbContext
    where TEntity : PersistentObject
    where TDto : class, IMasterClass
    {
        private readonly IDbContextFactory<TDbContext> _dbfactory = dbfactory;
        private readonly ICrudDefinition<TEntity, TDto> _cruddefinition = cruddefinition;  

        #region CONTROLLER OBJECTID
        private int? _tmpobjectid;
        protected int ControllerObjectId => _tmpobjectid ??=
            GetType()  // ✅ GetType() = tipe controller turunan yang sebenarnya
                .GetCustomAttribute<ControllerObjectIdAttribute>()
                ?.Id
                ?? throw new InvalidOperationException(
                    $"{GetType().Name} harus deklarasi [ControllerObjectId(id)]");
        #endregion

        #region PROTECTED CORE ENGINE
        /// <summary>
        /// delete command, TData adalah Dto yang diturunkan dari IMasterClass
        /// yang berisi Id. 
        /// Jika berhasil maka ResponseData Id akan berisi 0,
        /// dan entity yang berhasil dihapus akan dikirimkan balik.
        /// </summary>
        /// <typeparam name="TData">
        /// Type data dari Dto yang akan dihapus. Dto ini hrus diturunkan dari
        /// IMasterClass yang berisi property Id bertype long
        /// </typeparam>
        /// <param name="copytoDtoAsResult">
        /// Expression untuk memindahkan data dari entity ke 
        /// dto yg menjadi hasil response. 
        /// </param>
        /// <param name="request">Dto yang ingin dihapus</param>
        /// <returns></returns>
        protected virtual async Task<ActionResult<ResponseData<TDto>>>
                        EntityDeleteAsync(RequestData<TDto> request)
        {
            using var db = _dbfactory.CreateDbContext();
            try
            {
                request.ThrowIfContentNullOrMultipleItem();

                var dbentity = db.Set<TEntity>();
                var currententity = 
                    await dbentity.LoadEntityByIdAsync(request.Contents[0].Id);
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

        protected virtual async Task<ActionResult<ResponseData<TDto>>> 
                        EntityUpdateAsync(
                            RequestData<TDto> request,
                            CopyIdStatus copyidstatus = CopyIdStatus.DonotCopy)
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

                _cruddefinition.UpdateEntityFromDto.Invoke(
                    requesteddata, 
                    currententity, 
                    copyidstatus);
                
                dbentity.Update(currententity);
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
            EntityCreateAsync(RequestData<TDto> request)
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

                var entity = _cruddefinition.CopyDtoToEntity(dto);
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

        #region PUBLIC ENDPOINTS
        //==================================================
        // GET LIST
        //==================================================
        [HttpPost("getlist")]
        public virtual async Task<ActionResult<ResponseData<TDto>>>
                        GetList(RequestData<QueryContent>? request = null)
        {
            return await BuildListAsync(request);
        }

        //==================================================
        // GET BY ID
        //==================================================
        [HttpPost("getbyid")]
        public virtual async Task<ActionResult<ResponseData<TDto>>>
            GetById(RequestData<long> request)
        {
            return await BuildByIdAsync(request);
        }

        //==================================================
        // CREATE
        //==================================================
        [HttpPost("createnew")]
        public virtual async Task<ActionResult<ResponseData<TDto>>>
            CreateNew(RequestData<TDto> request)
        {
            return await EntityCreateAsync(request);
        }

        //==================================================
        // UPDATE
        //==================================================
        [HttpPost("update")]
        public virtual async Task<ActionResult<ResponseData<TDto>>> Update(
            RequestData<TDto> request)
        {
            return await EntityUpdateAsync(request);
        }

        //==================================================
        // DELETE (SOFT DELETE)
        //==================================================
        [HttpPost("delete")]
        public virtual async Task<ActionResult<ResponseData<TDto>>> Delete(
            RequestData<TDto> request)
        {
            return await EntityDeleteAsync(request);
        }
        #endregion

    }
}
