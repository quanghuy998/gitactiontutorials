namespace TestOnlineProject.API.Dtos
{
    public class ExamRequest
    {
        public record CreateExamRequest(string title);
        public record UpdateExamRequest(string title);
    }
}
