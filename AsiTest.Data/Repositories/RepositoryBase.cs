using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AsiTest.Business.Repositories;

/// <summary>
/// Base repository used for direct database interactions
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TU"></typeparam>
public class RepositoryBase<T,TU> : IRepositoryBase<T,TU> where T : class
{
    private readonly DbContext _repositoryContext;
    public RepositoryBase(DbContext repositoryContext) 
    {
        _repositoryContext = repositoryContext; 
    }

    /// <summary>
    /// Finds and returns entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public T? FindById(TU id)
    {
        return _repositoryContext.Find<T>(id);
    }

    /// <summary>
    /// Returns all entities within the database
    /// </summary>
    /// <returns></returns>
    public IQueryable<T> FindAll() => _repositoryContext.Set<T>().AsNoTracking();

    /// <summary>
    /// Finds entities by supplied expression
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => 
        _repositoryContext.Set<T>().Where(expression).AsNoTracking();

    /// <summary>
    /// Creates a new entity with supplied entity object
    /// </summary>
    /// <param name="entity"></param>
    public void Create(T entity) => _repositoryContext.Set<T>().Add(entity);

    /// <summary>
    /// Updates entity with supplied entity object
    /// </summary>
    /// <param name="entity"></param>
    public void Update(T entity) => _repositoryContext.Set<T>().Update(entity);

    /// <summary>
    /// Deletes entity with supplied entity object
    /// </summary>
    /// <param name="entity"></param>
    public void Delete(T entity) => _repositoryContext.Set<T>().Remove(entity);
}