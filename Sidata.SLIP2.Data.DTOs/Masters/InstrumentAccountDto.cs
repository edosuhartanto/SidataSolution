using Sidata.Abstractions.Interfaces;
using Sidata.SLIP2.Data.Enums;

namespace Sidata.SLIP2.Data.DTOs.Masters
{
    /// <summary>
    /// Dto for InstrumentAccount entity class
    /// </summary>
    public class InstrumentAccountDto : IMasterClass
    {
        public long Id { get; set; }

        #region FKeys (Merchant,Customer,InstrumentDefinition)
        /// <summary>
        /// FK to Merchant who owns this instrument
        /// </summary>
        public long MerchantId { get; set; }

        /// <summary>
        /// FK to Customer who owns this instrument
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// FK to InstrumentDefinition which defined the behaviour of this instrument
        /// </summary>
        public long InstrumentDefinitionId { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// user defined and user friendly account number
        /// everytime this object is created account number must be provided
        /// </summary>
        public string AccountNumber { get; set; } = default!;

        /// <summary>
        /// user defined and user friendly account name
        /// everytime this object is created account name must be provided
        /// </summary>
        public string AccountName { get; set; } = default!;

        /// <summary>
        /// the last balance. the UOM is always in point, not currency (eg.IDR)
        /// if balance saved in IDR, the kurs value will be 1:1 
        /// After Commit => CurrentBalance - Transaction.Amount
        /// </summary>
        public decimal CurrentBalance { get; set; }

        /// <summary>
        /// the balance before transaction committed. 
        /// the UOM is always in point, not currency IDR
        /// Before Commit => ReservedBalance + Transaction.Amount
        /// After Commit => ReservedBalance - Transaction.Amount
        /// </summary>
        public decimal ReservedBalance { get; set; }

        /// <summary>
        /// status of this instrument
        /// 0=undefined, 1=draft, 2=activated, 3=locked, 4=closed, 5=redeemed, 6=expired
        /// </summary>
        public InstrumentAccountStatus Status { get; set; }

        /// <summary>
        /// the activation date
        /// set when this account is activated
        /// </summary>
        public DateTime? ActivatedAtUtc { get; set; }

        /// <summary>
        /// if pin is set, to do transaction against this account, 
        /// must be using this Pin, 
        /// can be used as activation PIN,
        /// can be used as transaction PIN,
        /// this is a hash value of the PIN,
        /// Using PIN or not is defined in a policy, also the max attempt of failed and locked duration
        /// </summary>
        public string? PinHash { get; set; }

        /// <summary>
        /// if Pin failed for AttemptCount,
        /// this porperty will be set with a time when it will reset and allow to try again,
        /// </summary>
        public DateTime? PinLockedUntilUtc { get; set; }
        #endregion
    }
}
