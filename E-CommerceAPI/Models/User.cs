using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public Cart? Cart { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}
