
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.BaseClasses;

namespace Sidata.SLIP2.Data.Periods
{
    /// <summary>
    /// Rekap Statistic for each Merchant.
    /// Split based on Year and Month.
    /// Closing period state see AccountSummaryPeriod
    /// </summary>
    public class MerchantSummaryPeriod : PersistentObject
    {
        #region ForeignKeys
        /// <summary>
        /// Merchant yg memiliki semua rekap
        /// </summary>
        public long MerchantId { get; set; }
        #endregion region

        #region Period Controller
        /// <summary>
        /// the year of accounting period where this transaction is transacted.
        /// </summary>
        public int AccountingPeriodYear { get; set; }

        /// <summary>
        /// the month of accounting period where this transaction is transacted.
        /// </summary>
        public int AccountingPeriodMonth { get; set; }
        #endregion

        #region Properties
        public int TotalCustomerCount { get; set; }

        public int TotalActiveCustomerCount { get; set; }

        public int TotalAccountCount { get; set; }

        public int TotalActiveAccountCount { get; set; }

        public decimal TotalOpeningBalance { get; set; }

        public decimal TotalClosingBalance { get; set; }

        public decimal TotalEarnAmount { get; set; }

        public decimal TotalRedeemAmount { get; set; }

        public decimal TotalExpireAmount { get; set; }
        public decimal TotalOtherAmount { get; set; }
        #endregion
    }
}
