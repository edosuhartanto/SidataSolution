using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.DataContext.BaseContexts;
using Sidata.Abstractions.DataContext.Interfaces;

namespace Sidata.Auth.Data.Context
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options,
                               IContextUser? contextuser = null)
        : BaseDbContext<AuthDbContext>(options,
                                       typeof(AuthDbContext),
                                       contextuser)
    {
        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<ApiClient> ApiClients => Set<ApiClient>();

    }
}
