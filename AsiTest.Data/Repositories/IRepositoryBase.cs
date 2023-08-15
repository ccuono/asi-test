using System.Linq.Expressions;

namespace AsiTest.Business.Repositories;

public interface IRepositoryBase<T>
{
    T? FindById(long id);
    IQueryable<T> FindAll(); 
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression); 
    void Create(T entity); 
    void Update(T entity); 
    void Delete(T entity);
}