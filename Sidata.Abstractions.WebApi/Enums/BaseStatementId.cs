
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.WebApi.Enums
{
    /// <summary>
    /// pengenal untuk statement yang ada dalam CRUD system
    /// </summary>
    public enum BaseStatementId : int
    {
        Unknown = 0,
        Create = 1,
        Update = 2,
        Delete = 3,
        GetList = 4,
        GetById = 5,
        AccessToken = 6
    }
}
