// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.SLIP2.Data.Enums
{
    /// <summary>
    /// status of idempotency record
    /// </summary>
    public enum IdempotencyRecordStatus : byte
    {
        /// <summary>
        /// status is not defined yet
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// transaction is still processing
        /// </summary>
        Processing = 1,

        /// <summary>
        /// transaction is completed
        /// </summary>
        Completed = 2,

        /// <summary>
        /// transaction is failed
        /// </summary>
        Failed = 3
    }
}
