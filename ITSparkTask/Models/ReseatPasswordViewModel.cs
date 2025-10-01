using System.ComponentModel.DataAnnotations;

namespace ITSparkTask.PL.Models
{
    public class ReseatPasswordViewModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
