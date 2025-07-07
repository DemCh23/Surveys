namespace Surveys.Domain.Contracts.Results
{
    /// <summary>
    /// Контракт данных для сохранения ответов на вопрос.
    /// </summary>
    public class SaveResultContract : IContract
    {
        /// <summary>
        /// Идентификатор вопроса.
        /// </summary>
        public long QuestionId { get; set; }

        /// <summary>
        /// Идентификатор интервью.
        /// </summary>
        public long InterviewId { get; set; }

        /// <summary>
        /// Идентификаторы ответов.
        /// </summary>
        public IEnumerable<long> AnswerIds { get; set; } = Enumerable.Empty<long>();
    }
}
