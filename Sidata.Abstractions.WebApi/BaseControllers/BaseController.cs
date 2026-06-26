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
    public class WebApiBaseController<TDbContext, TEntity, TDto>(
                    IDbContextFactory<TDbContext> dbfactory)
        : ControllerBase
    where TDbContext : DbContext
    where TEntity : PersistentObject
    where TDto : IMasterClass
    {
        #region ControllerObjectId LazyInit
        private int? _tmpobjectid;
        protected int ControllerObjectId => _tmpobjectid ??=
            GetType()  // ✅ GetType() = tipe controller turunan yang sebenarnya
                .GetCustomAttribute<ControllerObjectIdAttribute>()
                ?.Id
                ?? throw new InvalidOperationException(
                    $"{GetType().Name} harus deklarasi [ControllerObjectId(id)]"); 
        #endregion

        private readonly IDbContextFactory<TDbContext> _dbfactory = dbfactory;
    
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
        protected async Task<ActionResult<ResponseData<TDto>>>
                        EntityDeleteAsync(
                            Func<TEntity, TDto> copyEntityToDtoAsResult,                            
                            RequestData<TDto> request)
        {
            using var db = _dbfactory.CreateDbContext();
            try
            {
                request.ThrowIfContentNullOrMultipleItem();

                var dbentity = db.Set<TEntity>();
                var requestdto = request.Data[0];
                var id = requestdto.Id;

                var currententity =
                    await dbentity.LoadEntityByIdAsync(id);
                dbentity.Remove(currententity); 
                // softdelete is happened inside TDbContext
                await db.SaveChangesAsync();

                requestdto = copyEntityToDtoAsResult(currententity);
                return Ok(requestdto.BuildOkResponseData());
            }
            catch (Exception ex)
            {
                return Ok(
                    ex.BuildErrorResponseData(
                        ControllerObjectIdExtension.Builder(ControllerObjectId, BaseStatementId.Delete)));
            }
        }

        protected async Task<ActionResult<ResponseData<TData>>> 
                        EntityUpdateAsync<TData>(
                            Func<TData, 
                                 Expression<Func<TEntity, bool>>> duplicatechecker,
                            Action<TData, TEntity> updateRequestDtoToEntity,
                            Func<TEntity, TData> copytoDtoAsResult,
                            RequestData<TData> request)
        where TData : class, IMasterClass
        {
            using var db = _dbfactory.CreateDbContext();
            try
            {
                request.ThrowIfContentNullOrMultipleItem();

                var dbentity = db.Set<TEntity>();
                var requesteddata = request.Data[0];
                await dbentity.ThrowIfDuplicateEntity(requesteddata, duplicatechecker);

                var currententity = 
                    await dbentity.LoadEntityByIdAsync(requesteddata.Id);                

                updateRequestDtoToEntity(requesteddata, currententity);
                dbentity.Add(currententity);
                await db.SaveChangesAsync();

                var resultDto = copytoDtoAsResult(currententity);
                return Ok(resultDto.BuildOkResponseData());
            }
            catch (Exception ex)
            {
                return Ok(
                    ex.BuildErrorResponseData(
                        ControllerObjectIdExtension.Builder(ControllerObjectId, BaseStatementId.Update)));
            }
        }

        protected async Task<ActionResult<ResponseData<TData>>>
            EntityCreateAsync<TData>(
                Func<TData,
                     Expression<Func<TEntity, bool>>> doublechecker,
                Func<TData, TEntity> buildEntityFromDto,
                Func<TEntity, TData> copytoDtoAsResult,
                RequestData<TData> request)
        where TData : class
        {
            using var db = _dbfactory.CreateDbContext();

            try
            {
                request.ThrowIfContentNullOrMultipleItem();

                var dbentity = db.Set<TEntity>();
                var dto = request.Data[0];
                await dbentity
                        .ThrowIfDuplicateEntity(dto, 
                                                doublechecker);

                var entity = buildEntityFromDto(dto);
                dbentity.Add(entity);
                await db.SaveChangesAsync();

                var resultDto = copytoDtoAsResult(entity);
                return Ok(resultDto.BuildOkResponseData());
            }
            catch (Exception ex)
            {
                return Ok(
                    ex.BuildErrorResponseData(
                        ControllerObjectIdExtension.Builder(ControllerObjectId, BaseStatementId.Create)));
            }
        }

        protected async Task<ActionResult<ResponseData<TData>>> 
                        BuildByIdAsync<TData>(
                            Expression<Func<TEntity, TData>> propertyselector,
                            RequestData<long> request)
        where TData : class
        {
            using var _db = _dbfactory.CreateDbContext();

            try
            {
                request.ThrowIfContentNullOrMultipleItem();

                var id = request.Data[0];
                var dbentity = _db.Set<TEntity>();
                var response = 
                    await dbentity.LoadEntityByIdAsync(propertyselector, id);
                
                return Ok(response.BuildOkResponseData());
            }
            catch (Exception ex)
            {
                return Ok(
                    ex.BuildErrorResponseData(
                            ControllerObjectIdExtension.Builder(ControllerObjectId, BaseStatementId.GetById)));
            }
        }

        protected async Task<ActionResult<ResponseData<TData>>>
                        BuildListAsync<TData>(
                            Expression<Func<TEntity, TData>> propertyselector,
                            RequestData<QueryContent>? request = null)
        where TData : class
        {
            using var _db = _dbfactory.CreateDbContext();

            try
            {
                var entity = _db.Set<TEntity>();
                var query = entity.AsNoTracking()
                                  .Select(propertyselector);
                var querycontent = new QueryContent();
                var pagenumber = 0;
                var pagesize = 0;

                if (request is not null)
                {
                    // querycontent must be available and only have single data
                    request.ThrowIfContentNullOrMultipleItem();

                    // build query based on request in QueryContent
                    querycontent = request.Data[0];
                    query.ApplyQuery(querycontent);

                    // paging mode
                    // try? ... if pagenumber and pagesize = 0,
                    // it will cancel adding page mode
                    pagenumber = request.PageNumber;
                    pagesize = request.PageSize;
                    query.TryAddPagingMode(pagenumber, pagesize);
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
                        ControllerObjectIdExtension.Builder(ControllerObjectId, BaseStatementId.GetList)));
            }
        }

    }
}
