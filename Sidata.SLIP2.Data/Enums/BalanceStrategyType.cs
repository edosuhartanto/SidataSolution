// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.SLIP2.Data.Enums
{
    /// <summary>
    /// The strategy to calculate the balance of an instrument.
    /// </summary>
    public enum BalanceStrategyType : byte
    {
        /// <summary>
        /// no strategy attached
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// balance can be freely redeemed and refilled from transaction form
        /// </summary>
        Reusable = 1,

        /// <summary>
        /// balance is get from bucket, and bucket can only be refilled from transaction form
        /// each bucket have their own life (expired date)
        /// </summary>
        Bucket = 2,

        /// <summary>
        /// balance can only be redeemed once, and cannot be refilled
        /// </summary>
        OneTime = 3,

        /// <summary>
        /// balance cannot be refilled, but can be redeem partially
        /// </summary>
        PartialSingleTime = 4
    }
}
