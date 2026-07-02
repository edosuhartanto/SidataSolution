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
    public sealed class AppUserConfiguration
        : IEntityTypeConfiguration<AppUser>

    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ConfigurePersistentObject("AppUser");
            builder.ConfigureString50Property(x => x.Username,
                                              RequiredMode.Yes);
            builder.ConfigureString255Property(x => x.FullName,
                                               RequiredMode.Yes);
            builder.ConfigureString255Property(x => x.Email);
            builder.ConfigureString50Property(x => x.PhoneNumber);
            builder.ConfigureMaxStringProperty(x => x.PasswordHash);
            builder.ConfigureBooleanProperty(x => x.IsHashed,
                                             RequiredMode.Yes,
                                             false);
            builder.ConfigureBooleanProperty(x => x.IsActive,
                                             RequiredMode.Yes,
                                             false);

            // unique index for username and email should be unique
            builder.ConfigureIndexes(x => x.Username, UniqueMode.Yes);
            builder.ConfigureIndexes(x => x.Email, UniqueMode.Yes);
        }
    }
}