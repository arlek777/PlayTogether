using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models.Profile
{
    public class MainProfileModel
    {
        public bool IsVacancyOpen { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string VacancyFilterTitle { get; set; }

        [Required]
        [MaxLength(256)]
        public string ContactEmail { get; set; }

        [Required]
        [MaxLength(256)]
        public string Phone1 { get; set; }
        public string Phone2 { get; set; } 

        [Required]
        [MaxLength(256)]
        public string City { get; set; } 
        public string Address { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public double Experience { get; set; }
        public string PhotoBase64 { get; set; }

        public virtual ICollection<WorkType> WorkTypes { get; set; }
    }
}