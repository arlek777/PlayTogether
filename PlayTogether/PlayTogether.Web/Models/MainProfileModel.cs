namespace PlayTogether.Web.Models
{
    public class MainProfileModel
    {
        public string UserName { get; set; }
        public string Name { get; set; } // Required - Text editor (input) Validation max-length/min-length
        public string ContactEmail { get; set; } // Required - Text editor - Validation on email (+ auto-prepopulate your email)
        public string Phone1 { get; set; } // Ruquired - Text - validation on ukrainina phones(optional, mask)
        public string Phone2 { get; set; } // Optional - the same on phone1
        public string City { get; set; } // Required - google maps?? or how to check it? 
        public string Address { get; set; } // Optional
        public string Description { get; set; } // Text-area optional
        public int Age { get; set; }
        public double Experience { get; set; } // Required - range input from 0 to 50
        public string PhotoBase64 { get; set; } // Required for user photo uploader
    }
}