using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class Group: ISimpleEntity
    {
        public Group()
        {
            SearchRequests = new List<SearchRequest>();
            SearchResponses = new List<SearchResponse>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid CreatorUserId { get; set; }
        public Guid ProfileId { get; set; }
        public virtual User User { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<SearchRequest> SearchRequests { get; set; }
        public virtual ICollection<SearchResponse> SearchResponses { get; set; }
    }
}