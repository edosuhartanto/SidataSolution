
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidata.Abstractions.DataContext.Enums;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;
using Sidata.SLIP2.Data.Definitions;

namespace Sidata.SLIP2.Data.Context.Configurations.Definitions
{
    public sealed class InstrumentTypeConfiguration
        : IEntityTypeConfiguration<InstrumentType>
    {
        public void Configure(
            EntityTypeBuilder<InstrumentType> builder)
        {
            builder.ConfigurePersistentObject("InstrumentType");
            // without default value, means caller should provide the value explicitly
            builder.ConfigureCodeStringProperty(x => x.TypeCode, 
                                                RequiredMode.Yes);
            builder.ConfigureDescriptionStringProperty(x => x.Description);

            // default selectable behavior
            builder.ConfigureBooleanProperty(x => x.DefaultAllowTopup);
            builder.ConfigureBooleanProperty(x => x.DefaultAllowDebit);
            builder.ConfigureBooleanProperty(x => x.DefaultHasExpiration);
            builder.ConfigureIntegerProperty(x => x.DefaultExpireAfterDays);
            builder.ConfigureDecimalProperty(x => x.DefaultMaximumBalance, 20, 4);
            builder.ConfigureBooleanProperty(x => x.DefaultSingleUseOnly);
            builder.ConfigureBooleanProperty(x => x.DefaultTransferable);
            builder.ConfigureBooleanProperty(x => x.DefaultAllowNegativeBalance);

            // unique key
            builder.ConfigureIndexes(x => x.TypeCode, UniqueMode.Yes);

            // added aggregation one to many relationship
            // CAUTION: do use lazy load for aggregation ... can slowing query performance
            //          always explicit .include for better query performance
            builder.ConfigureAggregation(
                        t => t.InstrumentDefinitions,
                        b => b.InstrumentType,
                        b => b.InstrumentTypeId);
        }
    }
}
