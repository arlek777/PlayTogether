﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace PlayTogether.Domain
{
    public class User: IUser<Guid>, ISimpleEntity
    {
        public User()
        {
            Vacancies = new List<Vacancy>();
            VacancyResponses = new List<VacancyResponse>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string UserName { get; set; } // email
        public string PasswordHash { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
        public virtual ICollection<VacancyResponse> VacancyResponses { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}