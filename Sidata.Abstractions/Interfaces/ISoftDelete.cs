// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
namespace Sidata.Abstractions.Interfaces
{
    /// <summary>
    /// interface untuk menentukan kontrak bahwa sebuah object yang bisa 
    /// disimpan dalam database, seharusnya memiliki flag IsDeleted,
    /// yang membuat object tersebut tidak dihapus permanent,
    /// namun hanya ditandai
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// flag true/false yang menandakan bahwa object terhapus atau tidak.
        /// Default=false (tidak terhapus), 
        /// maka set ke "true" ketika dikatakan terhapus.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
