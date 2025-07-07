using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Domain.Entities
{
    /// <summary>
    /// Анкета.
    /// </summary>
    public class Survey
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Название анкеты.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание анкеты.
        /// </summary>
        public string Description { get; set; }
    }
}
