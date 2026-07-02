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
    public class MerchantSummaryPeriodCrudDefinition :
        CrudDefinition<MerchantSummaryPeriod, MerchantSummaryPeriodDto>
    {
        public override Func<MerchantSummaryPeriodDto, Expression<Func<MerchantSummaryPeriod, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.MerchantId = dto.MerchantId &&
                              c.AccountingPeriodYear == dto.AccountingPeriodYear &&
                              c.AccountingPeriodMonth == dto.AccountingPeriodMonth;
        public override Func<MerchantSummaryPeriodDto, Expression<Func<MerchantSummaryPeriod, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.MerchantId = dto.MerchantId && 
                              c.AccountingPeriodYear == dto.AccountingPeriodYear &&
                              c.AccountingPeriodMonth == dto.AccountingPeriodMonth &&
                              c.Id != dto.Id;
        public override Action<MerchantSummaryPeriodDto, MerchantSummaryPeriod, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, MerchantSummaryPeriod, copyid) =>
                {
                    MerchantSummaryPeriod.TotalCustomerCount = dto.TotalCustomerCount;
                    MerchantSummaryPeriod.TotalActiveCustomerCount = dto.TotalActiveCustomerCount;
                    MerchantSummaryPeriod.TotalAccountCount = dto.TotalAccountCount;
                    MerchantSummaryPeriod.TotalActiveAccountCount = dto.TotalActiveAccountCount;
                    MerchantSummaryPeriod.TotalOpeningBalance = dto.TotalOpeningBalance;
                    MerchantSummaryPeriod.TotalClosingBalance = dto.TotalClosingBalance;
                    MerchantSummaryPeriod.TotalEarnAmount = dto.TotalEarnAmount;
                    MerchantSummaryPeriod.TotalRedeemAmount = dto.TotalRedeemAmount;
                    MerchantSummaryPeriod.TotalExpireAmount = dto.TotalExpireAmount;
                    MerchantSummaryPeriod.TotalOtherAmount = dto.TotalOtherAmount;

                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        MerchantSummaryPeriod.MerchantId = dto.MerchantId;
                        MerchantSummaryPeriod.AccountingPeriodYear = dto.AccountingPeriodYear;
                        MerchantSummaryPeriod.AccountingPeriodMonth = dto.AccountingPeriodMonth;
                        MerchantSummaryPeriod.Id = dto.Id;
                    }
                };
        public override Func<MerchantSummaryPeriodDto, MerchantSummaryPeriod>
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    MerchantId = dto.MerchantId,
                    AccountingPeriodYear = dto.AccountingPeriodYear,
                    AccountingPeriodMonth = dto.AccountingPeriodMonth,
                    TotalCustomerCount = dto.TotalCustomerCount,
                    TotalActiveCustomerCount = dto.TotalActiveCustomerCount,
                    TotalAccountCount = dto.TotalAccountCount,
                    TotalActiveAccountCount = dto.TotalActiveAccountCount,
                    TotalOpeningBalance = dto.TotalOpeningBalance,
                    TotalClosingBalance = dto.TotalClosingBalance,
                    TotalEarnAmount = dto.TotalEarnAmount,
                    TotalRedeemAmount = dto.TotalRedeemAmount,
                    TotalExpireAmount = dto.TotalExpireAmount,
                    TotalOtherAmount = dto.TotalOtherAmount
                };
        public override Expression<Func<MerchantSummaryPeriod, MerchantSummaryPeriodDto>>
            LinqExpressionEntityToDto =>
                (cust) => new()
                {
                    Id = cust.Id,
                    MerchantId = cust.MerchantId,
                    AccountingPeriodYear = cust.AccountingPeriodYear,
                    AccountingPeriodMonth = cust.AccountingPeriodMonth,
                    TotalCustomerCount = cust.TotalCustomerCount,
                    TotalActiveCustomerCount = cust.TotalActiveCustomerCount,
                    TotalAccountCount = cust.TotalAccountCount,
                    TotalActiveAccountCount = cust.TotalActiveAccountCount,
                    TotalOpeningBalance = cust.TotalOpeningBalance,
                    TotalClosingBalance = cust.TotalClosingBalance,
                    TotalEarnAmount = cust.TotalEarnAmount,
                    TotalRedeemAmount = cust.TotalRedeemAmount,
                    TotalExpireAmount = cust.TotalExpireAmount,
                    TotalOtherAmount = cust.TotalOtherAmount
                };
    }
}