using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class ContactRequest: ISimpleEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public ContactRequestStatus Status { get; set; }
        public Guid UserId { get; set; }
        public Guid ToUserId { get; set; }

        public virtual User User { get; set; }
    }
}