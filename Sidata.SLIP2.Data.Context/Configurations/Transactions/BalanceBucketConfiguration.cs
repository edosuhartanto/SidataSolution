
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidata.Abstractions.DataContext.Enums;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;
using Sidata.SLIP2.Data.Transactions;

namespace Sidata.SLIP2.Data.Context.Configurations.Transactions
{
    public sealed class BalanceBucketConfiguration
        : IEntityTypeConfiguration<BalanceBucket>
    {
        public void Configure(
            EntityTypeBuilder<BalanceBucket> builder)
        {
            builder.ConfigurePersistentObject("BalanceBucket");
            // without default value, means caller should provide the value explicitly
            builder.ConfigureIntegerProperty(x => x.SequenceNumber, 
                                             RequiredMode.Yes);

            builder.ConfigureDecimalProperty(x => x.OriginalAmount, 20, 4,
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.ConsumedAmount, 20, 4,
                                             RequiredMode.Yes, 0);

            builder.ConfigureUtcDateTimeProperty(x => x.EarnedAtUtc,
                                                 RequiredMode.Yes);
            builder.ConfigureUtcDateTimeProperty(x => x.ExpireAtUtc,
                                                 RequiredMode.Yes);

            // FK
            builder.ConfigureForeignKey(x => x.InstrumentAccount, x => x.InstrumentAccountId,
                                        DeleteBehavior.NoAction, RequiredMode.Yes);

            // unique key
            // definition code is unique for each Merchant
            builder.ConfigureIndexes(x => new { x.InstrumentAccountId, x.SequenceNumber },
                                     UniqueMode.Yes);
        }
    }
}
