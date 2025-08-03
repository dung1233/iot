namespace TTS.Dto
{
    public class LoginResponseDto
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public UserRepodto User { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiry { get; set; }
    }
}
