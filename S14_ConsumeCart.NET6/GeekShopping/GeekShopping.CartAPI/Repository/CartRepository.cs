using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.CartAPI.Repository {
    public class CartRepository : ICartRepository {

        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CartRepository(MySQLContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode) {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId) {
            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cartHeader != null) {
                _context.CartDetails
                    .RemoveRange(
                    _context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));
                _context.CartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartVO> FindCartByUserId(string userId) {
            Cart cart = new() {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(
                    c => c.UserId == userId)
            };
            cart.CartDetails = _context.CartDetails
                .Where(c => c.CartHeaderId == cart.CartHeader.Id)
                .Include(c => c.Product);
            return _mapper.Map<CartVO>(cart);
        }

        public async Task<bool> RemoveCoupon(string userId) {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId) {
            try {
                CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(
                    c => c.Id == cartDetailsId);
                int total = _context.CartDetails
                    .Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();
                _context.CartDetails.Remove(cartDetail);
                if(total == 1) {
                    var cartHeaderToRemove = await _context.CartHeaders
                        .FirstOrDefaultAsync(c=> c.Id == cartDetail.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) {

                return false;
            }
        }
        public async Task<CartVO> SaveOrUpdateCart(CartVO vo) {
            Cart cart = _mapper.Map<Cart>(vo);
            //Verificar se o produto já está salvo no BD - Se não estiver, então salvar
            var product = await _context.Products.FirstOrDefaultAsync(
                p => p.Id == vo.CartDetails.FirstOrDefault().ProductId);

            if (product == null) {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }
            //Verificar se CartHeader é nulo
            var CartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                c => c.UserId == cart.CartHeader.UserId);
            
            if (CartHeader == null) {
                //Criar CartHeader e CartDetails
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else {
                //Se CartHeader não é null
                //Verificar se CartDetails possui algum produto
                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    p.CartHeaderId == CartHeader.Id);
                if(cartDetail == null) {
                    //Criar CartDetails
                    cart.CartDetails.FirstOrDefault().CartHeaderId = CartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();

                }
                else {
                    //Atualizar contador do produto e CartDetails
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartVO>(cart);
        }
    }
}
