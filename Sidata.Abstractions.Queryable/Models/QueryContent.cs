// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.Queryable.Models
{ 
    /// <summary>
    /// class utk mendata apa saja yang mau dibuild sebagai expression Linq,
    /// Filters utk membentuk .Where()
    /// Sort utk membentuk .Sort()
    /// (opsional) Search utk mengirim sebuah string utk mencari dalam database 
    /// </summary>
    public class QueryContent
    {
        /// <summary>
        /// daftar operasi utk membentuk WHERE
        /// </summary>
        public List<FilterContent> Filters { get; set; } = [];

        /// <summary>
        /// daftar property utk membentuk SORT
        /// </summary>
        public List<SortContent> Sorts { get; set; } = [];

        /// <summary>
        /// opsional property utk menentukan ukuran jumlah record dalam satu page.
        /// jika paging mode tidak digunakan, isi dengan 0
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// opsional property utk kirim nomor halaman yang ingin dikembalikan.
        /// jika paging mode tidak digunakan, isi dengan 0
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// opsional property utk mencari dlm database, jika diperlukan
        /// biasanya search adalah operasi UI, namun jika semua dilakukan sepenuhnya
        /// di database engine, maka property ini bisa digunakan.
        /// </summary>
        public string? Search { get; set; }
    }
}
