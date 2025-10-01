using System.ComponentModel.DataAnnotations;

namespace ITSparkTask.PL.Models
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemmberMe { get; set; }

    }
}
