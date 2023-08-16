using System.Linq.Expressions;

namespace AsiTest.Business.Repositories;

/// <summary>
/// Base repository used for direct database interactions
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TU"></typeparam>
public interface IRepositoryBase<T,TU>
{
    /// <summary>
    /// Finds and returns entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    T? FindById(TU id);
    
    /// <summary>
    /// Returns all entities within the database
    /// </summary>
    /// <returns></returns>
    IQueryable<T> FindAll(); 
    
    /// <summary>
    /// Finds entities by supplied expression
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression); 
    
    /// <summary>
    /// Creates a new entity with supplied entity object
    /// </summary>
    /// <param name="entity"></param>
    void Create(T entity); 
    
    /// <summary>
    /// Updates entity with supplied entity object
    /// </summary>
    /// <param name="entity"></param>
    void Update(T entity);
    
    /// <summary>
    /// Deletes entity with supplied entity object
    /// </summary>
    /// <param name="entity"></param>
    void Delete(T entity);
}