
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.WebApi.Enums
{
    /// <summary>
    /// status yg menentukan apakah unique fields (id atau kode atau 
    /// nomor bukti transaksi ... biasanya diset SetIndexes(unique = true))
    /// dicopy saat memasukkan data dari class Dto ke Entity.
    /// </summary>
    public enum CopyIdStatus : byte
    {
        DonotCopy = 0,
        CopyIt = 1
    }
}
