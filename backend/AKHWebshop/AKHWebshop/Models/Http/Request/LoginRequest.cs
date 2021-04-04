using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AKHWebshop.Models.Http.Request
{
    public class LoginRequest
    {
        [Required]
        [JsonPropertyName("username")]
        public string UserName { get; set; }


        [JsonPropertyName("password")] public string Password { get; set; }
    }
}