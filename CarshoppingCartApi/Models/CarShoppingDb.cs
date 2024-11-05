using CarshoppingCartApi.Const;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace CarshoppingCartApi.Models
{
    public class CarShoppingDb:IdentityDbContext<IdentityUser>
    {
        public CarShoppingDb()
        {
            
        }
        public CarShoppingDb(DbContextOptions option):base()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-5O226AV\\SQLEXPRESS02;Database=CarShopping;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Car> cars {get;set;}
      
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<OrderStatus> orderStatuses { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Role.Admin.ToString(),
                    NormalizedName = "admin",
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                },
                 new IdentityRole()
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = Role.User.ToString(),
                     NormalizedName = "user",
                     ConcurrencyStamp = Guid.NewGuid().ToString(),
                 }
                

            );
            base.OnModelCreating(builder);
        }

    }
}
