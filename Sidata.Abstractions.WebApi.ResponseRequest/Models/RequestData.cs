// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.WebApi.ResponseRequest.Interfaces;

namespace Sidata.Abstractions.WebApi.ResponseRequest.Models
{
    /// <summary>
    /// request data model.
    /// can hold a list of TParam as parameter
    /// </summary>
    /// <typeparam name="TData">
    /// segala macam type bisa disimpan disini,
    /// tergantung kebutuhan endpoint
    /// </typeparam>
    public class RequestData<TData>: IRequestResponseDto<TData>
    {
        /// <summary>
        /// page number where caller wants to request from 
        /// send 0 if request donot use paging system
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// total record per page, caller wants to request
        /// send 0 if request donot use paging system
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// a list of parameters to be send to an endpoint
        /// </summary>
        public List<TData> Data { get; set; } = [];
    }
}
