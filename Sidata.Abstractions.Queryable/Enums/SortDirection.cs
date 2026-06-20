// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.Queryable.Enums
{
    /// <summary>
    /// arah sort yang diharapkan
    /// </summary>
    public enum SortDirection : byte
    {
        /// <summary>
        /// sort A-Z, default menggunakan arah ini
        /// </summary>
        Ascending = 0,

        /// <summary>
        /// sort Z-A
        /// </summary>
        Descending = 1
    }
}
