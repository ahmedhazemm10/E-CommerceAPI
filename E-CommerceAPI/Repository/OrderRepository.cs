using E_CommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public void CreateOrder(Order order)
        {
            appDbContext.Orders.Add(order);
        }

        public List<Order> GetAllOrders()
        {
            return appDbContext.Orders.Include(o => o.OrderItems).ThenInclude(o => o.Product)
                .OrderByDescending(o => o.CreatedAt).ToList();
        }

        public Order? GetOrderById(int orderId)
        {
            return appDbContext.Orders.Include(o => o.OrderItems).ThenInclude(o => o.Product)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public List<Order> GetUserOrders(string userId)
        {
            return appDbContext.Orders.Include(o => o.OrderItems).ThenInclude(o => o.Product)
                .Where(o => o.UserId == userId).OrderByDescending(o => o.CreatedAt).ToList();
        }

        public void Save()
        {
            appDbContext.SaveChanges();
        }

        public bool UpdateOrderStatus(Status status, int orderId)
        {
            var order = appDbContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return false;
            }
            order.Status = status;
            return true;
        }
    }
    public interface IOrderRepository
    {
        public void CreateOrder(Order order);
        public List<Order> GetUserOrders(string userId);
        public Order? GetOrderById(int orderId);
        public bool UpdateOrderStatus(Status status, int orderId);
        public List<Order> GetAllOrders();
        public void Save();
    }
}
