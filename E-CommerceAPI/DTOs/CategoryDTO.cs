using System.ComponentModel.DataAnnotations;

namespace E_CommerceAPI.DTOs
{
    public class CategoryDTO
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }
    }
}
