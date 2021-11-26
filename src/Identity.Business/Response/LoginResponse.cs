namespace Identity.Business.Response
{
    public class LoginResponse : ResponseBase
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenResponse UserToken { get; set; }
    }

    public class UserTokenResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimResponse> Claims { get; set; }
    }

    public class ClaimResponse
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}