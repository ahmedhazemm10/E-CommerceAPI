using System.ComponentModel.DataAnnotations;

namespace E_CommerceAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
