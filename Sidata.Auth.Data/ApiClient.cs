// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.BaseClasses;

namespace Sidata.Auth.Data
{
    public class ApiClient : PersistentObject
    {
        public string ClientCode { get; set; } = default!;
        public string ClientDescription { get; set; } = default!;
        public string ClientKeyHash { get; set; } = default!;
        public bool IsHashed { get; set; }
        public bool IsActive { get; set; }
    }
}