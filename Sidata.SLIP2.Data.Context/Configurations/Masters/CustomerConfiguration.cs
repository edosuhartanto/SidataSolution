
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
    public sealed class CustomerConfiguration
        : IEntityTypeConfiguration<Customer>
    {
        public void Configure(
            EntityTypeBuilder<Customer> builder)
        {
            builder.ConfigurePersistentObject("Customer");
            builder.ConfigureLongIdProperty(x => x.SimariCustomerId, 
                                            RequiredMode.Yes, 0); 
            builder.ConfigureCodeStringProperty(x => x.CustomerNumber, 
                                                RequiredMode.Yes);
            builder.ConfigureDescriptionStringProperty(x => x.Name, 
                                                       RequiredMode.Yes);
            builder.ConfigureCodeStringProperty(x => x.PhoneNumber);
            builder.ConfigureDescriptionStringProperty(x => x.Email);
            builder.ConfigureBooleanProperty(x => x.IsActive, 
                                             RequiredMode.Yes, 
                                             true);

            // unique index for customer number
            // (unique across merchant? use this) =>; builder.ConfigureIndexes(x => x.CustomerNumber, true); 
            builder.ConfigureIndexes(x => new{ x.MerchantId, x.CustomerNumber }, 
                                     UniqueMode.Yes);
            builder.ConfigureIndexes(x => x.SimariCustomerId);

            // FK : leave here as recoqnition that no need to build FK if 
            //      there is ConfigureAggregation inside MerchantConfiguration
            //builder.ConfigureForeignKey(c => c.Merchant, c => c.MerchantId);

            // Aggregation of Account (what instruments does customer have?)
            builder.ConfigureAggregation(
                            c => c.InstrumentAccounts,
                            a => a.Customer,
                            a => a.CustomerId);
        }
    }
}
