using System.ComponentModel.DataAnnotations;

namespace PlayTogether.Web.Models.Profile
{
    public class ContactProfileModel
    {
        [Required]
        [MaxLength(256)]
        public string ContactEmail { get; set; }

        [Required]
        [MaxLength(15)]
        public string Phone1 { get; set; }

        [MaxLength(15)]
        public string Phone2 { get; set; }

        [Required]
        [MaxLength(256)]
        public string City { get; set; }

        [MaxLength(256)]
        public string Url1 { get; set; }

        [MaxLength(256)]
        public string Url2 { get; set; }

        [MaxLength(256)]
        public string Address { get; set; }
    }
}