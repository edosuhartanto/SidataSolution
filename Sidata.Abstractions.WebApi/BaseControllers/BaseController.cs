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
using Sidata.Abstractions.WebApi.ResponseRequest.Extensions;
using Sidata.Abstractions.WebApi.ResponseRequest.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace Sidata.Abstractions.WebApi.BaseControllers
{
    /// <summary>
    /// base controller for any Entity, and any DbContext.
    /// controller utama harus menurunkan dari base controller ini agar 
    /// entity dapat diakses dari/ke database.
    /// Dan entity harus turunan dari PersistentObject.
    /// Controller turunan harus memiliki Attribute "[ControllerObjectId(no)]".
    /// Dan harus menurunkan fungsi2 berikut:
    /// Expression&lt;Func&lt;TDto,TEntity&gt;&gt; SelectEntityIntoDto()
    /// Func&lt;TDto,TEntity&gt; CopyDtoToEntity()
    /// Func&lt;TDto,TEntity&gt; CopyEntityToDto() 
    /// Action&lt;TDto,TEntity&gt; UpdateRequestDtoToEntity() 
    /// </summary>
    /// <remarks>
    /// Class ini dibuat sebagai Abstract, agar controller ini tidak bisa
    /// digunakan secara langsung.
    /// </remarks>
    public abstract class WebApiCrudControllerBase<TDbContext, TEntity, TDto>(
                          IDbContextFactory<TDbContext> dbfactory)
        : ControllerBase
    where TDbContext : DbContext
    where TEntity : PersistentObject
    where TDto : class, IMasterClass
    {
        private readonly IDbContextFactory<TDbContext> _dbfactory = dbfactory;

        #region CONTROLLER OBJECTID
        private int? _tmpobjectid;
        protected int ControllerObjectId => _tmpobjectid ??=
            GetType()  // ✅ GetType() = tipe controller turunan yang sebenarnya
                .GetCustomAttribute<ControllerObjectIdAttribute>()
                ?.Id
                ?? throw new InvalidOperationException(
                    $"{GetType().Name} harus deklarasi [ControllerObjectId(id)]");
        #endregion

        #region ABSTRACT DELEGATE FUNCTIONS
        protected abstract Func<TDto, Expression<Func<TEntity, bool>>> 
            InsertDuplicateChecker { get; }

        protected abstract Func<TDto, Expression<Func<TEntity, bool>>>
            UpdateDuplicateChecker { get; }

        /// <summary>
        /// controller turunan harus membuat proses update Entity
        /// dgn data yang didapat dari Dto.
        /// </summary>
        /// <remarks>
        /// jika Id dan Kode (yg unik per record) tidak ingin dioverwrite
        /// set CopyIdStatus ke DonotCopy.
        /// </remarks>
        protected abstract Action<TDto, TEntity, CopyIdStatus> 
                    UpdateEntityFromDto { get; }

        /// <summary>
        /// controller turunan harus menurunkan cara mengkopi Dto ke Entity
        /// </summary>
        protected abstract Func<TDto, TEntity> CopyDtoToEntity { get; }

        /// <summary>
        /// controller turunan harus menurunkan cara mengkopi Dto ke Entity
        /// </summary>
        /// <remarks>
        /// fungsi ini khusus utk digunakan dalam Linq Expression
        /// jadi meskipun mungkin isinya sama seperti yang digunakan dalam 
        /// CopyEntityToDto, 
        /// jangan referensikan ke sini, namun harus dibuatlah sekali lagi.
        /// agar Linq dapat menjadikannya perintah SQL. Kalo tidak, maka
        /// proses perubahan ini akan terjadi di memory, bukan di server SQL.
        /// </remarks>
        protected abstract Expression<Func<TEntity, TDto>> 
                    LinqExpressionEntityToDto { get; }
        /// <summary>
        /// fungsi yang harus diturunkan utk mengkopi entity to dto
        /// </summary>
        protected abstract Func<TEntity, TDto> CopyEntityToDto { get; }
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
                    await dbentity.LoadEntityByIdAsync(request.Data[0].Id);
                dbentity.Remove(currententity); 
                // softdelete is happened inside TDbContext
                await db.SaveChangesAsync();

                return Ok(
                    CopyEntityToDto(currententity)
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
                var requesteddata = request.Data[0];
                await dbentity.ThrowIfDuplicateEntity(
                    requesteddata,
                    UpdateDuplicateChecker);

                var currententity = 
                    await dbentity.LoadEntityByIdAsync(requesteddata.Id);

                UpdateEntityFromDto.Invoke(
                    requesteddata, 
                    currententity, 
                    copyidstatus);
                
                dbentity.Add(currententity);
                await db.SaveChangesAsync();

                return Ok(CopyEntityToDto(currententity)
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
                var dto = request.Data[0];
                await dbentity
                        .ThrowIfDuplicateEntity(
                            dto, 
                            InsertDuplicateChecker);

                var entity = CopyDtoToEntity(dto);
                dbentity.Add(entity);
                await db.SaveChangesAsync();

                return Ok(CopyEntityToDto(entity)
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
                if (request.Data.Count > 1)
                {
                    var response =
                         await dbentity.LoadEntityByIdAsync(
                             LinqExpressionEntityToDto, request.Data);
                    return Ok(response.BuildOkResponseData());
                }
                else
                {
                    var response =
                       await dbentity.LoadEntityByIdAsync(
                           LinqExpressionEntityToDto, request.Data[0]);
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
                                  .Select(LinqExpressionEntityToDto);
                var querycontent = new QueryContent();
                var pagenumber = 0;
                var pagesize = 0;

                if (request is not null)
                {
                    // querycontent must be available and only have single data
                    request.ThrowIfContentNullOrMultipleItem();

                    // build query based on request in QueryContent
                    querycontent = request.Data[0];
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
