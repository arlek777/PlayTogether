using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace PlayTogether.Domain
{
    public class User: BaseEntity, IUser<Guid>
    {
        public User()
        {
            UserToGroups = new List<UserToGroup>();
            MusicGenres = new List<MusicGenre>();
            WorkCategories = new List<WorkCategory>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<WorkCategory> WorkCategories { get; set; }
    }
}