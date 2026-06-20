// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.SLIP2.Data.Abstractions.Enums
{
    /// <summary>
    /// status life cycle of an instrument
    /// </summary>
    public enum InstrumentAccountStatus : byte
    {
        /// <summary>
        /// instrument isnot defined yet
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// instrument is in draft mode, cannot be used in transaction.
        /// instrument must be activated first
        /// </summary>
        Draft = 1,

        /// <summary>
        /// instrument is active
        /// </summary>
        Active = 2,

        #region Non Active or Closed Statuses
        /// <summary>
        /// closed because admin force to close it (internal rule)
        /// </summary>
        Closed = 4,

        /// <summary>
        /// closed because user violate a rule, and admin suspend it,
        /// this status can be reactivated back to active
        /// </summary>
        Locked = 5,

        /// <summary>
        /// closed because user already get all the value of the instrument or
        /// definition defined it to closed once redeemed
        /// </summary>
        Redeemed = 6,

        /// <summary>
        /// closed because expired date is lapsed
        /// </summary>
        Expired = 7
        #endregion
    }
}
