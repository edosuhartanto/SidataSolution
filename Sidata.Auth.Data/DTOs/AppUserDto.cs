// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.Interfaces;

namespace Sidata.Auth.Data.DTOs
{
    public class AppUserDto : IMasterClass
    {
        public long Id { get; set; }
        public string Username { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public bool IsHashed { get; set; }
        public bool IsActive { get; set; }
    }
}