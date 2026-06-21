// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.DataContext.BaseContexts;
using Sidata.Abstractions.DataContext.Interfaces;
using Sidata.SLIP2.Data.Definitions;
using Sidata.SLIP2.Data.Masters;
using Sidata.SLIP2.Data.Periods;
using Sidata.SLIP2.Data.Transactions;

namespace Sidata.SLIP2.Data.Context
{
    public class LoyaltyDbContext(DbContextOptions<LoyaltyDbContext> options,
                                  IContextUser? contextuser = null) 
        : BaseDbContext<LoyaltyDbContext>(options, 
                                            typeof(LoyaltyDbContext), 
                                            contextuser)
    {

        #region Masters
        public DbSet<Merchant> Merchants => Set<Merchant>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<InstrumentAccount> InstrumentAccounts => Set<InstrumentAccount>();
        #endregion

        #region Definitions
        public DbSet<MerchantInstrumentType> MerchantInstrumentTypes => Set<MerchantInstrumentType>();
        public DbSet<InstrumentType> InstrumentTypes => Set<InstrumentType>();
        public DbSet<InstrumentDefinition> InstrumentDefinitions => Set<InstrumentDefinition>();
        #endregion

        #region Transactions
        public DbSet<IdempotencyRecord> IdempotencyRecords => Set<IdempotencyRecord>();
        public DbSet<BalanceBucket> BalanceBuckets => Set<BalanceBucket>();
        public DbSet<LedgerTransaction> LedgerTransactions => Set<LedgerTransaction>();
        #endregion

        #region Periods
        public DbSet<AccountSummaryPeriod> AccountSummaryPeriods => Set<AccountSummaryPeriod>();
        public DbSet<MerchantSummaryPeriod> MerchantSummaryPeriods => Set<MerchantSummaryPeriod>();
        #endregion

    }
}
