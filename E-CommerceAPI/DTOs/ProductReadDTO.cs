using System.ComponentModel.DataAnnotations;

namespace E_CommerceAPI.DTOs
{
    public class ProductReadDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
    }
}
