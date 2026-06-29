
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidata.Abstractions.DataContext.Enums;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;
using Sidata.SLIP2.Data.Enums;
using Sidata.SLIP2.Data.Masters;

namespace Sidata.SLIP2.Data.Context.Configurations.Masters
{
    public sealed class InstrumentAccountConfiguration
        : IEntityTypeConfiguration<InstrumentAccount>
    {
        public void Configure(
            EntityTypeBuilder<InstrumentAccount> builder)
        {
            builder.ConfigurePersistentObject("InstrumentAccount");
            // without default value, means caller should provide the value explicitly
            builder.ConfigureString50Property(x => x.AccountNumber, 
                                                RequiredMode.Yes);
            // without default value, means caller should provide the value explicitly
            builder.ConfigureString255Property(x => x.AccountName, 
                                                        RequiredMode.Yes);            
            builder.ConfigureDecimalProperty(x => x.CurrentBalance, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.ReservedBalance, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureEnumProperty(x => x.Status, 
                                            RequiredMode.Yes, 
                                            InstrumentAccountStatus.Undefined);
            builder.ConfigureUtcDateTimeProperty(x => x.ActivatedAtUtc);
            // pinhash used Description mode string (255 char)
            // to make sure that hash data has enough spaces
            // donot use nvarchar(max) (using generic ConfigureStringProperty()
            // without MaxLength) because it will slow down the query a bit
            builder.ConfigureString255Property(x => x.PinHash);
            builder.ConfigureUtcDateTimeProperty(x => x.PinLockedUntilUtc);

            // FK : because there is no Aggregation in MerchantConfigure
            builder.ConfigureForeignKey(a => a.Merchant, 
                                        a => a.MerchantId, 
                                        DeleteBehavior.NoAction, 
                                        RequiredMode.Yes);

            // unique key
            builder.ConfigureIndexes(x => new { x.MerchantId, x.AccountNumber }, 
                                     UniqueMode.Yes);
            builder.ConfigureIndexes(x => x.Status);
            
            // added aggregation one to many relationship
            // CAUTION: do use lazy load for aggregation ... can slowing query performance
            //          always explicit .include for better query performance
            builder.ConfigureAggregation(
                        a => a.BalanceBuckets,
                        b => b.InstrumentAccount,
                        b => b.InstrumentAccountId);
        }
    }
}
