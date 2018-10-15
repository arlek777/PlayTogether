using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class Profile: BaseSkills, ISimpleEntity
    {
        [ForeignKey("User")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public double Experience { get; set; }
        public string PhotoBase64 { get; set; }
        public double Rating { get; set; }
        public bool NotifyByEmail { get; set; }

        public virtual User User { get; set; }
    }

}