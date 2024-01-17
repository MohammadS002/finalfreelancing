using System.ComponentModel.DataAnnotations;

namespace finalfreelancing.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
