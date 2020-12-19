namespace AKHWebshop.Models.Auth
{
    public class JwtTokenHelperOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
    }
}