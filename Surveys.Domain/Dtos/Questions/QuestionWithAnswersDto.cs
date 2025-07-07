using Surveys.Domain.Dtos.Answers;
using Surveys.Domain.Enums;

namespace Surveys.Domain.Dtos.Questions
{
    public record QuestionWithAnswersDto
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Порядковый номер вопроса.
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Текст вопроса.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Тип выбора ответов в вопросе.
        /// </summary>
        public QuestionSelectionType SelectionType { get; set; }

        /// <summary>
        /// Список ответов на вопрос.
        /// </summary>
        public IEnumerable<AnswerDto> Answers { get; set; } = Enumerable.Empty<AnswerDto>();
    }
}
