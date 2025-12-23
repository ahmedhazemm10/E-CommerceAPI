using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceAPI.DTOs
{
    public class ProductCreateDTO
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }
        [Range(0.1, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
