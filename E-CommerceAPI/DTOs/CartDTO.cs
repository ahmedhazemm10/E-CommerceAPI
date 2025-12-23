namespace E_CommerceAPI.DTOs
{
    public class CartDTO
    {
        public int ItemsCount { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemReadDTO> Items { get; set; } = new();
    }
}
