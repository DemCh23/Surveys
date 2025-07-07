using Microsoft.AspNetCore.Mvc;
using Surveys.Domain.Contracts;
using Surveys.Logic;

namespace Surveys.Web.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected async Task<IActionResult> ExecuteQuery<TQuery, TContract, TResult>(
            TQuery query,
            TContract contract,
            CancellationToken ct = default)
            where TQuery : IQuery<TContract, TResult>
            where TContract : IContract
            where TResult : notnull 
        {
            try
            {
                var result = await query.ExecuteAsync(contract, ct);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
