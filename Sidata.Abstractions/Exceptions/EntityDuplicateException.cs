// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
namespace Sidata.Abstractions.Exceptions
{
    public class EntityDuplicateException(string message) : Exception(message), ISafeException 
    {
    }
}