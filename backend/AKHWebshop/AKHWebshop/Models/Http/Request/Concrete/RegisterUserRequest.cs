using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace AKHWebshop.Models.Http.Request.Concrete
{
    public class RegisterUserRequest
    {
        [FromBody]
        [Required]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}