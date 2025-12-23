using E_CommerceAPI.Models;

namespace E_CommerceAPI.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal TotalPrice { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
    }
}
