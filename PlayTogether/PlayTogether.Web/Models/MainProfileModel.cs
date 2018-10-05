using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models
{
    public class MainProfileModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ProfileId { get; set; }
        public bool IsActivated { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; } // Required - Text editor (input) Validation max-length/min-length

        [Required]
        [MaxLength(256)]
        public string ContactEmail { get; set; } // Required - Text editor - Validation on email (+ auto-prepopulate your email)

        [Required]
        [MaxLength(256)]
        public string Phone1 { get; set; } // Ruquired - Text - validation on ukrainina phones(optional, mask)
        public string Phone2 { get; set; } // Optional - the same on phone1

        [Required]
        [MaxLength(256)]
        public string City { get; set; } // Required - google maps?? or how to check it? 
        public string Address { get; set; } // Optional
        public string Description { get; set; } // Text-area optional
        public int Age { get; set; }
        public double Experience { get; set; } // Required - range input from 0 to 50
        public string PhotoBase64 { get; set; } // Required for user photo uploader

        // Checkboxes
        public virtual ICollection<WorkType> WorkTypes { get; set; }
    }
}