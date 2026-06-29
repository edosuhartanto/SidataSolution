using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidata.Abstractions.BaseClasses;
using Sidata.Abstractions.DataContext.Enums;

namespace Sidata.Abstractions.Queryable.SqlServer.Extensions
{
    public static class PersistentObjectConfigurationExtension
    {

        /// <summary>
        /// all object decended from PersistentObject
        /// must use this configuration extension to registers its persistent properties.
        /// using perssistentobject should declare its name
        /// </summary>
        public static void ConfigurePersistentObject<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            string tablename,
            bool UseSoftDelete = true)
            where TEntity : PersistentObject
        {
            // using perssistentobject should declare its name
            builder.ToTable(tablename);

            // id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // created audit trail
            builder.ConfigureUtcDateTimeProperty(x => x.CreatedAtUtc, 
                                                 RequiredMode.Yes, 
                                                 p => p.HasDefaultValueSql("getutcdate()"));
            builder.ConfigureString50Property(x => x.CreatedBy, 
                                                RequiredMode.Yes, 
                                                GlobalSharedConstants.DefaultDeveloperUserName);

            // updated/modified audit trail
            builder.Property(x => x.UpdatedAtUtc);
            builder.ConfigureString50Property(x => x.UpdatedBy);

            // deleted audit trail
            builder.Property(x => x.DeletedAtUtc);
            builder.ConfigureString50Property(x => x.DeletedBy);
            builder.ConfigureBooleanProperty(x => x.IsDeleted, 
                                             RequiredMode.Yes, 
                                             false);

            // rowversion
            builder.Property(x => x.RowVersion).IsRowVersion();

            // special indexes for isDeleted
            builder.ConfigureIndexes(x => x.IsDeleted);

            if (UseSoftDelete)
            {
                // default filter
                builder.HasQueryFilter(x => !x.IsDeleted);
            }
        }
    }
}
