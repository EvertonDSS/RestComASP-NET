using GeekShopping.CouponPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponPI.Model {
    [Table("coupon")]
    public class Coupon : BaseEntity{
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Column("id")]
        //public long Id { get; set; }

        [Column("coupon_code")]
        [Required]
        [StringLength(30)]
        public string CouponCode { get; set; }

        [Column("discount_amount")]
        [Required]
        public decimal DiscountAmount { get; set; }

    }
}
