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
    public class InstrumentAccountCrudDefinition :
        CrudDefinition<InstrumentAccount, InstrumentAccountDto>
    {
        public override Func<InstrumentAccountDto, Expression<Func<InstrumentAccount, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.MerchantId = dto.MerchantId && 
                              c.AccountNumber == dto.AccountNumber;
        public override Func<InstrumentAccountDto, Expression<Func<InstrumentAccount, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.MerchantId = dto.MerchantId &&
                              c.AccountNumber == dto.AccountNumber &&
                              c.Id != dto.Id;
        public override Action<InstrumentAccountDto, InstrumentAccount, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, InstrumentAccount, copyid) =>
                {
                    InstrumentAccount.CustomerId = dto.CustomerId;
                    InstrumentAccount.InstrumentDefinitionId = dto.InstrumentDefinitionId;
                    InstrumentAccount.AccountName = dto.AccountName;
                    InstrumentAccount.CurrentBalance = dto.CurrentBalance;
                    InstrumentAccount.ReservedBalance = dto.ReservedBalance;
                    InstrumentAccount.Status = dto.Status;
                    InstrumentAccount.ActivatedAtUtc = dto.ActivatedAtUtc;
                    InstrumentAccount.PinHash = dto.PinHash;
                    InstrumentAccount.PinLockedUntilUtc = dto.PinLockedUntilUtc;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        InstrumentAccount.MerchantId = dto.MerchantId;
                        InstrumentAccount.AccountNumber = dto.AccountNumber;
                        InstrumentAccount.Id = dto.Id;
                    }
                };
        public override Func<InstrumentAccountDto, InstrumentAccount>
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    MerchantId = dto.MerchantId,
                    CustomerId = dto.CustomerId,
                    InstrumentDefinitionId = dto.InstrumentDefinitionId,
                    AccountNumber = dto.AccountNumber,
                    AccountName = dto.AccountName,
                    CurrentBalance = dto.CurrentBalance,
                    ReservedBalance = dto.ReservedBalance,
                    Status = dto.Status,
                    ActivatedAtUtc = dto.ActivatedAtUtc,
                    PinHash = dto.PinHash,
                    PinLockedUntilUtc = dto.PinLockedUntilUtc
                };
        public override Expression<Func<InstrumentAccount, InstrumentAccountDto>>
            LinqExpressionEntityToDto =>
                (cust) => new()
                {
                    Id = cust.Id,
                    MerchantId = cust.MerchantId,
                    CustomerId = cust.CustomerId,
                    InstrumentDefinitionId = cust.InstrumentDefinitionId,
                    AccountNumber = cust.AccountNumber,
                    AccountName = cust.AccountName,
                    CurrentBalance = cust.CurrentBalance,
                    ReservedBalance = cust.ReservedBalance,
                    Status = cust.Status,
                    ActivatedAtUtc = cust.ActivatedAtUtc,
                    PinHash = cust.PinHash,
                    PinLockedUntilUtc = cust.PinLockedUntilUtc
                };


    }
}