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
    public class AccountSummaryPeriodCrudDefinition :
        CrudDefinition<AccountSummaryPeriod, AccountSummaryPeriodDto>
    {
        public override Func<AccountSummaryPeriodDto, Expression<Func<AccountSummaryPeriod, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.AccountingPeriodYear == dto.AccountingPeriodYear && 
                              c.AccountingPeriodMonth == dto.AccountingPeriodMonth;
        public override Func<AccountSummaryPeriodDto, Expression<Func<AccountSummaryPeriod, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.AccountingPeriodYear == dto.AccountingPeriodYear &&
                              c.AccountingPeriodMonth == dto.AccountingPeriodMonth &&
                              c.Id != dto.Id;
        public override Action<AccountSummaryPeriodDto, AccountSummaryPeriod, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, AccountSummaryPeriod, copyid) =>
                {
                    AccountSummaryPeriod.InstrumentAccountId = dto.InstrumentAccountId;
                    AccountSummaryPeriod.AccountingPeriodYear = dto.AccountingPeriodYear;
                    AccountSummaryPeriod.AccountingPeriodMonth = dto.AccountingPeriodMonth;
                    AccountSummaryPeriod.OpeningBalance = dto.OpeningBalance;
                    AccountSummaryPeriod.TotalEarnAmount = dto.TotalEarnAmount;
                    AccountSummaryPeriod.TotalRedeemAmount = dto.TotalRedeemAmount;
                    AccountSummaryPeriod.TotalExpireAmount = dto.TotalExpireAmount;
                    AccountSummaryPeriod.TotalOtherAmount = dto.TotalOtherAmount;
                    AccountSummaryPeriod.ClosingBalance = dto.ClosingBalance;
                    AccountSummaryPeriod.IsClosed = dto.IsClosed;
                    AccountSummaryPeriod.ClosedAtUtc = dto.ClosedAtUtc;
                    AccountSummaryPeriod.ClosedBy = dto.ClosedBy;
                    
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        AccountSummaryPeriod.Id = dto.Id;
                    }
                };
        public override Func<AccountSummaryPeriodDto, AccountSummaryPeriod>
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    InstrumentAccountId = dto.InstrumentAccountId,
                    AccountingPeriodYear = dto.AccountingPeriodYear,
                    AccountingPeriodMonth = dto.AccountingPeriodMonth,
                    OpeningBalance = dto.OpeningBalance,
                    TotalEarnAmount = dto.TotalEarnAmount,
                    TotalRedeemAmount = dto.TotalRedeemAmount,
                    TotalExpireAmount = dto.TotalExpireAmount,
                    TotalOtherAmount = dto.TotalOtherAmount,
                    ClosingBalance = dto.ClosingBalance,
                    IsClosed = dto.IsClosed,
                    ClosedAtUtc = dto.ClosedAtUtc,
                    ClosedBy = dto.ClosedBy
                };
        public override Expression<Func<AccountSummaryPeriod, AccountSummaryPeriodDto>>
            LinqExpressionEntityToDto =>
                (cust) => new()
                {
                    Id = cust.Id,
                    InstrumentAccountId = cust.InstrumentAccountId,
                    AccountingPeriodYear = cust.AccountingPeriodYear,
                    AccountingPeriodMonth = cust.AccountingPeriodMonth,
                    OpeningBalance = cust.OpeningBalance,
                    TotalEarnAmount = cust.TotalEarnAmount,
                    TotalRedeemAmount = cust.TotalRedeemAmount,
                    TotalExpireAmount = cust.TotalExpireAmount,
                    TotalOtherAmount = cust.TotalOtherAmount,
                    ClosingBalance = cust.ClosingBalance,
                    IsClosed = cust.IsClosed,
                    ClosedAtUtc = cust.ClosedAtUtc,
                    ClosedBy = cust.ClosedBy
                };
    }
}