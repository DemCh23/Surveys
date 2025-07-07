using Microsoft.AspNetCore.Mvc;
using Surveys.Domain.Contracts.Results;
using Surveys.Logic.Results;

namespace Surveys.Web.Controllers.Results
{
    [ApiController]
    [Route("[controller]")]
    public class ResultsController : ApiControllerBase
    {
        /// <summary>
        /// Сохраняет ответы на вопрос и возвращает id следующего вопроса.
        /// </summary>
        [HttpPost]
        public Task<IActionResult> SaveResult(
            [FromServices] SaveResultAndGetNextQuestionIdQuery query,
            [FromBody] SaveResultContract contract,
            CancellationToken ct)
        {
            return ExecuteQuery<
                SaveResultAndGetNextQuestionIdQuery,
                SaveResultContract,
                long>
                (query, contract, ct);
        }
    }
}
