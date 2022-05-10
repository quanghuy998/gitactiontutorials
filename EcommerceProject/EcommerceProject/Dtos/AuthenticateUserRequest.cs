namespace EcommerceProject.API.Dtos
{
    public class AuthenticateUserRequest
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public bool RememberMe { get; init; }
    }
}
