using Sidata.Abstractions.Interfaces;
using Sidata.SLIP2.Data.Enums;
using Sidata.SLIP2.Data.Masters;

namespace Sidata.SLIP2.Data.DTOs.Transactions
{
    /// <summary>
    /// Dto for BalanceBucket entity class
    /// </summary>
    public class BalanceBucketDto : IMasterClass
    {
        public long Id { get; set; }

        /// <summary>
        /// Foreign Key yg menghubungkan pelanggan ke 
        /// merchant tempat pelanggan ini bernaung (dimiliki).        /// 
        /// </summary>
        /// 
        
        #region FK (InstrumentAccount)
        /// <summary>
        /// id to instrument account
        /// </summary>
        public long InstrumentAccountId { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// number to identify the sequence
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// the earning balance (the start amount)
        /// </summary>
        public decimal OriginalAmount { get; set; }

        /// <summary>
        /// amount value has been used/consumed, 
        /// it should be less than or equal to 
        /// OriginalAmount
        /// </summary>
        public decimal ConsumedAmount { get; set; }

        /// <summary>
        /// the date when the first balance is earned
        /// </summary>
        public DateTime EarnedAtUtc { get; set; } = default!;

        /// <summary>
        /// when the time do this balance expire
        /// REMEMBER: set it when build this object
        /// </summary>
        public DateTime ExpireAtUtc { get; set; } = default!;
        #endregion

        #region Owner Relationships
        /// <summary>
        /// instrument account object which own this bucket
        /// </summary>
        public InstrumentAccount InstrumentAccount { get; set; } = default!;
        #endregion

        #region Functional Properties
        /// <summary>
        /// flag that defined if this bucket is already expired or not
        /// </summary>
        public bool IsExpired => ExpireAtUtc <= DateTime.UtcNow;

        /// <summary>
        /// the remaining amount of this bucket
        /// </summary>
        public decimal RemainingAmount { get => OriginalAmount - ConsumedAmount; }
        #endregion
    }
}
