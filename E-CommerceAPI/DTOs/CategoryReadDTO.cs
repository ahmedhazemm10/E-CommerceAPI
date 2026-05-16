using System.ComponentModel.DataAnnotations;

namespace E_CommerceAPI.DTOs
{
    public class CategoryReadDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}
