// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.BaseClasses;

namespace Sidata.SLIP2.Data.Masters
{
    /// <summary>
    /// Kelas yang merepresentasikan pelanggan (customer) di dalam sistem.
    /// Pelanggan dapat memiliki beberapa akun instrumen yang terkait dengannya,
    /// dan satu pelanggan ini dimiliki oleh satu merchant.
    /// </summary>
    public class Customer : PersistentObject
    {
        #region Foreign Keys Properties
        /// <summary>
        /// Foreign Key yg menghubungkan pelanggan ke 
        /// merchant tempat pelanggan ini bernaung (dimiliki).        /// 
        /// </summary>
        public long MerchantId { get; set; }

        /// <summary>
        /// (opsional) Foreign Key utk koneksi ke Simari Customer Id.
        /// </summary>
        /// <remarks>
        /// dapat dikosongkan jika customer tidak dikonekkan dengan simari.
        /// </remarks>
        public long SimariCustomerId { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// Kode identifikasi unik untuk pelanggan di dalam sistem.
        /// Kode ditentukan oleh pengguna dan ramah-pengguna (user-friendly)
        /// untuk mengidentifikasi seorang pelanggan.
        /// NIK (eKTP) adalah kandidat yang sangat bagus untuk digunakan sbg kode.
        /// </summary>
        public string CustomerNumber { get; set; } = default!;

        /// <summary>
        /// nama pelanggan. 
        /// nama yang ramah-pengguna utk keperluan tampilan dan pengenal referensi.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// (optional) email, utk alamat kontak
        /// </summary>
        public string? Email { get; set; }
        
        /// <summary>
        /// (optional) nomor telpon, utk alamat kontak. 
        /// Nomor dgn WA sangat direkomendasikan. 
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// tanggal lahir sebenarnya opsional, sebagai penanda informasi.
        /// dapat dijadikan penanda juga utk program promosi.
        /// </summary>
        public DateTime BirthDate { get; set; } = default!;
        
        /// <summary>
        /// status apakah seorang pelanggan masih aktif atau tidak.
        /// </summary>
        public bool IsActive { get; set; }
        #endregion

        #region Relationships
        /// <summary>
        /// hubungan dengan entitas Merchant.
        /// </summary>
        /// <example>
        /// tidak perlu melakukan configure foreign key ke sini
        /// krn Aggregate di kelas Merchant yang diregistrasi.
        /// </example>
        public Merchant Merchant { get; set; } = default!;

        /// <summary>
        /// hubungan dengan entitas InstrumentAccount. 
        /// satu customer dapat memiliki banyak InstrumentAccount 
        /// </summary>
        /// <remarks>
        /// (voucher no.001, voucher no.126, poin reward id=47112312, dll).
        /// </remarks>
        public ICollection<InstrumentAccount> InstrumentAccounts { get; set; } = [];
        #endregion
    }
}
