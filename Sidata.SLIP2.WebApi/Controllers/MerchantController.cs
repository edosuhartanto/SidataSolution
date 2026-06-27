using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.Queryable.Models;
using Sidata.Abstractions.WebApi.Attributes;
using Sidata.Abstractions.WebApi.BaseControllers;
using Sidata.Abstractions.WebApi.ResponseRequest.Models;
using Sidata.SLIP2.Data.Context;
using Sidata.SLIP2.Data.DTOs.CustomerSlice.Models;
using Sidata.SLIP2.Data.DTOs.MerchantSlice.Extensions;
using Sidata.SLIP2.Data.DTOs.MerchantSlice.Models;
using Sidata.SLIP2.Data.Masters;

namespace Sidata.SLIP2.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ControllerObjectId(1)]
    public class MerchantController(IDbContextFactory<LoyaltyDbContext> dbfactory) 
        : WebApiCrudControllerBase<LoyaltyDbContext, Merchant, MerchantDto>(dbfactory)
    {
        //==================================================
        // OVERRIDE ABSTRACT FUNCTIONs
        //==================================================
        protected override Func<Merchant, MerchantDto> CopyEntityToDto =>
            x => new()
            {
                Id = x.Id,
                MerchantCode = x.MerchantCode,
                MerchantName = x.MerchantName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.IsActive
            };

        //==================================================
        // GET LIST
        //==================================================
        [HttpPost("getlist")]
        public async Task<ActionResult<ResponseData<MerchantDto>>> 
                        GetList(RequestData<QueryContent>? request = null)
        {
            return await BuildListAsync(
                                MerchantDataTransfer.LinqExpressionMerchantToDto, 
                                request);
        }

        //==================================================
        // GET BY ID
        //==================================================
        [HttpPost("getbyid")]
        public async Task<ActionResult<ResponseData<MerchantDto>>> 
            GetById(RequestData<long> id)
        {
            return await BuildByIdAsync(
                    MerchantDataTransfer.LinqExpressionMerchantToDto, 
                    id);
        }

        //==================================================
        // CREATE
        //==================================================
        [HttpPost("createnew")]
        public async Task<ActionResult<ResponseData<MerchantDto>>> 
            CreateNew(RequestData<MerchantDto> request)
        {
            return await EntityCreateAsync(
                (req) => x => x.MerchantCode == req.MerchantCode,
                MerchantDataTransfer.CopyDtoToMerchant,
                MerchantDataTransfer.CopyMerchantToDto,
                request);
        }

        //==================================================
        // UPDATE
        //==================================================
        [HttpPost("update")]
        public async Task<ActionResult<ResponseData<MerchantDto>>> 
            Update(RequestData<MerchantDto> request)
        {
            return await EntityUpdateAsync(
                (dto) => x => x.MerchantCode == dto.MerchantCode && 
                               x.Id != dto.Id,
                (dto, m) => dto.UpdateMerchant(m),
                MerchantDataTransfer.CopyMerchantToDto,
                request);
        }

        //==================================================
        // DELETE (SOFT DELETE)
        //==================================================
        [HttpPost("delete")]
        public async Task<ActionResult<ResponseData<MerchantDto>>> 
            Delete(RequestData<MerchantDto> request)
        {
            return await EntityDeleteAsync(request);
        }

    }
}
