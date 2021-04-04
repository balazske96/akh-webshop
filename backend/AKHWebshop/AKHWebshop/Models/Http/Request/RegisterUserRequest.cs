using System.Text.Json.Serialization;

namespace AKHWebshop.Models.Http.Request
{
    public class RegisterUserRequest
    {
        [JsonPropertyName("username")] public string UserName { get; set; }

        [JsonPropertyName("password")] public string Password { get; set; }
    }
}