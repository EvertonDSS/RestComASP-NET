using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponPI.Model.Context {
    public class MySQLContext : DbContext {

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Coupon> Coupons  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon {
                Id = 1,
                CouponCode = "EXEMPLO",
                DiscountAmount = 10
            });
            
            modelBuilder.Entity<Coupon>().HasData(new Coupon {
                Id = 2,
                CouponCode = "EVERTON",
                DiscountAmount = 15,
            }); ;
        }

    }
}
