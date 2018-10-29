using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class City : ISimpleEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}