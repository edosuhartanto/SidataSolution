using Sidata.Abstractions.Interfaces;
using Sidata.SLIP2.Data.Enums;

namespace Sidata.SLIP2.Data.DTOs.Definitions
{
    /// <summary>
    /// Dto for InstrumentType entity class
    /// </summary>
    public class InstrumentTypeDto : IMasterClass
    {
        public long Id { get; set; }

        /// <summary>
        /// Foreign Key yg menghubungkan pelanggan ke 
        /// merchant tempat pelanggan ini bernaung (dimiliki).        /// 
        /// </summary>

        #region Properties
        /// <summary>
        /// code a small string to identify the type
        /// using Uppercase is strongly recommended
        /// </summary>
        public string TypeCode { get; set; } = default!;

        /// <summary>
        /// (optional) description of the type
        /// </summary>
        public string? Description { get; set; }
        #endregion

        #region Default Selectable Behavior
        public bool DefaultAllowTopup { get; set; }

        public bool DefaultAllowDebit { get; set; }

        public bool DefaultHasExpiration { get; set; }

        public int DefaultExpireAfterDays { get; set; }

        public decimal DefaultMaximumBalance { get; set; }

        public bool DefaultSingleUseOnly { get; set; }

        public bool DefaultTransferable { get; set; }

        public bool DefaultAllowNegativeBalance { get; set; }
        #endregion    
    }
}
