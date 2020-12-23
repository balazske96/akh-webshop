using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AKHWebshop.Models.Auth
{
    public class AppRole : IdentityRole
    {
        public List<UserRole> Users { get; set; }
    }
}