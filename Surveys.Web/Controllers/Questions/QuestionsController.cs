using Microsoft.AspNetCore.Mvc;
using Surveys.Domain.Contracts.Questions;
using Surveys.Domain.Dtos.Questions;
using Surveys.Logic.Questions;

namespace Surveys.Web.Controllers.Questions
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ApiControllerBase
    {
        /// <summary>
        /// Возвращает dto вопроса с его вариантами ответов.
        /// </summary>
        [HttpGet("{Id:long}/with-answers")]
        public Task<IActionResult> GetQuestionWithAnswers(
            [FromServices] GetQuestionWithAnswersQuery query,
            [FromRoute] GetQuestionWithAnswersContract contract,
            CancellationToken ct)
        {
            return ExecuteQuery<
                GetQuestionWithAnswersQuery,
                GetQuestionWithAnswersContract,
                QuestionWithAnswersDto>
                (query, contract, ct);
        }
    }
}
