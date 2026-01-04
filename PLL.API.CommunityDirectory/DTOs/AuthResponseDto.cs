namespace PLL.API.CommunityDirectory.DTOs
{
    public class AuthResponseDto
    {
        public bool Success { get; set; } 
        public string Message { get; set; }
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; } 
        public UserDto? User { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
