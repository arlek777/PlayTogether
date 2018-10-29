using System;
using System.ComponentModel.DataAnnotations;

namespace PlayTogether.Web.Models.ContactRequest
{
    public class ContactRequestModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public bool IsApproved { get; set; }
    }
}