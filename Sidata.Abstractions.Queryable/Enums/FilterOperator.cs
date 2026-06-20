// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.Queryable.Enums
{
        
    /// <summary>
    /// enum utk menentukan apa saja filter operator yang didukung QueryContent
    /// </summary>
    public enum FilterOperator : byte
    {
        /// <summary>
        /// sama dengan (=)
        /// </summary>
        Equal = 0,

        /// <summary>
        /// tidak sama dengan (!=)
        /// </summary>
        NotEqual = 1,

        /// <summary>
        /// lebih besar (>)
        /// </summary>
        GreaterThan = 2,

        /// <summary>
        /// lebih besar sama dengan (>=)
        /// </summary>
        GreaterThanOrEqual = 3,

        /// <summary>
        /// lebih kecil (<)
        /// </summary>
        LessThan = 4,

        /// <summary>
        /// lebih kecil sama dengan (<=)
        /// </summary>
        LessThanOrEqual = 5,

        /// <summary>
        /// spesial operator SQL utk mencari bagian dari sebuah string,
        /// dapat menggunakan wildcard seperti "%" atau "_" 
        /// [note: tergantung provider database] 
        /// dalam LINQ, operator ini membutuhkan proses building yg berbeda
        /// gunakan operator Contains, jika ada banyak Filter yang menggunakan LIKE
        /// agar proses building query lebih cepat.
        /// </summary>
        Like = 6,

        /// <summary>
        /// sama seperti LIKE 'find%', mencari kata di awal kalimat.
        /// </summary>
        StartsWith = 7,

        /// <summary>
        /// sama seperti LIKE '%find', mencari kata di akhir kalimat.
        /// </summary>
        EndsWith = 8,

        /// <summary>
        /// sama seperti LIKE '%find%', mencari kata di tengah kalimat.
        /// </summary>
        Contains = 9,

        /// <summary>
        /// mencari yang NULL
        /// </summary>
        IsNull = 10,

        /// <summary>
        /// mencari yang tidak NULL
        /// </summary>
        IsNotNull = 11,

        /// <summary>
        /// operator spesial utk mencari beberapa item sekaligus
        /// contoh: Kode in [1, 2, 3, 4] ... berarti mencari kode=1 atau 2, 
        /// atau 3, atau 4
        /// </summary>
        In = 12
    }
}
