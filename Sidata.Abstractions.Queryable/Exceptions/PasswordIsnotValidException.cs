// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
namespace Sidata.Abstractions.Queryable.Exceptions
{
    public class PasswordIsnotValidException(string message) : SidataAuthenticationException(message)
    {
    }
}