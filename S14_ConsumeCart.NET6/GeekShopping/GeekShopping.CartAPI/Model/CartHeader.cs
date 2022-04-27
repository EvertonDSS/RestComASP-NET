using GeekShopping.CartAPI.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.CartAPI.Model {
    [Table("cart_header")]

    public class CartHeader : BaseEntity {
        [Column("user_id")]
        public string UserId {  get; set; }
        [Column("coupon_code")]
        public string CouponCode { get; set; }
    }
}
