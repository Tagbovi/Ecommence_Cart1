using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace ecomence_Cart.CartModel
{
    public class CartDb : DbContext
    {
        public CartDb(DbContextOptions<CartDb>options): base(options) { }

      

        public DbSet<Cart> CarT { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           
           

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");

          
            modelBuilder.Entity<Cart>().HasData(
                new
                {
                    ItemID=1,
                    ItemName="Air-Max",
                    Quantity=1,
                    UnitPrice=1000.00,
                    phoneNumbers=020303346,
                    AddedTime = DateTime.UtcNow

                }
                
                );

           
        }

    }
}
