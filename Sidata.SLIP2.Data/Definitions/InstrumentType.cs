
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.BaseClasses;

namespace Sidata.SLIP2.Data.Definitions
{
    /// <summary>
    /// Type of instrument.
    /// only have CodeName ... must be unique
    /// Description can be null ... to give a long description
    /// Eg. VOUCHER, GIFTCARD, etc
    /// NOTE: an instrument type is always available for all merchant,
    /// to disable some types for specific merchant 
    /// setup MerchantInstrumentType with isDisabled = true
    /// </summary>
    /// <remarks>
    /// QC=20260603
    /// </remarks>
    public class InstrumentType : PersistentObject
    {
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

        #region Aggregation Relationships
        /// <summary>
        /// all intrument definitions of this type
        /// </summary>
        public ICollection<InstrumentDefinition> InstrumentDefinitions { get; set; } = [];
        #endregion
    }
}
