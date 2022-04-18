using GeekShopping.CartAPI.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.CartAPI.Data.ValueObjects {

    public class CartHeaderVO {

        public long Id { get; set; }

        public string UserId { get; set; }

        public string CouponCode { get; set; }
    }
}
