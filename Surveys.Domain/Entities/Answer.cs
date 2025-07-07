using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Domain.Entities
{
    /// <summary>
    /// Вариант ответа на вопрос анкеты.
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
