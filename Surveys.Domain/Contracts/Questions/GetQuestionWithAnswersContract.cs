namespace Surveys.Domain.Contracts.Questions
{
    /// <summary>
    /// Контракт получения вопроса с ответами.
    /// </summary>
    public class GetQuestionWithAnswersContract : IContract
    {
        /// <summary>
        /// Идентификатор вопроса.
        /// </summary>
        public long Id { get; set; }
    }
}
