using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceAPI.Models
{
    public class OrderItems
    {
        public int ID { get; set; }
        public Order Order {  get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Product Product { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
