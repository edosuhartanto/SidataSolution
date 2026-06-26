// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.WebApi.ResponseRequest.Interfaces
{
    public interface IRequestResponseDto<TData>
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        List<TData> Data { get; set; }
    }
}
