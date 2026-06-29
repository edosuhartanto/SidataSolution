// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.WebApi.ResponseRequest.Interfaces
{
    /// <summary>
    /// default Interface for Request and Response Data Contents
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface IRequestResponseContent<TData>
    {
        List<TData> Contents { get; set; }
    }
}
