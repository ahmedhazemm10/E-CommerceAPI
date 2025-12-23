using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal TotalPrice { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public List<OrderItems> OrderItems { get; set; } = new();
    }
}
