
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidata.Abstractions.DataContext.Enums;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;
using Sidata.SLIP2.Data.Periods;

namespace Sidata.SLIP2.Data.Context.Configurations.Periods
{
    public sealed class AccountSummaryPeriodConfiguration
        : IEntityTypeConfiguration<AccountSummaryPeriod>
    {
        public void Configure(
            EntityTypeBuilder<AccountSummaryPeriod> builder)
        {
            builder.ConfigurePersistentObject("AccountSummaryPeriod");

            // without default value, means caller should provide the value explicitly
            builder.ConfigureIntegerProperty(x => x.AccountingPeriodYear, 
                                             RequiredMode.Yes);
            builder.ConfigureIntegerProperty(x => x.AccountingPeriodMonth, 
                                             RequiredMode.Yes);
            builder.ConfigureDecimalProperty(x => x.OpeningBalance, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalEarnAmount, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalRedeemAmount, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalExpireAmount, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalOtherAmount, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.ClosingBalance, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureBooleanProperty(x => x.IsClosed, 
                                             RequiredMode.Yes, false);
            builder.ConfigureUtcDateTimeProperty(x => x.ClosedAtUtc);
            builder.ConfigureCodeStringProperty(x => x.ClosedBy);
            
            // FK
            builder.ConfigureIndexes(x => x.InstrumentAccountId);
            builder.ConfigureIndexes(x => x.IsClosed);

            // Unique key
            builder.ConfigureIndexes(x => new { x.InstrumentAccountId, 
                                                x.AccountingPeriodYear, 
                                                x.AccountingPeriodMonth }, 
                                     UniqueMode.Yes);

        }
    }
}
