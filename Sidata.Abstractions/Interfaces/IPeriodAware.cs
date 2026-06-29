// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.Interfaces
{
    /// <summary>
    /// Interface untuk memastikan sebuah Entity membutuhkan
    /// proses setting periode accounting ke dirinya sendiri
    /// berdasarkan tanggal transaksi.
    /// </summary>
    public interface IPeriodAware
    {
        public void SetAccountingPeriod();
    }
}
