using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class SearchRequest: ISimpleEntity
    {
        public SearchRequest()
        {
            SearchResponses = new List<SearchResponse>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid? GroupId { get; set; }
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool NotifyByEmail { get; set; }
        public DateTime Date { get; set; }
        public SearchRequestStatus Status { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
        public virtual SearchFilter SearchFilter { get; set; }
        public virtual ICollection<SearchResponse> SearchResponses { get; set; }
    }
}