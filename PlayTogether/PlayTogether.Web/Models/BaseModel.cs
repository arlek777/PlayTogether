using System;
using System.ComponentModel.DataAnnotations;

namespace PlayTogether.Web.Models
{
    public class BaseModel
    {
        [Required]
        public Guid UserId { get; set; }
    }
}