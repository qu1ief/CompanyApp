using CompanyApp.DataContent.Interfaces;
using CompanyApp.Domain.Entities;

namespace CompanyApp.DataContent.Repositories;

public class DepartmentRepository : IRepository<Department>
{
    public bool Create(Department entity)
    {
        try
        {
            DbContext.Departments.Add(entity);
            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(Department entity)
    {
        try
        {
            DbContext.Departments.Remove(entity);
            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }

    public Department Get(Predicate<Department> filter)
    {
        return DbContext.Departments.Find(filter);
    }

    public List<Department> GetAll(Predicate<Department> filter = null)
    {
        return filter == null ? DbContext.Departments : DbContext.Departments.FindAll(filter);
    }

    public bool Update(Department entity)
    {
        try
        {
            var existed = Get(d => d.Id == entity.Id);
            existed = entity;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
