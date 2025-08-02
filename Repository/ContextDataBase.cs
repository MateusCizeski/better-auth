using ApiBase.Core.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using Repository.SettingsEF.Users;

namespace Repository
{
    public class ContextDataBase : Context
    {
        public ContextDataBase(DbContextOptions<Context> options) : base(options) { }

        public override void ModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}
