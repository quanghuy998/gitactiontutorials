using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.QuestionAggregate
{
    public interface IQuestionRepository : IRepository<Question, Guid>
    {
    }
}
