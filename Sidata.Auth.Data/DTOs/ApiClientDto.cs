// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.Interfaces;

namespace Sidata.Auth.Data.DTOs
{
    public class ApiClientDto : IMasterClass
    {
        public long Id { get; set; }
        public string ClientCode { get; set; } = default!;
        public string ClientDescription { get; set; } = default!;
        public string ClientKey { get; set; } = default!;
        public bool IsHashed { get; set; }
        public bool IsActive { get; set; }
    }
}