using EcommerceProject.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Infrastructure.Domain
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private bool _hasActiveTransaction;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken)
        {
            if (_hasActiveTransaction)
            {
                await action();
                return;
            }

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                _hasActiveTransaction = true;
                try
                {
                    await action();
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
                finally
                {
                    _hasActiveTransaction = false;
                }
            });
        }

        public Task<TResponse> ExecuteAsync<TResponse>(Func<Task<TResponse>> action,
            CancellationToken cancellationToken = default)
        {
            if (_hasActiveTransaction) return action();

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            return strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                _hasActiveTransaction = true;
                try
                {
                    var result = await action();
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return result;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
                finally
                {
                    _hasActiveTransaction = false;
                }
            });
        }
    }
}
