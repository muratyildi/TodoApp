using Data.EntityFramework.Configurations;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TodoProject> TodoProjects { get; set; }
        public DbSet<TodoProjectItem> TodoProjectItems { get; set; }
        public DbSet<TodoProjectUserMap> TodoProjectUserMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new TodoProjectConfiguration());
            modelBuilder.ApplyConfiguration(new TodoProjectItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TodoProjectUserMapConfiguration());
        }
    }
}
