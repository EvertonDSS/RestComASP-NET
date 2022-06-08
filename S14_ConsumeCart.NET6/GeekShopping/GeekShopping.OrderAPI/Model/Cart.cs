using GeekShopping.OrderAPI.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GeekShopping.CartAPI.Model {
    public class Cart {
       
        public CartHeader CartHeader { get; set; }
        [ValidateNever]
        public IEnumerable<CartDetail> CartDetails { get; set; }
    }
}
