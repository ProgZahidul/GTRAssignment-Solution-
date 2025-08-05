namespace GTR.WebAPI.Models
{
    public class AuthResponse
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
