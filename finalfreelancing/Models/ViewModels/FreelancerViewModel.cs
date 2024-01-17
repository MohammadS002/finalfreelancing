using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace finalfreelancing.Models.ViewModels
{
    public class FreelancerViewModel
    {

        public int FreelancerId { get; set; }
        public IFormFile? Pic { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Exp { get; set; }
        [Required]
        public string? Career { get; set; }
        [Required]
        public string? Email { get; set; }
        public decimal Rating { get; set; }

    }
}
