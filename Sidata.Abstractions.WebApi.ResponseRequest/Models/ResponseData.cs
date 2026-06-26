
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.WebApi.ResponseRequest.Models
{
    /// <summary>
    /// template response data. So all endpoint should return OK result (=200).
    /// returnid is the identification if error is occured from a process.
    /// returnid = 0 => ok ... returnMessage will return empty string
    /// returnid <> 0 => error ... returnMessage will return the message
    /// </summary>
    public class ResponseData<TData>
    {
        /// <summary>
        /// the identification if error is occured from a process.
        /// = 0 => ok ... returnMessage will return empty string
        /// <> 0 => error ... returnMessage will return the message
        /// = 0, but returnMessage isnot empty, it means WARNING
        /// </summary>
        public long ReturnId { get; set; }
 
        /// <summary>
        /// the explicit short and simple error message.
        /// </summary>
        public string ReturnMessage { get; set; } = string.Empty;

        /// <summary>
        /// the real content of the data, 
        /// can be null, if returnId = 0
        /// if WARNNIG is called (returnId=0, returnMessage is not empty string)
        /// or when returnId isnot 0
        /// this list MUST return a string.
        /// </summary>
        public ResponseContentData<TData>? Content { get; set; } = null;
    }
}
