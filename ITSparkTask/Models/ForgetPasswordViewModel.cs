using System.ComponentModel.DataAnnotations;

namespace ITSparkTask.PL.Models
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
