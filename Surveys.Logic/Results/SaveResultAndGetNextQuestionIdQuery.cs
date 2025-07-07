using Microsoft.EntityFrameworkCore;
using Surveys.DataAccess.DbContexts;
using Surveys.Domain.Contracts.Results;
using Surveys.Domain.Entities;

namespace Surveys.Logic.Results
{
    public class SaveResultAndGetNextQuestionIdQuery : IQuery<SaveResultContract, long>
    {
        private readonly AppDbContext _appDbContext;

        public SaveResultAndGetNextQuestionIdQuery(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<long> ExecuteAsync(SaveResultContract contract, CancellationToken ct)
        {
            contract.AnswerIds.Distinct().ToList();

            await CheckParamsAsync(contract, ct);

            await using var transaction = await _appDbContext.Database.BeginTransactionAsync(ct);

            foreach (var answerId in contract.AnswerIds)
            {
                await _appDbContext.Results.AddAsync(new Result
                {
                    AnswerId = answerId,
                    InterviewId = contract.InterviewId
                },
                ct);
            }

            var nextQuestionId = await _appDbContext.Questions
                .Where(q => q.OrderNumber >
                    _appDbContext.Questions
                        .Where(q2 => q2.Id == contract.QuestionId)
                        .Select(q2 => q2.OrderNumber)
                        .FirstOrDefault())
                .OrderBy(q => q.OrderNumber)
                .Select(q => q.Id)
                .FirstOrDefaultAsync(ct);

            await _appDbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync();

            return nextQuestionId;
        }

        private async Task CheckParamsAsync(SaveResultContract contract, CancellationToken ct)
        {
            var answers = await _appDbContext.Answers
                .Where(a => contract.AnswerIds.Contains(a.Id))
                .ToListAsync(ct);

            if (!answers.Any())
            {
                throw new InvalidOperationException("Ответы с указанными идентификаторами не найдены.");
            }

            var question = await _appDbContext.Questions
                .Where(q => q.Id == contract.QuestionId)
                .FirstOrDefaultAsync(ct);

            if (question is null)
            {
                throw new InvalidOperationException("Вопрос с указанным идентификатором не найден.");
            }

            var validAnswers = await _appDbContext.Answers
            .Where(a => a.QuestionId == contract.QuestionId && contract.AnswerIds.Contains(a.Id))
            .ToListAsync(ct);

            if (validAnswers.Count() != contract.AnswerIds.Count())
            {
                throw new InvalidOperationException("Некоторые ответы не относятся к указанному вопросу.");
            }
        }
    }
}
