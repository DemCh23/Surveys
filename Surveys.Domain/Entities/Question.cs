using Surveys.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Domain.Entities
{
    /// <summary>
    /// Вопрос анкеты.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор анкеты.
        /// </summary>
        public long SurveyId { get; set; }

        /// <summary>
        /// Текст вопроса.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Порядковый номер в опросе.
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Тип выбора ответов в вопросе.
        /// </summary>
        public QuestionSelectionType SelectionType { get; set; }
    }
}
