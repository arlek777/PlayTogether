using System;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models.Vacancy
{
    public class PublicVacancyModel
    {
        public Guid Id { get; set; }
        public Guid UserCreatorId { get; set; }
        public string UserCreatorName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsContactRequestSent { get; set; }

        public UserType UserType { get; set; }
        public double MinRating { get; set; }
        public double MinExperience { get; set; }

        public string Cities { get; set; }
        public string MusicGenres { get; set; }
        public string MusicianRoles { get; set; }
        public string WorkTypes { get; set; }
    }
}