namespace SmartSave.Application.DTOs
{
    public class LoginResponseDto : BasicResponse
    {
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
