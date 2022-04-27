using GeekShopping.CartAPI.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.CartAPI.Model {
    
    [Table("cart_detail")]

    public class CartDetail : BaseEntity {
        public long CartHeaderId { get; set; }


        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader { get; set; }

        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Column("count")]
        public int Count { get; set; }



    }
}
