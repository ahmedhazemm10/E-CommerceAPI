using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceAPI.Models
{
    public class CartItems
    {
        public int ID { get; set; }
        public Cart Cart { get; set; }
        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public Product Product { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
