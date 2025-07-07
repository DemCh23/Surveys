using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Domain.Entities
{
    /// <summary>
    /// Информация об интервью.
    /// </summary>
    public class Interview
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
        /// Идентификатор пользователя.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Начало анкетирования.
        /// </summary>
        public DateTimeOffset StartedAt { get; set; }

        /// <summary>
        /// Конец анкетирования.
        /// </summary>
        public DateTimeOffset? CompletedAt { get; set; }
    }
}
