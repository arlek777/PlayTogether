using System;

namespace PlayTogether.Web.Models.Profile
{
    public class PublicProfileModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string ContactEmail { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public double Experience { get; set; }
        public string PhotoBase64 { get; set; }

        public string MusicGenres { get; set; }
        public string MusicianRoles { get; set; }
        public string WorkTypes { get; set; }
    }
}