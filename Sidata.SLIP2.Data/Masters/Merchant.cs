// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.BaseClasses;
using Sidata.SLIP2.Data.Definitions;

namespace Sidata.SLIP2.Data.Masters
{
    /// <summary>
    /// Entitas yang merepresentasikan merchant (pedagang/mitra), 
    /// pemilik dari seluruh kelas dalam sistem ini.
    /// Meskipun kelas ini memiliki hubungan yang kuat dgn kelas2 dibawahnya, 
    /// namun kode pemuat (loader) dari database
    /// harus dipertimbangkan untuk dioptimalkan, 
    /// dan tidak memuat semua data terkait sekaligus, 
    /// karena hal itu dapat menyebabkan masalah performa.    
    /// </summary>
    public class Merchant : PersistentObject
    {
        #region Properties
        /// <summary>
        /// Kode Merchant yang ditentukan oleh pengguna dan ramah pengguna (user-friendly)
        /// untuk mengidentifikasi merchant.
        /// Kode Ini juga merupakan pengidentifikasi yg unik,
        /// Sebuah kode yang dapat digunakan untuk keperluan tampilan 
        /// atau sebagai referensi di dalam sistem.        
        /// </summary>
        public string MerchantCode { get; set; } = default!;

        /// <summary>
        /// nama merchant.
        /// </summary>
        public string MerchantName { get; set; } = default!;

        /// <summary>
        /// (optional) alamat email merchant.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// (optional) nomor telepon atau nomor WA merchant. 
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// menentukan apakah status merchant ini aktif atau tidak.
        /// </summary>
        public bool IsActive { get; set; } = true;
        #endregion

        #region Aggregation Relationships
        /// <summary>
        /// Hubungan yang menyatakan semua Pelanggan (Customer) yang dimiliki satu merchant.
        /// </summary>
        /// <remarks>
        /// merchant: TOKO_A => pelanggan: member01, member02, member03 ... etc
        /// </remarks>
        public ICollection<Customer> Customers { get; set; } = [];

        /// <summary>
        /// Hubungan yang menyatakan semua definisi instrument yang dimiliki 
        /// satu merchant.
        /// </summary>
        /// <remarks>
        /// merchant: TOKO_A => definition: pointmember, giftcard, voucher, deposit, ... etc
        /// </remarks>
        public ICollection<InstrumentDefinition> InstrumentDefinitions { get; set; } = [];
        #endregion
    }
}
