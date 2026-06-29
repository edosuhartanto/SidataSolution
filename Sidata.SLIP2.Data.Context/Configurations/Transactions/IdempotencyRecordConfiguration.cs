
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
using Sidata.SLIP2.Data.Transactions;

namespace Sidata.SLIP2.Data.Context.Configurations.Transactions
{
    public sealed class IdempotencyRecordConfiguration
        : IEntityTypeConfiguration<IdempotencyRecord>
    {
        public void Configure(
            EntityTypeBuilder<IdempotencyRecord> builder)
        {
            builder.ConfigurePersistentObject("IdempotencyRecord");

            // without default value, means caller should provide the value explicitly
            builder.ConfigureString50Property(x => x.IdempotencyKey, 
                                                RequiredMode.Yes);
            builder.ConfigureEnumProperty(x => x.IdempotencyRecordStatus,
                                          RequiredMode.Yes, 
                                          IdempotencyRecordStatus.Undefined);
            builder.ConfigureString255Property(x => x.RequestHash, 
                                                       RequiredMode.Yes);
            builder.ConfigureMaxStringProperty(x => x.RequestData);
            builder.ConfigureUtcDateTimeProperty(x => x.ExpireAtUtc,    
                                                 RequiredMode.Yes);

            // unique key : IdempotencyKey (build by caller)
            //              + RequestHash (build by server)
            builder.ConfigureIndexes(x => new { x.IdempotencyKey, x.RequestHash },
                                     UniqueMode.Yes);
        }
    }
}
