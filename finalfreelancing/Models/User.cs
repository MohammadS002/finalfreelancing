using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalfreelancing.Models
{
    public class User 
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }

        public string? Password { get; set; }
   
        public string? Email { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
