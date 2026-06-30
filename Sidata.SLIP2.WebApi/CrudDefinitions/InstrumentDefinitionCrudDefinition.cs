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
    public class InstrumentDefinitionCrudDefinition :
        CrudDefinition<InstrumentDefinition, InstrumentDefinitionDto>
    {
        public override Func<InstrumentDefinitionDto, Expression<Func<InstrumentDefinition, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.MerchantId == dto.MerchantId &&
                              c.DefinitionCode == dto.DefinitionCode;
        public override Func<InstrumentDefinitionDto, Expression<Func<InstrumentDefinition, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.MerchantId == dto.MerchantId &&
                              c.DefinitionCode == dto.DefinitionCode &&
                              c.Id != dto.Id;
        public override Action<InstrumentDefinitionDto, InstrumentDefinition, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, InstrumentDefinition, copyid) =>
                {
                    InstrumentDefinition.InstrumentTypeId = dto.InstrumentTypeId;
                    InstrumentDefinition.DefinitionName = dto.DefinitionName;
                    InstrumentDefinition.BalanceStrategyType = dto.BalanceStrategyType;
                    InstrumentDefinition.EarnConversionRate = dto.EarnConversionRate;
                    InstrumentDefinition.EarnRoundingMode = dto.EarnRoundingMode;
                    InstrumentDefinition.EarnRoundingFactor = dto.EarnRoundingFactor;
                    InstrumentDefinition.RedeemConversionRate = dto.RedeemConversionRate;
                    InstrumentDefinition.RedeemRoundingMode = dto.RedeemRoundingMode;
                    InstrumentDefinition.RedeemRoundingFactor = dto.RedeemRoundingFactor;
                    InstrumentDefinition.IsActive = dto.IsActive;
                    InstrumentDefinition.AllowTopup = dto.AllowTopup;
                    InstrumentDefinition.AllowDebit = dto.AllowDebit;
                    InstrumentDefinition.HasExpiration = dto.HasExpiration;
                    InstrumentDefinition.ExpireAfterDays = dto.ExpireAfterDays;
                    InstrumentDefinition.MaximumBalance = dto.MaximumBalance;
                    InstrumentDefinition.SingleUseOnly = dto.SingleUseOnly;
                    InstrumentDefinition.Transferable = dto.Transferable;
                    InstrumentDefinition.AllowNegativeBalance = dto.AllowNegativeBalance;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        InstrumentDefinition.MerchantId = dto.MerchantId;
                        InstrumentDefinition.DefinitionCode = dto.DefinitionCode;
                        InstrumentDefinition.Id = dto.Id;
                    }
                };
        public override Func<InstrumentDefinitionDto, InstrumentDefinition>
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    MerchantId = dto.MerchantId,
                    InstrumentTypeId = dto.InstrumentTypeId,
                    DefinitionCode = dto.DefinitionCode,
                    DefinitionName = dto.DefinitionName,
                    BalanceStrategyType = dto.BalanceStrategyType,
                    EarnConversionRate = dto.EarnConversionRate,
                    EarnRoundingMode = dto.EarnRoundingMode,
                    EarnRoundingFactor = dto.EarnRoundingFactor,
                    RedeemConversionRate = dto.RedeemConversionRate,
                    RedeemRoundingMode = dto.RedeemRoundingMode,
                    RedeemRoundingFactor = dto.RedeemRoundingFactor,
                    IsActive = dto.IsActive,
                    AllowTopup = dto.AllowTopup,
                    AllowDebit = dto.AllowDebit,
                    HasExpiration = dto.HasExpiration,
                    ExpireAfterDays = dto.ExpireAfterDays,
                    MaximumBalance = dto.MaximumBalance,
                    SingleUseOnly = dto.SingleUseOnly,
                    Transferable = dto.Transferable,
                    AllowNegativeBalance = dto.AllowNegativeBalance
                };
        public override Expression<Func<InstrumentDefinition, InstrumentDefinitionDto>>
            LinqExpressionEntityToDto =>
                (cust) => new()
                {
                    Id = cust.Id,
                    MerchantId = cust.MerchantId,
                    InstrumentTypeId = cust.InstrumentTypeId,
                    DefinitionCode = cust.DefinitionCode,
                    DefinitionName = cust.DefinitionName,
                    BalanceStrategyType = cust.BalanceStrategyType,
                    EarnConversionRate = cust.EarnConversionRate,
                    EarnRoundingMode = cust.EarnRoundingMode,
                    EarnRoundingFactor = cust.EarnRoundingFactor,
                    RedeemConversionRate = cust.RedeemConversionRate,
                    RedeemRoundingMode = cust.RedeemRoundingMode,
                    RedeemRoundingFactor = cust.RedeemRoundingFactor,
                    IsActive = cust.IsActive,
                    AllowTopup = cust.AllowTopup,
                    AllowDebit = cust.AllowDebit,
                    HasExpiration = cust.HasExpiration,
                    ExpireAfterDays = cust.ExpireAfterDays,
                    MaximumBalance = cust.MaximumBalance,
                    SingleUseOnly = cust.SingleUseOnly,
                    Transferable = cust.Transferable,
                    AllowNegativeBalance = cust.AllowNegativeBalance
                };
    }
}