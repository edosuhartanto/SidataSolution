
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidata.Abstractions.DataContext.Enums;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;
using Sidata.SLIP2.Data.Masters;

namespace Sidata.SLIP2.Data.Context.Configurations.Masters
{
    public sealed class MerchantConfiguration
        : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(
            EntityTypeBuilder<Merchant> builder)
        {
            builder.ConfigurePersistentObject("Merchant");
            builder.ConfigureCodeStringProperty(x => x.MerchantCode, 
                                                RequiredMode.Yes);
            builder.ConfigureDescriptionStringProperty(x => x.MerchantName, 
                                                        RequiredMode.Yes);
            builder.ConfigureDescriptionStringProperty(x => x.Email);
            builder.ConfigureDescriptionStringProperty(x => x.PhoneNumber);
            builder.ConfigureBooleanProperty(x => x.IsActive, 
                                             RequiredMode.Yes, 
                                             false);

            // unique key
            builder.ConfigureIndexes(x => x.MerchantCode, UniqueMode.Yes);

            // added aggregation one to many relationship
            // CAUTION: do use lazy load for aggregation ... can slowing query performance
            //          always explicit .include for better query performance
            // Merchant - 
            builder.ConfigureAggregation(
                        m => m.Customers,
                        c => c.Merchant,
                        c => c.MerchantId);
            builder.ConfigureAggregation(
                        m => m.InstrumentDefinitions,
                        d => d.Merchant,
                        d => d.MerchantId);
        }
    }
}
