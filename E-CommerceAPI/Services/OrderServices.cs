using E_CommerceAPI.DTOs;
using E_CommerceAPI.Models;
using E_CommerceAPI.Repository;

namespace E_CommerceAPI.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly ICartRepository cartRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;


        public OrderServices(ICartRepository cartRepository, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            this.cartRepository = cartRepository;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }

        public bool CreateOrder(string userId)
        {
            var cart = cartRepository.GetCart(userId);
            if (cart == null)
            {
                return false;
            }
            var order = new Order()
            {
                UserId = userId,
                Status = Status.Processing,
                TotalPrice = cart.CartItems.Select(c => c.Quantity * c.Product.Price).Sum(),
            };
            foreach (var item in cart.CartItems)
            {
                var orderItems = new OrderItems();
                orderItems.ProductId=item.ProductId;
                orderItems.Quantity=item.Quantity;
                orderItems.UnitPrice = item.Product.Price;
                order.OrderItems.Add(orderItems);
                productRepository.GetById(item.ProductId).Quantity -= item.Quantity;
            }
            orderRepository.CreateOrder(order);
            orderRepository.Save();
            cartRepository.ClearCart(userId);
            cartRepository.Save();
            return true;
        }

        public List<OrderDTO> GetAllOrders()
        {
            return orderRepository.GetAllOrders().Select(o => new OrderDTO()
            {
                CreatedAt = o.CreatedAt,
                Id = o.Id,
                Status = o.Status,
                TotalPrice = o.TotalPrice,
                Items = o.OrderItems.Select(o => new OrderItemDTO()
                {
                    ProductName = o.Product.Name,
                    Quantity = o.Quantity,
                    UnitPrice = o.UnitPrice
                }).ToList()
            }).ToList();
        }

        public OrderDTO? GetOrderById(int orderId)
        {
            var order = orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                return null;
            }
            var orderDTO = new OrderDTO()
            {
                CreatedAt = order.CreatedAt,
                Id = orderId,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                Items = order.OrderItems.Select(o => new OrderItemDTO()
                {
                    ProductName = o.Product.Name,
                    Quantity = o.Quantity,
                    UnitPrice = o.UnitPrice
                }).ToList()
            };
            return orderDTO;
        }

        public List<OrderDTO> GetUserOrders(string userId)
        {
            return orderRepository.GetUserOrders(userId).Select(order => new OrderDTO()
            {
                CreatedAt = order.CreatedAt,
                Id = order.Id,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                Items = order.OrderItems.Select(o => new OrderItemDTO()
                {
                    ProductName = o.Product.Name,
                    Quantity = o.Quantity,
                    UnitPrice = o.UnitPrice
                }).ToList()
            }).ToList();
        }

        public bool UpdateOrderStatus(Status status, int orderId)
        {
            var result = orderRepository.UpdateOrderStatus(status, orderId);
            if (result == false)
            {
                return false;
            }
            orderRepository.Save();
            return true;
        }
    }
    public interface IOrderServices
    {
        public bool CreateOrder(string userId);
        public List<OrderDTO> GetUserOrders(string userId);
        public OrderDTO? GetOrderById(int orderId);
        public bool UpdateOrderStatus(Status status, int orderId);
        public List<OrderDTO> GetAllOrders();
    }
}
