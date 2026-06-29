using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.Queryable.Models;
using Sidata.Abstractions.WebApi.Attributes;
using Sidata.Abstractions.WebApi.BaseControllers;
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.ResponseRequest.Models;
using Sidata.SLIP2.Data.Context;
using Sidata.SLIP2.Data.DTOs.Masters;
using Sidata.SLIP2.Data.Masters;
using System.Linq.Expressions;

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
        protected override Func<Merchant, MerchantDto> 
            CopyEntityToDto =>
                m => new()
                {
                    Id = m.Id,
                    MerchantCode = m.MerchantCode,
                    MerchantName = m.MerchantName,
                    Email = m.Email,
                    PhoneNumber = m.PhoneNumber,
                    IsActive = m.IsActive
                };

        protected override Expression<Func<Merchant, MerchantDto>> 
            LinqExpressionEntityToDto =>
                m => new()
                {
                    Id = m.Id,
                    MerchantCode = m.MerchantCode,
                    MerchantName = m.MerchantName,
                    Email = m.Email,
                    PhoneNumber = m.PhoneNumber,
                    IsActive = m.IsActive
                };

        protected override Action<MerchantDto, Merchant, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, merchant, copyid) =>
                {
                    merchant.MerchantName = dto.MerchantName;
                    merchant.Email = dto.Email;
                    merchant.PhoneNumber = dto.PhoneNumber;
                    merchant.IsActive = dto.IsActive;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        merchant.MerchantCode = dto.MerchantCode;
                        merchant.Id = dto.Id;
                    }
                };

        protected override Func<MerchantDto, Merchant> 
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    MerchantCode = dto.MerchantCode,
                    MerchantName = dto.MerchantName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    IsActive = dto.IsActive
                };

        protected override Func<MerchantDto, Expression<Func<Merchant, bool>>>
            InsertDuplicateChecker =>
                (dto) => m => m.MerchantCode == dto.MerchantCode;

        protected override Func<MerchantDto, Expression<Func<Merchant, bool>>>
            UpdateDuplicateChecker =>
                (dto) => m => (m.MerchantCode == dto.MerchantCode) &&
                              (m.Id != dto.Id);

    }
}
