// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.Queryable.Enums
{
    /// <summary>
    /// selector utk menentukan apakah pembuatan QueryContent 
    /// utk Filter saja, Sort saja, keduanya, atau tidak membuat apa pun
    /// </summary>
    public enum FilterSortSelector : byte
    {
        /// <summary>
        /// tidak membuat LINQ filter dan juga sort
        /// </summary>
        None = 0,

        /// <summary>
        /// membuat LINQ utk filter (="where")
        /// </summary>
        Filter = 1,

        /// <summary>
        /// membuat LINQ utk sort
        /// </summary>
        Sort = 2,

        /// <summary>
        /// membuat LINQ utk filter dan sort
        /// </summary>
        All = 0xF
    }
}
