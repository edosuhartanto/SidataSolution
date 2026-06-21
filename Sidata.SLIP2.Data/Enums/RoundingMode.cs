// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.SLIP2.Data.Enums
{
    /// <summary>
    /// mode rounding for earn and redeem conversion rate,
    /// </summary>
    public enum RoundingMode : byte
    {
        /// <summary>
        /// no rounding, the value will be as it is, with all the decimal places
        /// </summary>
        None = 0,

        /// <summary>
        /// round down to the nearest integer
        /// </summary>
        Floor = 1,

        /// <summary>
        /// round up to the nearest integer
        /// </summary>
        Ceiling = 2,

        /// <summary>
        /// round to the nearest integer
        /// </summary>
        Round = 3
    }
}
