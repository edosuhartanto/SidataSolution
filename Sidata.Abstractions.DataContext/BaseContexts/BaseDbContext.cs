// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.BaseClasses;
using Sidata.Abstractions.DataContext.Interfaces;

namespace Sidata.Abstractions.DataContext.BaseContexts
{
    
    /// <summary>
    /// default context user, jika contructor basedbcontext tidak diisi dengan
    /// object implementasi dari IContextUser yg benar
    /// </summary>
    public sealed class DefaultContextUser : IContextUser
    {
        public string UserName { get => GlobalSharedConstants.DefaultDeveloperUserName; }
    }

    /// <summary>
    /// Base DbContext utk digunakan dalam platform sbg dasar DbContext.
    /// Object base ini harus                                                                                                                                                                              diturunkan sebelum dapat digunakan
    /// </summary>
    public abstract class BaseDbContext<TContext>(
        DbContextOptions<TContext> options,
        Type contextconfigureclasstype,
        IContextUser? contextUser = null)
        : DbContext(options)
        where TContext : DbContext
    {
        private readonly Type _contextconfigureclasstype = contextconfigureclasstype;
        private readonly IContextUser _contextuser = contextUser ?? new DefaultContextUser();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(_contextconfigureclasstype.Assembly);
        }

        private void SetAuditInfo()
        {
            var entries = ChangeTracker.Entries<PersistentObject>();
            foreach (var entry in entries)
            {
                var e = entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        e.IsDeleted = false;
                        e.CreatedAtUtc = DateTime.UtcNow;
                        e.CreatedBy = _contextuser.UserName;
                        if (e is IPeriodAware trans)
                            trans.SetAccountingPeriod();
                        break;

                    case EntityState.Modified:
                        e.UpdatedAtUtc = DateTime.UtcNow;
                        e.UpdatedBy = _contextuser.UserName;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        e.IsDeleted = true;
                        e.DeletedAtUtc = DateTime.UtcNow;
                        e.DeletedBy = _contextuser.UserName;
                        break;
                }
            }
        }

        public override int SaveChanges()
        {
            SetAuditInfo();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}