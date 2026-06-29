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
        /// sama seperti LIKE 'find%', mencari kata di awal kalimat.
        /// </summary>
        StartsWith = 6,

        /// <summary>
        /// sama seperti LIKE '%find', mencari kata di akhir kalimat.
        /// </summary>
        EndsWith = 7,

        /// <summary>
        /// sama seperti LIKE '%find%', mencari kata di tengah kalimat.
        /// </summary>
        Contains = 8,

        /// <summary>
        /// mencari yang NULL
        /// </summary>
        IsNull = 9,

        /// <summary>
        /// mencari yang tidak NULL
        /// </summary>
        IsNotNull = 10,

        /// <summary>
        /// operator spesial utk mencari beberapa item sekaligus
        /// contoh: Kode in [1, 2, 3, 4] ... berarti mencari kode=1 atau 2, 
        /// atau 3, atau 4
        /// </summary>
        In = 11,

        /// <summary>
        /// spesial operator SQL utk mencari bagian dari sebuah string,
        /// dengan wildcard secara bebas (tidak seperti Contains).<br/>
        /// Karakter wildcard yang didukung, seperti:
        /// <list type="bullet">
        /// <item>'%'(percent)     : kata bebas</item>
        /// <item>'_'(underscore)  : 1 karakter bebas</item>
        /// <item>' '(spasi)       : akan diganti menjadi "%"</item>
        /// <item>'"'(doublequote) : akan diganti menjadi " " (spasi)</item>
        /// </list>
        /// <b>contoh:</b>
        /// [boneka "XL"] => [%boneka% XL %]
        /// hasilnya [bantal boneka bebek xl pink]
        /// tetapi bukan [boneka doraemon xl] 
        /// (krn "XL" di kalimat ini hanya memiliki spasi di depan)<br/>
        /// [boneka "XL] => inputan ini yg akan menghasilkan 
        /// [boneka doraemon xl] tetapi juga akan menghasilkan 
        /// [boneka ertiga xl7]
        /// </summary>
        /// <remarks>
        /// LIKE sangat tergantung pada provider database.<br/> 
        /// Dalam LINQ, operator ini membutuhkan proses building yg berbeda.<br/>
        /// Gunakan operator <seealso cref="Contains">Contains</seealso>, 
        /// jika ada banyak Filter yang menggunakan LIKE
        /// agar proses building query lebih cepat, serta lebih idiomatik.
        /// </remarks>
        Like = 12
    }
}
