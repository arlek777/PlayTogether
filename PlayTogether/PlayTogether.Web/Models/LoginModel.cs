using System.ComponentModel.DataAnnotations;

namespace PlayTogether.Web.Models
{
    public class LoginModel
    {
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        [Required]
        public string UserName { get; set; }

        [MinLength(6)]
        [Required]
        public string Password { get; set; }
    }
}