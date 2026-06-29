// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
namespace Sidata.Abstractions.WebApi.Attributes
{
    /// <summary>
    /// attribut utk dipasangkan di nama class controller, 
    /// dan menyediakan angka spesifik. Angka ini digunakan sebagai
    /// identifikasi bila muncul pesan kesalahan.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ControllerObjectIdAttribute(int objectId) 
                : Attribute
    {
        public int Id { get; } = objectId;
    }
}