using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class LoginUserModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserNameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
