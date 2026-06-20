
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.BaseClasses;
using Sidata.SLIP2.Data.Masters;

namespace Sidata.SLIP2.Data.Definitions
{
    /// <summary>
    /// Connection to define which instrument type is disbled for a merchant
    /// Default is always enable for all merchant
    /// but this class can be used to disable certain instrument type 
    /// for a merchant
    /// </summary>
    public class MerchantInstrumentType : PersistentObject
    {
        public long MerchantId { get; set; }

        public long InstrumentTypeId { get; set; }
        
        public bool IsDisabled { get; set; }

        #region FK Relationships
        public Merchant Merchant { get; set; } = default!;

        public InstrumentType InstrumentType { get; set; } = default!;
        #endregion
    }
}
