namespace EcommerceProject.API.Dtos
{
    public class RegisterUserRequest
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
    }
}
