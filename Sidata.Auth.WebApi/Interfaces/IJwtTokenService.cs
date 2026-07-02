// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Auth.Data;

namespace Sidata.Abstractions.Auth.JWT.Services
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(ApiClient client);
        string GenerateLoginToken(AppUser user);
    }
}