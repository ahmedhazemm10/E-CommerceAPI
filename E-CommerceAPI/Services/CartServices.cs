using E_CommerceAPI.DTOs;
using E_CommerceAPI.Models;
using E_CommerceAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceAPI.Services
{
    public class CartServices : ICartServices
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartServices(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public bool AddItemToCart(CartItemCreateDTO cartItemDTO, string userId)
        {
            var cart = _cartRepository.GetCart(userId);
            if (cart == null)
            {
                var createCart = new Cart()
                {
                    UserId = userId
                };
                cart = createCart;
                _cartRepository.AddCart(cart, userId);
                _cartRepository.Save();
            }
            var existingQuantity = _productRepository.GetStock(cartItemDTO.ProductId);
            if (existingQuantity == null)
            {
                return false;
            }
            if (cartItemDTO.Quantity > existingQuantity)
            {
                return false;
            }
            var item = new CartItems()
            {
                CartId = cart.Id,
                ProductId = cartItemDTO.ProductId,
                Quantity = cartItemDTO.Quantity
            };
            _cartRepository.AddItemToCart(item);
            _cartRepository.Save();
            return true;
        }

        public bool ClearCart(string userId)
        {
            var result = _cartRepository.ClearCart(userId);
            if (result == false)
            {
                return false;
            }
            _cartRepository.Save();
            return true;
        }

        public CartDTO? GetCart(string userId)
        {
            var cart = _cartRepository.GetCart(userId);
            if (cart == null)
            {
                return null;
            }
            var cartDto = new CartDTO()
            {
                Items = cart.CartItems.Select(c =>
                new CartItemReadDTO()
                {
                    ProductId = c.ProductId,
                    CartItemId = c.ID,
                    Price = c.Product.Price,
                    ProductName = c.Product.Name,
                    Quantity = c.Quantity
                }).ToList(),
                ItemsCount = cart.CartItems.Count,
                TotalPrice = cart.CartItems.Select(c => c.Product.Price * c.Quantity).Sum()
            };
            return cartDto;
        }

        public bool RemoveItemFromCart(int itemId)
        {
            var result = _cartRepository.RemoveItemFromCart(itemId);
            if (result == false)
            {
                return false;
            }
            _cartRepository.Save();
            return true;
        }

        public bool UpdateItemQuantity(int itemId, int newQuantity)
        {
            var productId = _cartRepository.GetProductId(itemId);
            if (productId == null)
            {
                return false;
            }
            var existingQuantity = _productRepository.GetStock(productId);
            if (existingQuantity == null)
            {
                return false;
            }    
            if (newQuantity > existingQuantity)
            {
                return false;
            }
            _cartRepository.UpdateItemQuantity(itemId, newQuantity);
            _cartRepository.Save();
            return true;
        }
    }
    public interface ICartServices
    {
        public CartDTO? GetCart(string userId);
        public bool AddItemToCart(CartItemCreateDTO cartItemDTO, string userId);
        public bool UpdateItemQuantity(int itemId, int newQuantity);
        public bool RemoveItemFromCart(int itemId);
        public bool ClearCart(string userId);
    }
}
