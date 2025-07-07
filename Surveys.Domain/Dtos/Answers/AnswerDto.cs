namespace Surveys.Domain.Dtos.Answers
{
    public record AnswerDto
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор вопроса.
        /// </summary>
        public long QuestionId { get; set; }

        /// <summary>
        /// Текст ответа.
        /// </summary>
        public string Text { get; set; }
    }
}
