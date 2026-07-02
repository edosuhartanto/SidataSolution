
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.BaseClasses;
using Sidata.Abstractions.Interfaces;
using Sidata.Abstractions.Queryable.Models;
using Sidata.Abstractions.WebApi.Interfaces;
using Sidata.Abstractions.WebApi.ResponseRequest.Models;

namespace Sidata.Abstractions.WebApi.BaseControllers
{
    /// <summary>
    /// base controller untuk membentuk endpoint CRUD secara otomatis
    /// dan langsung tersedia di WebApi, tanpa perlu menulis satu pun attribute endpoint.<br/>
    /// class ini merupakan abstract class, sehingga harus diimplementasi di controller turunan.
    /// </summary>
    /// <remarks>
    /// Seluruh endpoint bersifat POST, dan standard action, tidak bisa diubah.
    /// Jika ingin menambahkan fungsi2 khusus, turunkan langsung dari class <seealso cref="BaseController{TDbContext, TEntity, TDto}"/>
    /// </remarks>
    public abstract class WebApiCrudControllerBase<TDbContext, TEntity, TDto>(
                          IDbContextFactory<TDbContext> dbfactory,
                          ICrudDefinition<TEntity, TDto> cruddefinition)
        : BaseController<TDbContext, TEntity, TDto>(dbfactory, cruddefinition)
    where TDbContext : DbContext
    where TEntity : PersistentObject
    where TDto : class, IMasterClass
    {
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
            RequestData<long> request)
        {
            return await EntityDeleteAsync(request);
        }
        #endregion

    }
}
