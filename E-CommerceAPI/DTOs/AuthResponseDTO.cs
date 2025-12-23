namespace E_CommerceAPI.DTOs
{
    public class AuthResponseDTO
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
