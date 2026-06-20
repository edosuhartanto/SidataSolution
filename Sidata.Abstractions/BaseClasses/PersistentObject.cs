// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Sidata.Abstractions.BaseClasses
{
    /// <summary>
    /// Abstract class utk memastikan semua object yang ingin bisa 
    /// dipersistent ke PersistentEngine (eg. EFCore) memiliki pola dan format 
    /// yang sama
    /// Siapa yg create, siapa update, siapa delete dan waktunya kapan
    /// </summary>
    public abstract partial class PersistentObject:ISoftDelete, IMasterClass
    {

        /// <summary>
        /// Id = long (64 bit), harus jadi PrimaryKey.
        /// Bisa autonumber/identity. 
        /// Rekomendasi format = sss.cc.yyyymmdd.nnnnn (18 digit tanpa titik).
        /// </summary>
        /// <remarks>
        /// sss = kode site/merchant (max=999); 
        /// cc = kode mesin (max=99); 
        /// yyyymmdd = tanggal terjadinya record (max=9999-99-99); 
        /// nnnn = sequential number (max=99999)
        /// </remarks>
        /// <example>
        /// kode site/merchant=315 
        /// kode mesin=24
        /// tanggal terjadi=16 maret 2024
        /// nomor urut di site, mesin, dan tanggal itu=325
        /// ID = 315242024031600325
        /// </example>
        public long Id { get; set; }

        /// <summary>
        /// kapan record dibuat pertama kali (dlm UTC).
        /// </summary>
        public DateTime CreatedAtUtc { get; set; }

        /// <summary>
        /// siapa yang membuat pertama kali.
        /// </summary>
        public string CreatedBy { get; set; } = default!;

        /// <summary>
        /// kapan diubah terakhir kali (dlm UTC).
        /// </summary>
        public DateTime? UpdatedAtUtc { get; set; }

        /// <summary>
        /// siapa yang mengubah
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// kapan dihapus (dlm UTC).
        /// </summary>
        public DateTime? DeletedAtUtc { get; set; }

        /// <summary>
        /// siapa yang menghapus
        /// </summary>
        public string? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// just a timestamp ... used by many cache engines
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; } = default!;
    }
}
