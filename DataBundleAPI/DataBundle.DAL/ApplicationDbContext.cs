using DataBundle.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace DataBundle.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<APIAccount> APIAccount { get; set; } = null!;
        public virtual DbSet<APIRequest> APIRequest { get; set; } = null!;
        public virtual DbSet<APIRequestMetadata> APIRequestMetadata { get; set; } = null!;
        public virtual DbSet<APIUsage> APIUsage { get; set; } = null!;
        public virtual DbSet<APIBundle> APIBundle { get; set; } = null!;
        public virtual DbSet<APIBundleRequests> APIBundleRequests { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<APIBundleRequests>().HasKey(table => new {
                table.BundleId,
                table.RequestId
            });
        }
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../DataBundleAPI/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseSqlServer(connectionString);
            return new ApplicationDbContext(builder.Options);
        }

    }
    
}
