namespace Customers.Api.Persistence.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    Task<int> Commit(CancellationToken cancellationToken);
    void Rollback();
}
