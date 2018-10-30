using PlayTogether.Domain;
using System;

namespace PlayTogether.Web.Models.ContactRequest
{

    public class ContactRequestModel
    {
        public Guid Id { get; set; }
        public ContactRequestStatus Status { get; set; }
        public Guid ToUserId { get; set; }
        public Guid UserId { get; set; }
        public string FromUserName { get; set; }
    }
}