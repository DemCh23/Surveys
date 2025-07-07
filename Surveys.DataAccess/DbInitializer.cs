using Surveys.DataAccess.DbContexts;
using Surveys.Domain.Entities;
using Surveys.Domain.Enums;

namespace Surveys.DataAccess
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _appDbContext;

        public DbInitializer(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task InitializeAsync(CancellationToken ct)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync(ct);

            if (!_appDbContext.Surveys.Any()
                && !_appDbContext.Questions.Any()
                && !_appDbContext.Answers.Any())
            {
                var survey = await _appDbContext.Surveys.AddAsync(new Survey
                {
                    Title = "Тестовая анкета",
                    Description = "Тестовое описание",
                });
                await _appDbContext.SaveChangesAsync(ct);

                var question = await _appDbContext.Questions.AddAsync(new Question
                {
                    SurveyId = survey.Entity.Id,
                    Text = "Тестовый вопрос?",
                    OrderNumber = 1,
                    SelectionType = QuestionSelectionType.Single,
                });

                await _appDbContext.Questions.AddAsync(new Question
                {
                    SurveyId = survey.Entity.Id,
                    Text = "Тестовый вопрос?",
                    OrderNumber = 2,
                    SelectionType = QuestionSelectionType.Single,
                });
                await _appDbContext.SaveChangesAsync(ct);

                await _appDbContext.Answers.AddAsync(new Answer
                {
                    QuestionId = question.Entity.Id,
                    Text = "Да",
                });

                await _appDbContext.Answers.AddAsync(new Answer
                {
                    QuestionId = question.Entity.Id,
                    Text = "Нет",
                });

                await _appDbContext.Interviews.AddAsync(new Interview
                {
                    SurveyId = survey.Entity.Id,
                    UserId = 0,
                    StartedAt = DateTimeOffset.UtcNow,
                });
                await _appDbContext.SaveChangesAsync(ct);

                await transaction.CommitAsync();
            }
        }
    }
}
