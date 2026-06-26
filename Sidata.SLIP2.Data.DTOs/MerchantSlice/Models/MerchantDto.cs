using Sidata.Abstractions.Interfaces;

namespace Sidata.SLIP2.Data.DTOs.MerchantSlice.Models
{
    /// <summary>
    /// Dto for Merchant entity class
    /// </summary>
    public class MerchantDto : IMasterClass
    {
        public long Id { get; set; }

        /// <summary>
        /// user defined and user friendly code to identify the merchant. 
        /// This is also a unique identifier, 
        /// A code that can be used for display purposes or as a reference in the system.
        /// </summary>
        public string MerchantCode { get; set; } = default!;

        /// <summary>
        /// The name of the merchant.
        /// </summary>
        public string MerchantName { get; set; } = default!;

        /// <summary>
        /// (optional) The email address of the merchant.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// (optional) The phone number of the merchant. 
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Indicates whether the merchant is active.
        /// Default is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

    }
}
