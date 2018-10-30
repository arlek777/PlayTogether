using System;
using System.ComponentModel.DataAnnotations.Schema;
using PlayTogether.Domain.MasterValues;

namespace PlayTogether.Domain
{
    public class Profile: BaseSkills, ISimpleEntity
    {
        public Profile()
        {
            JsonContactTypes = "";
        }

        [ForeignKey("User")]
        public Guid Id { get; set; }

        public bool IsActivated { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string ContactEmail { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string JsonCity { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public double Experience { get; set; }
        public string PhotoBase64 { get; set; }
        public double Rating { get; set; }
        public bool NotifyByEmail { get; set; }
        public string Url1 { get; set; }
        public string Url2 { get; set; }
        public string JsonContactTypes { get; set; }

        public virtual User User { get; set; }
    }

}