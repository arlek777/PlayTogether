﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class VacancyFilter : ISimpleEntity
    {
        public VacancyFilter()
        {
            MusicGenreIds = new List<Guid>();
            MusicianRoleIds = new List<Guid>();
            WorkTypeIds = new List<Guid>();
        }

        [ForeignKey("Vacancy")]
        public Guid Id { get; set; }
        public double MinRating { get; set; }
        public double MinExpirience { get; set; }
        public ICollection<string> Cities { get; set; }
        public ICollection<Guid> WorkTypeIds { get; set; }
        public ICollection<Guid> MusicGenreIds { get; set; }
        public ICollection<Guid> MusicianRoleIds { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}