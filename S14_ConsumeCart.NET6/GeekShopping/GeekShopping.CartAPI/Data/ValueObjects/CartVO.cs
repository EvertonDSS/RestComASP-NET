using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GeekShopping.CartAPI.Data.ValueObjects
{
    public class CartVO
    {
        public CartHeaderVO CartHeader { get; set; }

        public IEnumerable<CartDetailVO> CartDetails { get; set; }
    }
}
