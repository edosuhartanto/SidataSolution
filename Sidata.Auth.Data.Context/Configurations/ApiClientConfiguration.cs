// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidata.Abstractions.DataContext.Enums;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;

namespace Sidata.Auth.Data.Context.Configurations
{
    public sealed class ApiClientConfiguration
        : IEntityTypeConfiguration<ApiClient>

    {
        public void Configure(EntityTypeBuilder<ApiClient> builder)
        {
            builder.ConfigurePersistentObject("ApiClient");
            builder.ConfigureString50Property(x => x.ClientCode,
                                              RequiredMode.Yes);
            builder.ConfigureMaxStringProperty(x => x.ClientKeyHash,
                                               RequiredMode.Yes);
            builder.ConfigureMaxStringProperty(x => x.ClientDescription,
                                               RequiredMode.Yes);
            builder.ConfigureBooleanProperty(x => x.IsHashed,
                                             RequiredMode.Yes,
                                             false);
            builder.ConfigureBooleanProperty(x => x.IsActive,
                                             RequiredMode.Yes,
                                             false);

            // unique index for client code
            builder.ConfigureIndexes(x => x.ClientCode, UniqueMode.Yes);
        }
    }
}