
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
    public sealed class MerchantInstrumentTypeConfiguration
        : IEntityTypeConfiguration<MerchantInstrumentType>
    {
        public void Configure(
            EntityTypeBuilder<MerchantInstrumentType> builder)
        {
            builder.ConfigurePersistentObject("MerchantInstrumentType");
            // remember, when a instrumen type is recorded in here
            // it means to disabled it, so isDisabled is set to default=true
            builder.ConfigureBooleanProperty(x => x.IsDisabled, 
                                             RequiredMode.Yes, 
                                             true);

            // FK
            builder.ConfigureForeignKey(m => m.Merchant, m => m.MerchantId,
                                        DeleteBehavior.NoAction,
                                        RequiredMode.Yes);
            builder.ConfigureForeignKey(m => m.InstrumentType, m => m.InstrumentTypeId,
                                        DeleteBehavior.NoAction,
                                        RequiredMode.Yes);

            // unique key
            builder.ConfigureIndexes(x => new { x.MerchantId, x.InstrumentTypeId }, 
                                     UniqueMode.Yes);
        }
    }
}
