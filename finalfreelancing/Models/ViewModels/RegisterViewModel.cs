using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalfreelancing.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Please Enter Email")]
        [MaxLength(40,ErrorMessage ="max 40 char")]
        [EmailAddress(ErrorMessage = "exp: exp@gmail.com")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        //[ForeignKey("Role")]
        //public int RoleId { get; set; }

        //public string? UserName { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Miss Match")]
        public string? ConfirmPassword { get; set; }

        public string? Mobile { get; set; }
    }
}
