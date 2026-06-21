
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.BaseClasses;
using Sidata.Abstractions.DataContext.Interfaces;
using Sidata.SLIP2.Data.Enums;
using Sidata.SLIP2.Data.Masters;

namespace Sidata.SLIP2.Data.Transactions
{
    /// <summary>
    /// the real transaction 
    /// </summary>
    public class LedgerTransaction : PersistentObject, IPeriodAware 
    {
        #region FK (Merchant, InstrumentAccount)
        /// <summary>
        /// the owner of this transaction
        /// </summary>
        public long MerchantId { get; set; }

        /// <summary>
        /// the account who own this transaction
        /// </summary>
        public long InstrumentAccountId { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// reference number
        /// </summary>
        public string TransactionReferenceNumber { get; set; } = default!;

        /// <summary>
        /// the year of accounting period where this transaction is transacted.
        /// </summary>
        public int AccountingPeriodYear { get; set; }

        /// <summary>
        /// the month of accounting period where this transaction is transacted.
        /// </summary>
        public int AccountingPeriodMonth { get; set; }

        /// <summary>
        /// the date of transaction
        /// </summary>
        public DateTime TransactionDateAtUtc { get; set; } = default!;

        /// <summary>
        /// status transaction
        /// </summary>
        public LedgerTransactionType TransactionType { get; set; }

        /// <summary>
        /// amount of transaction
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// connection into IdempotencyRecord 
        /// if the transaction is build under it.
        /// If the value = 0, IdempotencyRecord isnot available
        /// for example, BalanceBucket Expiration
        /// will create TransactionType.Expire
        /// this transaction donot have IdempotencyRecord
        /// </summary>
        public long IdempotencyRecordId { get; set; }

        /// <summary>
        /// (optional) reference number from external source
        /// </summary>
        public string? ExternalReferenceNumber { get; set; }

        /// <summary>
        /// (optional) branch reference number
        /// </summary>
        public string? BranchReference { get; set; }

        /// <summary>
        /// (optional) machine/POS reference number
        /// </summary>
        public string? MachineReference { get; set; }

        /// <summary>
        /// (optional) remark
        /// </summary>
        public string? Remark { get; set; }
        #endregion

        #region Relationships
        /// <summary>
        /// Instrument Account who own this ledger
        /// </summary>
        public InstrumentAccount InstrumentAccount { get; set; } = default!;

        /// <summary>
        /// Merchant who own this ledger
        /// </summary>
        public Merchant Merchant { get; set; } = default!;

        #endregion

        #region IPeriodAware
        public void SetAccountingPeriod()
        {
            AccountingPeriodYear = TransactionDateAtUtc.Year;
            AccountingPeriodMonth = TransactionDateAtUtc.Month;
        }
        #endregion
    }
}
