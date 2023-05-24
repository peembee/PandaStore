using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PandaStore.Models;

namespace PandaStore.Data
{
    public class PandaStoreContext : IdentityDbContext<PandaUser, PandaRole, string>
    {
        public PandaStoreContext(DbContextOptions<PandaStoreContext> options)
            :base(options)
        {
            
        }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<CustomerProduct> CustomerProducts { get; set; }
        public DbSet<CustomerRate> CustomerRates { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<PandaUser> PandaUsers { get; set; }
        public DbSet<PandaRole> PandaRoles { get; set; }
    }
}
