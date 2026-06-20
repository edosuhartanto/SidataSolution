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
    /// class utk mendaftarkan sebuah operasi filter.
    /// </summary>
    /// <remarks>
    /// >>> where Kode = "PC001"
    /// maka dibentuk class ini dengan PropertyName berisi "Kode",
    /// Operator diisi FilterOperator.Equal,
    /// dan Value diisi "PC001"
    /// </remarks>
    public class FilterContent : IPropertyOperator
    {
        public required string PropertyName { get; set; }

        /// <summary>
        /// operator dari operasi yang akan dilakukan
        /// </summary>
        public FilterOperator Operator { get; set; } // default EQUAL

        /// <summary>
        /// nilai yang menjadi target operasi
        /// </summary>
        public string? Value { get; set; }
    }
}
