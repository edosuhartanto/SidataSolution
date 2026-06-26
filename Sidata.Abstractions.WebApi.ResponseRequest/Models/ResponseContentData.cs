
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.WebApi.ResponseRequest.Interfaces;

namespace Sidata.Abstractions.WebApi.ResponseRequest.Models
{
    public class ResponseContentData<TData> : IRequestResponseDto<TData>
    {
        /// <summary>
        /// page number from where the list of TParam get
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// total record per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// list of data return from an endpoint
        /// </summary>
        public List<TData> Data { get; set; } = [];

        /// <summary>
        /// if paging is used, totalsize is total all records actually have
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// total page available, if paging mode is off, total page = 1
        /// </summary>
        public long TotalPage => PageSize > 0 ?
                                    TotalSize / PageSize :
                                    1;
    }
}
