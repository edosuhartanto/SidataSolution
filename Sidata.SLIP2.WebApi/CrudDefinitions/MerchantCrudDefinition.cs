// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.Services;
using Sidata.SLIP2.Data.DTOs.Masters;
using Sidata.SLIP2.Data.Masters;
using System.Linq.Expressions;

namespace Sidata.SLIP2.WebApi.CrudDefinitions
{
    public class MerchantCrudDefinition :
        CrudDefinition<Merchant, MerchantDto>
    {
        public override Func<MerchantDto, Expression<Func<Merchant, bool>>>
            InsertDuplicateChecker =>
                (dto) => m => m.MerchantCode == dto.MerchantCode;
        public override Func<MerchantDto, Expression<Func<Merchant, bool>>>
            UpdateDuplicateChecker =>
                (dto) => m => (m.MerchantCode == dto.MerchantCode) &&
                              (m.Id != dto.Id);
        public override Action<MerchantDto, Merchant, CopyIdStatus>
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
        public override Func<MerchantDto, Merchant>
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
        public override Expression<Func<Merchant, MerchantDto>>
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
    }
}