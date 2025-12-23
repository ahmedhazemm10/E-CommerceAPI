using E_CommerceAPI.DTOs;
using E_CommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_CommerceAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddItemToCart(CartItems cartItem)
        {
            var item = _context.CartItems.FirstOrDefault(x => x.ProductId == cartItem.ProductId && x.CartId == cartItem.CartId);
            if (item == null)
            {
                _context.CartItems.Add(cartItem);
            }
            else
            {
                item.Quantity += cartItem.Quantity;
            }
        }

        public bool ClearCart(string userId)
        {
            var cart = _context.Carts.Include(x => x.CartItems).FirstOrDefault(x => x.UserId == userId);
            if (cart != null)
            {
                cart.CartItems.RemoveRange(0, cart.CartItems.Count);
                return true;
            }
            return false;
        }

        public Cart? GetCart(string userId)
        {
            return _context.Carts.Include(c => c.CartItems).ThenInclude(c => c.Product).FirstOrDefault(c => c.UserId == userId);
        }


        public bool RemoveItemFromCart(int itemId)
        {
            var item = _context.CartItems.FirstOrDefault(x => x.ID == itemId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                return true;
            }
            return false;
        }

        public bool UpdateItemQuantity(int itemId, int newQuantity)
        {
            if (newQuantity <= 0)
            {
                return RemoveItemFromCart(itemId);
            }
            var item = _context.CartItems.FirstOrDefault(x => x.ID == itemId);
            if (item != null)
            {
                item.Quantity = newQuantity;
                return true;
            }
            return false;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void AddCart(Cart cart, string userId)
        {
            cart.UserId = userId;
            _context.Carts.Add(cart);
        }

        public int? GetProductId(int itemId)
        {
            var item = _context.CartItems.FirstOrDefault(s => s.ID == itemId);
            return item != null ? item.ProductId : null;
        }
    }
    public interface ICartRepository
    {
        public Cart? GetCart(string userId);
        public int? GetProductId(int itemId);
        public void AddItemToCart(CartItems cartItem);
        public bool UpdateItemQuantity(int itemId, int newQuantity);
        public bool RemoveItemFromCart(int itemId);
        public bool ClearCart(string userId);
        public void Save();
        public void AddCart(Cart cart, string userId);
    }
}
