using System;
using System.ComponentModel.DataAnnotations;

namespace PlayTogether.Web.Models.ContactRequest
{
    public class SendContactRequestModel
    {
        [Required]
        public Guid ToUserId { get; set; }
    }
}