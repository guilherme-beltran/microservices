using Customers.Api.Core.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Customers.Api.Persistence.UnitOfWork;

internal sealed class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private readonly CustomerContext _context;

    public UnitOfWork(CustomerContext context)
    {
        _context = context;
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task<int> Commit(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
    }
}
