using Surveys.Domain.Contracts;

namespace Surveys.Logic
{
    public interface IQuery<TContract, TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        Task<TResult> ExecuteAsync(TContract contract, CancellationToken ct);
    }
}
