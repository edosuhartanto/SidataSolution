// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.Queryable.Interfaces
{
    /// <summary>
    /// alat bantu interfaces utk memudahkan mendapatkan property name
    /// dari expression yang dikirim lewat QueryContent
    /// </summary>
    public interface IPropertyOperator
    {
        /// <summary>
        /// nama property
        /// </summary>
        string PropertyName { get; set; }
    }
}
