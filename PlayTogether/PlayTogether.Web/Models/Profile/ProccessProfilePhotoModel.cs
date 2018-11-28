using System.ComponentModel.DataAnnotations;

namespace PlayTogether.Web.Models.Profile
{
    public class ProccessProfilePhotoModel
    {
        [Required]
        public string PhotoBase64 { get; set; }
    }
}