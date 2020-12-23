using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AKHWebshop.Models.Auth
{
    public class UserRole
    {
        [Required] [Column("user_id")] public string UserId { get; set; }

        public AppUser User { get; set; }

        [Required] [Column("role_id")] public string RoleId { get; set; }

        public AppRole Role { get; set; }
    }
}