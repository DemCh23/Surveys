using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Domain.Entities
{
    public class Result
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор ответа.
        /// </summary>
        public long AnswerId { get; set; }

        /// <summary>
        /// Идентификатор информации об интервью.
        /// </summary>
        public long InterviewId { get; set; }
    }
}
