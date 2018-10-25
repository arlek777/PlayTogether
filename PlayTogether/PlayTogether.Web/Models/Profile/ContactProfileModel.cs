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
        public string City { get; set; }  // todo it's not implemented yet

        [MaxLength(256)]
        public string Address { get; set; }
    }
}