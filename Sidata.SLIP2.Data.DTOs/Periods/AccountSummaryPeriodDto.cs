using Sidata.Abstractions.Interfaces;
using Sidata.SLIP2.Data.Enums;

namespace Sidata.SLIP2.Data.DTOs.Periods
{
    /// <summary>
    /// Dto for AccountSummaryPeriod entity class
    /// </summary>
    public class AccountSummaryPeriodDto : IMasterClass
    {
        public long Id { get; set; }

        /// <summary>
        /// Foreign Key yg menghubungkan pelanggan ke 
        /// merchant tempat pelanggan ini bernaung (dimiliki).        /// 
        /// </summary>

        #region Foreign keys
        /// <summary>
        /// instrument account which have these rekap 
        /// </summary>
        public long InstrumentAccountId { get; set; }
        #endregion

        #region Period Keys
        /// <summary>
        /// key for year
        /// </summary>
        public int AccountingPeriodYear { get; set; }

        /// <summary>
        /// key for month
        /// </summary>
        public int AccountingPeriodMonth { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// opening balance
        /// </summary>
        public decimal OpeningBalance { get; set; }

        /// <summary>
        /// total Earn
        /// </summary>
        public decimal TotalEarnAmount { get; set; }

        /// <summary>
        /// total Redeem
        /// </summary>
        public decimal TotalRedeemAmount { get; set; }

        /// <summary>
        /// total Expired
        /// </summary>
        public decimal TotalExpireAmount { get; set; }

        /// <summary>
        /// total other than earn, redeem, expired.
        /// adjust, closed
        /// </summary>
        public decimal TotalOtherAmount { get; set; }

        /// <summary>
        /// closing balance
        /// </summary>
        public decimal ClosingBalance { get; set; }
        #endregion

        #region Close States
        /// <summary>
        /// flag close state
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// when it is closed
        /// </summary>
        public DateTime? ClosedAtUtc { get; set; }

        /// <summary>
        /// who closed it
        /// </summary>
        public string? ClosedBy { get; set; }
        #endregion 
    }
}
