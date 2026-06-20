// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.SLIP2.Data.Abstractions.Enums
{
    /// <summary>
    /// type of a transaction.
    /// </summary>
    public enum LedgerTransactionType : byte
    {
        /// <summary>
        /// unknown type usually because error at input
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// add new point to balance
        /// </summary>
        Earn = 1,

        /// <summary>
        /// decrease point from balance
        /// </summary>
        Redeem = 2,

        /// <summary>
        /// make balance zero because instument is expired.
        /// Redeem till zero because expiration of an instrumen
        /// </summary>
        Expire = 3,

        /// <summary>
        /// can be plus or minus,
        /// to adjust balance of an instrument
        /// </summary>
        Adjustment = 4,

        /// <summary>
        /// move points from one instrument to another
        /// </summary>
        Migration = 5,

        /// <summary>
        /// move points from one customer to other customer
        /// </summary>
        TransferOwnership = 6,

        /// <summary>
        /// point inserted because instrument is activated
        /// </summary>
        Activation = 7,

        /// <summary>
        /// redeem point because account/instrument is closed
        /// </summary>
        Close = 12
    }
}
