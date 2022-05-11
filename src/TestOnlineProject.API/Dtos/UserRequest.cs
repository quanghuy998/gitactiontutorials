namespace TestOnlineProject.API.Dtos
{
    public record RegisterUserRequest(string username, string password, string name, string email);
    public record AuthenticateUserRequest(string username, string password);
}
