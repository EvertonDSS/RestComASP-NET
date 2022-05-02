﻿using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;

namespace GeekShopping.CartAPI.Messages;
public class CheckoutHeaderVO
{
    
    public long Id { get; set; }
    public string UserId { get; set; }
    public string? CouponCode { get; set; }
    public decimal PurchaseAmount { get; set; }
    public decimal DiscountTotal { get; set; }
    public string FirstName{ get; set; }
    public string LastName { get; set; }
    public DateTime DateTime { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string CardNumber{ get; set; }
    public string CVV { get; set; }
    public string ExpiryMothYear { get; set; }
    public int CartTotalItems { get; set; }
    public IEnumerable<CartDetailVO> CartDetails { get; set; }
        
}