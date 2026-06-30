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
    public class InstrumentTypeCrudDefinition :
        CrudDefinition<InstrumentType, InstrumentTypeDto>
    {
        public override Func<InstrumentTypeDto, Expression<Func<InstrumentType, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.TypeCode == dto.TypeCode;
        public override Func<InstrumentTypeDto, Expression<Func<InstrumentType, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.TypeCode == dto.TypeCode &&
                               c.Id != dto.Id;
        public override Action<InstrumentTypeDto, InstrumentType, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, InstrumentType, copyid) =>
                {
                    InstrumentType.Id = dto.Id;
                    InstrumentType.Description = dto.Description;
                    InstrumentType.DefaultAllowTopup = dto.DefaultAllowTopup;
                    InstrumentType.DefaultAllowDebit = dto.DefaultAllowDebit;
                    InstrumentType.DefaultHasExpiration = dto.DefaultHasExpiration;
                    InstrumentType.DefaultExpireAfterDays = dto.DefaultExpireAfterDays;
                    InstrumentType.DefaultMaximumBalance = dto.DefaultMaximumBalance;
                    InstrumentType.DefaultSingleUseOnly = dto.DefaultSingleUseOnly;
                    InstrumentType.DefaultTransferable = dto.DefaultTransferable;
                    InstrumentType.DefaultAllowNegativeBalance = dto.DefaultAllowNegativeBalance;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        InstrumentType.TypeCode = dto.TypeCode;
                        InstrumentType.Id = dto.Id;
                    }
                };
        public override Func<InstrumentTypeDto, InstrumentType>
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    TypeCode = dto.TypeCode,
                    Description = dto.Description,
                    DefaultAllowTopup = dto.DefaultAllowTopup,
                    DefaultAllowDebit = dto.DefaultAllowDebit,
                    DefaultHasExpiration = dto.DefaultHasExpiration,
                    DefaultExpireAfterDays = dto.DefaultExpireAfterDays,
                    DefaultMaximumBalance = dto.DefaultMaximumBalance,
                    DefaultSingleUseOnly = dto.DefaultSingleUseOnly,
                    DefaultTransferable = dto.DefaultTransferable,
                    DefaultAllowNegativeBalance = dto.DefaultAllowNegativeBalance
                };
        public override Expression<Func<InstrumentType, InstrumentTypeDto>>
            LinqExpressionEntityToDto =>
                (cust) => new()
                {
                    Id = cust.Id,
                    MerchantId = cust.MerchantId,
                    TypeCode = cust.TypeCode,
                    Description = cust.Description,
                    DefaultAllowTopup = cust.DefaultAllowTopup,
                    DefaultAllowDebit = cust.DefaultAllowDebit,
                    DefaultHasExpiration = cust.DefaultHasExpiration,
                    DefaultExpireAfterDays = cust.DefaultExpireAfterDays,
                    DefaultMaximumBalance = cust.DefaultMaximumBalance,
                    DefaultSingleUseOnly = cust.DefaultSingleUseOnly,
                    DefaultTransferable = cust.DefaultTransferable,
                    DefaultAllowNegativeBalance = cust.DefaultAllowNegativeBalance
                };


    }
}