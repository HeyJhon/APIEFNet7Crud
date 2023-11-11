using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Models;

namespace WebAPI.Database
{
    public class ApplicationDbContext : DbContext
    {
        private static readonly ILoggerFactory factory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales{ get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){}    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(connectionString)
            optionsBuilder.UseSqlServer()
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(factory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleDetail>().HasKey(sd => new {sd.SaleId,sd.ProductId});           
        }
    }
}
