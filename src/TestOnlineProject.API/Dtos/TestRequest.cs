namespace TestOnlineProject.API.Dtos
{
    public class TestRequest
    {
        public record CreateTestRequest(string title);
        public record UpdateTestRequest(string title);
    }
}
