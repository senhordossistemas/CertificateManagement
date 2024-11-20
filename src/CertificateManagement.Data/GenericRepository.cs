using System.Linq.Expressions;
using CertificateManagement.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CertificateManagement.Data;

public class GenericRepository(DataContext context) : IGenericRepository
{
    public IQueryable<T> QueryAsNoTracking<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
    {
        var query = context.Set<T>().AsQueryable().AsNoTrackingWithIdentityResolution();

        if (includeProperties != null)
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return query;
    }

    public IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
    {
        var query = context.Set<T>().AsQueryable();

        if (includeProperties != null)
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return query;
    }

    public IQueryable<T> QueryIncludeStringProperties<T>(params string[] includeProperties) where T : class
    {
        var query = context.Set<T>().AsQueryable();
        if (includeProperties is { Length: > 0 })
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return query;
    }

    public async Task AddAsync<T>(T entity) where T : class => await context.Set<T>().AddAsync(entity);

    public async Task AddRange<T>(IEnumerable<T> items) where T : class => await context.AddRangeAsync(items);

    public void Update<T>(T entity) where T : class => context.Set<T>().Update(entity);

    public void Remove<T>(T item) where T : class => context.Remove(item);

    public void RemoveRange<T>(IEnumerable<T> items) where T : class => context.RemoveRange(items);

    public async Task<bool> CommitAsync() => await context.SaveChangesAsync() > 0;
}