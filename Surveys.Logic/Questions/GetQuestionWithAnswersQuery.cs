using Microsoft.EntityFrameworkCore;
using Surveys.DataAccess.DbContexts;
using Surveys.Domain.Contracts.Questions;
using Surveys.Domain.Dtos.Answers;
using Surveys.Domain.Dtos.Questions;

namespace Surveys.Logic.Questions
{
    public class GetQuestionWithAnswersQuery : IQuery<GetQuestionWithAnswersContract, QuestionWithAnswersDto>
    {
        private readonly AppDbContext _appDbContext;

        public GetQuestionWithAnswersQuery(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<QuestionWithAnswersDto> ExecuteAsync(GetQuestionWithAnswersContract contract, CancellationToken ct)
        {
            var questionWithAnswers = await _appDbContext.Questions
                .AsNoTracking()
                .Where(q => q.Id == contract.Id)
                .Select(q => new QuestionWithAnswersDto
                {
                    Id = q.Id,
                    OrderNumber = q.OrderNumber,
                    Text = q.Text,
                    SelectionType = q.SelectionType,
                    Answers = _appDbContext.Answers
                        .Where(a => a.QuestionId == q.Id)
                        .Select(a => new AnswerDto
                        {
                            Id = a.Id,
                            QuestionId = q.Id,
                            Text = a.Text,
                        }).ToList()
                })
                .FirstOrDefaultAsync(ct);

            if (questionWithAnswers is null)
            {
                throw new InvalidOperationException("Вопрос с указанным идентификатором не найден.");
            }

            return questionWithAnswers;
        }
    }
}
