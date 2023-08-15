using System.Linq.Expressions;
using AsiTest.Business.Contexts;
using AsiTest.Business.Contexts.InMemory;
using Microsoft.EntityFrameworkCore;

namespace AsiTest.Business.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly DbContext _repositoryContext;
    public RepositoryBase(DbContext repositoryContext) 
    {
        _repositoryContext = repositoryContext; 
    }

    public IQueryable<T> FindAll() => _repositoryContext.Set<T>().AsNoTracking();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => 
        _repositoryContext.Set<T>().Where(expression).AsNoTracking();

    public void Create(T entity) => _repositoryContext.Set<T>().Add(entity);

    public void Update(T entity) => _repositoryContext.Set<T>().Update(entity);

    public void Delete(T entity) => _repositoryContext.Set<T>().Remove(entity);
}