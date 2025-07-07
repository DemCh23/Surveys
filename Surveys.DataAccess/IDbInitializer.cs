namespace Surveys.DataAccess
{
    public interface IDbInitializer
    {
        Task InitializeAsync(CancellationToken ct = default);
    }
}
