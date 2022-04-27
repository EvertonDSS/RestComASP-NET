using GeekShopping.CartAPI.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.CartAPI.Data.ValueObjects {



    public class CartDetailVO {

        public long Id { get; set; }

        public long CartHeaderId { get; set; }

        public CartHeaderVO CartHeader { get; set; }

        public long ProductId { get; set; }

        public ProductVO Product { get; set; }

        public int Count { get; set; }



    }
}
