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
        /// a list of parameters to be send to an endpoint
        /// </summary>
        public List<TData> Data { get; set; } = [];
    }
}
