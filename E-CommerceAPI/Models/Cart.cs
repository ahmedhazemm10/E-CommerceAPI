using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public List<CartItems> CartItems { get; set; } = new();
    }
}