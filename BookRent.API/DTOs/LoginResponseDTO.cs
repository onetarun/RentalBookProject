namespace BookRent.API.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public UserInfoDTO UserInfo { get; set; }
    }
}
