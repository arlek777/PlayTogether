using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class SearchResponse : ISimpleEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid SearchRequestId { get; set; }
        public Guid? GroupId { get; set; }
        public Guid? UserId { get; set; }
        public string Message { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
        public virtual SearchRequest SearchRequest { get; set; }
    }
}