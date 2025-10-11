using ApiBase.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using Repository.SettingsEF.BlacklistedTokens;
using Repository.SettingsEF.Permissions;
using Repository.SettingsEF.RefreshTokens;
using Repository.SettingsEF.RolePermissions;
using Repository.SettingsEF.Roles;
using Repository.SettingsEF.Users;

namespace Repository
{
    public class ContextDataBase : Context
    {
        public ContextDataBase(DbContextOptions<ContextDataBase> options) : base(options) { }

        public override void ModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfig());
            modelBuilder.ApplyConfiguration(new BlacklistedTokenConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new PermissionConfig());
            modelBuilder.ApplyConfiguration(new RolePermissionConfig());
        }
    }
}
