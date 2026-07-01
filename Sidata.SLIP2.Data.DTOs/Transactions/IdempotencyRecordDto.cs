using Sidata.Abstractions.Interfaces;
using Sidata.SLIP2.Data.Enums;
using Sidata.SLIP2.Data.Masters;

namespace Sidata.SLIP2.Data.DTOs.Transactions
{
    /// <summary>
    /// Dto for IdempotencyRecord entity class
    /// </summary>
    public class IdempotencyRecordDto : IMasterClass
    {
        public long Id { get; set; }

        /// <summary>
        /// Foreign Key yg menghubungkan pelanggan ke 
        /// merchant tempat pelanggan ini bernaung (dimiliki).        /// 
        /// </summary>
        /// 
        #region Properties
        /// <summary>
        /// build this key by caller
        /// </summary>
        public string IdempotencyKey { get; set; } = default!;

        /// <summary>
        /// set the status of idempotency
        /// 0=undefined 1=processing 2=completed 3=failed
        /// </summary>
        /// <remarks>
        /// public void AddRequest(RequestDto requestdto)
        /// {
        ///     try 
        ///     {
        ///         var key = BuildRandomIdempotencyKey();
        ///         var idem = saveidempotency(key, recordstatus.processing);
        ///         if (idem is not null) throw new Exception();
        ///         // ... your transaction here ...
        ///         updateidempotency(idem, recordstatus.completed);
        ///     }
        ///     catch(Exception ex)
        ///     {
        ///         if (idem isnot null)
        ///         {
        ///             updateidempotency(idem, recordstatus.failed);
        ///         }
        ///         else
        ///         {
        ///             key = buildGuid();
        ///             saveidempotency(key, recordstatus.Failed)
        ///         }
        ///     }
        /// }
        /// </remarks>
        public IdempotencyRecordStatus IdempotencyRecordStatus { get; set; }

        /// <summary>
        /// RequestHash should be build by server ... and return to caller ... 
        /// and everytime caller want to request the same transaction, it should send the same requesthash
        /// Server will check if requesthash is available or not
        /// </summary>
        public string RequestHash { get; set; } = default!;

        /// <summary>
        /// (opsional) Request Data can be saved in here.
        /// </summary>
        public string? RequestData { get; set; }

        /// <summary>
        /// when this idempotency will Expire
        /// </summary>
        public DateTime ExpireAtUtc { get; set; }
        #endregion

    }
}
