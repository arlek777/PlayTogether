using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class SearchFilter : ISimpleEntity
    {
        [ForeignKey("SearchRequest")]
        public Guid Id { get; set; }
        public double MinRating { get; set; }
        public double MinExpirience { get; set; }
        public ICollection<string> Cities { get; set; }
        public ICollection<Guid> MusicGenreIds { get; set; }
        public ICollection<Guid> WorkCategoryIds { get; set; }
        public ICollection<Guid> WorkStatusIds { get; set; }
        public virtual SearchRequest SearchRequest { get; set; }
    }
}