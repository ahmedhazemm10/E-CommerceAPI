using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [Required,MaxLength(100)]
        public string Description { get; set; }
        [Range(0.1,double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public Category Category { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public List<CartItems> CartItems { get; set; } = new();
        public List<OrderItems> OrderItems { get; set; } = new();
    }
}
