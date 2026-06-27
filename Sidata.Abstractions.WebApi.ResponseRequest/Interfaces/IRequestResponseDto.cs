// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.WebApi.ResponseRequest.Interfaces
{
    public interface IRequestResponseDto<TData>
    {
        List<TData> Data { get; set; }
    }
}
