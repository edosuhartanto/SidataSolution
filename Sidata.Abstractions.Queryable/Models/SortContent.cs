// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.Queryable.Enums;
using Sidata.Abstractions.Queryable.Interfaces;

namespace Sidata.Abstractions.Queryable.Models
{
    /// <summary>
    /// class untuk membentuk satu operasi sort. 
    /// </summary>
    /// <remarks>
    /// >>> ORDER BY Tanggal desc :
    /// PropertyName diisi "Tanggal",
    /// dan Direction diisi SortDirection.Descending
    /// </remarks>
    public class SortContent : IPropertyOperator
    {
        public required string PropertyName { get; set; }

        /// <summary>
        /// arah sort, default=A-Z (ascending) [=0 dlm byte]
        /// </summary>
        public SortDirection Direction { get; set; } // default = ascending
    }
}
