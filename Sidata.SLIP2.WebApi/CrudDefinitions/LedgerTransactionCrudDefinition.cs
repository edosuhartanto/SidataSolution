// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.Services;
using Sidata.SLIP2.Data.DTOs.Masters;
using Sidata.SLIP2.Data.DTOs.Transactions;
using Sidata.SLIP2.Data.Masters;
using Sidata.SLIP2.Data.Transactions;
using System.Linq.Expressions;

namespace Sidata.SLIP2.WebApi.CrudDefinitions
{
    public class LedgerTransactionCrudDefinition :
        CrudDefinition<LedgerTransaction, LedgerTransactionDto>
    {
        public override Func<LedgerTransactionDto, Expression<Func<LedgerTransaction, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => false;
        public override Func<LedgerTransactionDto, Expression<Func<LedgerTransaction, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.Id != dto.Id;
        public override Action<LedgerTransactionDto, LedgerTransaction, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, LedgerTransaction, copyid) =>
                {
                    LedgerTransaction.MerchantId = dto.MerchantId;
                    LedgerTransaction.InstrumentAccountId = dto.InstrumentAccountId;
                    LedgerTransaction.TransactionReferenceNumber = dto.TransactionReferenceNumber;
                    LedgerTransaction.AccountingPeriodYear = dto.AccountingPeriodYear;
                    LedgerTransaction.AccountingPeriodMonth = dto.AccountingPeriodMonth;
                    LedgerTransaction.TransactionDateAtUtc = dto.TransactionDateAtUtc;
                    LedgerTransaction.TransactionType = dto.TransactionType;
                    LedgerTransaction.Amount = dto.Amount;
                    LedgerTransaction.IdempotencyRecordId = dto.IdempotencyRecordId;
                    LedgerTransaction.ExternalReferenceNumber = dto.ExternalReferenceNumber;
                    LedgerTransaction.BranchReference = dto.BranchReference;
                    LedgerTransaction.MachineReference = dto.MachineReference;
                    LedgerTransaction.Remark = dto.Remark;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        LedgerTransaction.Id = dto.Id;
                    }
                };
        public override Func<LedgerTransactionDto, LedgerTransaction>
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    MerchantId = dto.MerchantId,
                    InstrumentAccountId = dto.InstrumentAccountId,
                    TransactionReferenceNumber = dto.TransactionReferenceNumber,
                    AccountingPeriodYear = dto.AccountingPeriodYear,
                    AccountingPeriodMonth = dto.AccountingPeriodMonth,
                    TransactionDateAtUtc = dto.TransactionDateAtUtc,
                    TransactionType = dto.TransactionType,
                    Amount = dto.Amount,
                    IdempotencyRecordId = dto.IdempotencyRecordId,
                    ExternalReferenceNumber = dto.ExternalReferenceNumber,
                    BranchReference = dto.BranchReference,
                    MachineReference = dto.MachineReference,
                    Remark = dto.Remark
                };
        public override Expression<Func<LedgerTransaction, LedgerTransactionDto>>
            LinqExpressionEntityToDto =>
                (cust) => new()
                {
                    Id = cust.Id,
                    MerchantId = cust.MerchantId,
                    InstrumentAccountId = cust.InstrumentAccountId,
                    TransactionReferenceNumber = cust.TransactionReferenceNumber,
                    AccountingPeriodYear = cust.AccountingPeriodYear,
                    AccountingPeriodMonth = cust.AccountingPeriodMonth,
                    TransactionDateAtUtc = cust.TransactionDateAtUtc,
                    TransactionType = cust.TransactionType,
                    Amount = cust.Amount,
                    IdempotencyRecordId = cust.IdempotencyRecordId,
                    ExternalReferenceNumber = cust.ExternalReferenceNumber,
                    BranchReference = cust.BranchReference,
                    MachineReference = cust.MachineReference,
                    Remark = cust.Remark
                };


    }
}