
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.WebApi.ResponseRequest.Models
{
    /// <summary>
    /// a data request special for getting a list of detail.
    /// data is blocked by value of id_header.
    /// </summary>
    public class RequestDetailData<TData> : RequestData<TData>
    {
        /// <summary>
        /// foreign key connector to header data
        /// </summary>
        public long Id_Header { get; set; }
    }
}
