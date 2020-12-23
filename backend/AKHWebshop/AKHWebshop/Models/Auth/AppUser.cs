using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace AKHWebshop.Models.Auth
{
    public class AppUser : IdentityUser
    {
        [Required]
        [JsonPropertyName("username")]
        [Column("username")]
        public override string UserName { get; set; }


        [JsonPropertyName("password")]
        [NotMapped]
        public string Password { get; set; }

        [JsonPropertyName("roles")] public List<UserRole> Roles { get; set; }
    }
}