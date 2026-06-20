// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.BaseClasses;
using Sidata.SLIP2.Data.Abstractions.Enums;
using Sidata.SLIP2.Data.Definitions;
using Sidata.SLIP2.Data.Transactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sidata.SLIP2.Data.Masters
{
    /// <summary>
    /// class that defined instrument that have balance
    /// own by a customer and a merchant
    /// have expiration date
    /// the behaviour of this instrument defined by InstrumentDefinition class
    /// sample of instrument like Voucher, PointReward, Membership, etc
    /// one customer can have multiple instruments
    /// </summary>
    public class InstrumentAccount : PersistentObject
    {
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

        #region Owner Relationships (Merchant, Customer, InstrumentDefinition)
        /// <summary>
        /// relationship to Merchant (based on MerchantId)
        /// </summary>
        public Merchant? Merchant { get; set; }

        /// <summary>
        /// relationship to Customer (based on CustomerId)
        /// </summary>
        public Customer? Customer { get; set; }

        /// <summary>
        /// relationship to InstrumentDefinition (based on InstrumentDefinitionId)
        /// </summary>
        public InstrumentDefinition? InstrumentDefinition { get; set; }
        #endregion

        #region Aggregation Relationships (BalanceBuckets)
        /// <summary>
        /// aggregation for BalanceBuckets who owns by this instrument.
        /// for ex. FIFO point bucket
        /// </summary>
        public ICollection<BalanceBucket> BalanceBuckets { get; set; } = [];
        #endregion

        #region Functional Properties (AvailableBalance)
        /// <summary>
        /// the available balance = current balance - reserved balance
        /// for ex.: current balance 100 ... reserved balance 50 ... available = 50
        /// </summary>
        [NotMapped]
        public decimal AvailableBalance { get => CurrentBalance - ReservedBalance; }
        #endregion
    }
}
