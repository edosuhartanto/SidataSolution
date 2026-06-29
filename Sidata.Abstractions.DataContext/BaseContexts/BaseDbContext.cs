// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.BaseClasses;
using Sidata.Abstractions.DataContext.Interfaces;
using Sidata.Abstractions.Interfaces;

namespace Sidata.Abstractions.DataContext.BaseContexts
{
    
    /// <summary>
    /// default context user, jika contructor basedbcontext tidak diisi 
    /// dengan object implementasi dari IContextUser yg benar
    /// </summary>
    public sealed class DefaultContextUser : IContextUser
    {
        public string UserName { get => GlobalSharedConstants.DefaultDeveloperUserName; }
    }

    /// <summary>
    /// Base DbContext utk digunakan dalam Sidata platform sbg dasar 
    /// DbContext. Object base ini harus diturunkan untuk bisa digunakan
    /// krn class ini masih sebuah abstract.
    /// </summary>
    /// <remarks>
    /// BaseDbContext bersifat agnostic terhadap provider database. 
    /// Setting yang lebih dekat dengan provider (seperti SqlServer)
    /// disediakan di namespace Sidata.Abstractions.Queryable.SqlServer.
    /// </remarks>
    /// <param name="contextconfigureclasstype">
    /// kirimkan typeof() sebuah class yang memiliki semua function 
    /// untuk configure model.
    /// </param>
    public abstract class BaseDbContext<TContext>(
        DbContextOptions<TContext> options,
        Type contextconfigureclasstype,
        IContextUser? contextUser = null)
        : DbContext(options)
        where TContext : DbContext
    {
        private readonly Type _contextconfigureclasstype = contextconfigureclasstype;
        private readonly IContextUser _contextuser = contextUser ?? new DefaultContextUser();


        /// <summary>
        /// Saat ini digunakan utk memanggil semua file configure yang 
        /// disediakan di sebuah namespace yg menjadi lokasi dari
        /// type yg dikirim lewat parameter constructor bernama
        /// "contextconfigureclasstype"
        /// </summary>
        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(_contextconfigureclasstype.Assembly);
        }

        /// <summary>
        /// default setup audit info, sesuai property PersistentObject,
        /// berlaku untuk Added, Modified dan Deleted.<br/>
        /// Delete bersifat softdelete, menggunakan flag dan tidak dihapus
        /// secara fisik.
        /// </summary>
        /// <remarks>
        /// turunkan jika dirasa konsep standard perlu diubah.
        /// </remarks>
        protected virtual void SetAuditInfo()
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

        /// <summary>
        /// turunan dari SaveChanges yang menjalankan set AuditInfo
        /// </summary>
        public override int SaveChanges()
        {
            SetAuditInfo();
            return base.SaveChanges();
        }

        /// <summary>
        /// turunan dari SaveChangesAsync</seealso> yang menjalankan set AuditInfo
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}