using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace PlayTogether.Domain
{
    public class User: IUser<Guid>, ISimpleEntity
    {
        public User()
        {
            SearchRequests = new List<SearchRequest>();
            SearchResponses = new List<SearchResponse>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid? WorkStatusId { get; set; }

        public string UserName { get; set; } // email
        public string PasswordHash { get; set; }

        public virtual WorkStatus WorkStatus { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<SearchRequest> SearchRequests { get; set; }
        public virtual ICollection<SearchResponse> SearchResponses { get; set; }
        public virtual ICollection<UserToGroup> UserToGroups { get; set; }
    }
}