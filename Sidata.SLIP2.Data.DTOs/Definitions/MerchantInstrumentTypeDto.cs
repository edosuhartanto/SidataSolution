using Sidata.Abstractions.Interfaces;
using Sidata.SLIP2.Data.Enums;

namespace Sidata.SLIP2.Data.DTOs.Definitions
{
    /// <summary>
    /// Dto for MerchantInstrumentType entity class
    /// </summary>
    public class MerchantInstrumentTypeDto : IMasterClass
    {
        public long Id { get; set; }

        /// <summary>
        /// Foreign Key yg menghubungkan pelanggan ke 
        /// merchant tempat pelanggan ini bernaung (dimiliki).        /// 
        /// </summary>

        public long MerchantId { get; set; }

        public long InstrumentTypeId { get; set; }

        public bool IsDisabled { get; set; }
    }
}
