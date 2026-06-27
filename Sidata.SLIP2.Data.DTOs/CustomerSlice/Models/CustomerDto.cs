using Sidata.Abstractions.Interfaces;

namespace Sidata.SLIP2.Data.DTOs.CustomerSlice.Models
{
    /// <summary>
    /// Dto for Customer entity class
    /// </summary>
    public class CustomerDto : IMasterClass
    {
        public long Id { get; set; }

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
    }
}
