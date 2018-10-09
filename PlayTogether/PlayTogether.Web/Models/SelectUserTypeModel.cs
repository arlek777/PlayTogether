using System.ComponentModel.DataAnnotations;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models
{
    public class SelectUserTypeModel
    {
        [Required]
        public UserType UserType { get; set; }
    }
}