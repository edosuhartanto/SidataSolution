// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
namespace Sidata.Abstractions.Interfaces
{
    /// <summary>
    /// interface utk menentukan kontrak, 
    /// bahwa sebuah object yang memiliki hubungan dgn data 
    /// yang bisa disimpan dalam database (=persistent)
    /// seharusnya memiliki Id, sebagai primary key
    /// </summary>
    public interface IMasterClass
    {
        /// <summary>
        /// Id ... harus bertype long (64 bit signed integer),
        /// max: 9,223,372,036,854,775,807 (19 digit)
        /// </summary>
        long Id { get; set; }
    }
}
