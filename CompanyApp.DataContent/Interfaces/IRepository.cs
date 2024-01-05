using CompanyApp.Domain.Entities.Common;

namespace CompanyApp.DataContent.Interfaces;

public interface IRepository<T>where T : BaseEntity
{
    bool Create(T entity);
    bool Update(T entity);
    bool Delete(T entity);  
    T Get( Predicate< T> filter);
    List<T> GetAll(Predicate<T> filter=null);
}
