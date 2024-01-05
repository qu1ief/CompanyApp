using CompanyApp.DataContent.Interfaces;
using CompanyApp.Domain.Entities;

namespace CompanyApp.DataContent.Repositories;

public class EmployeeRepository : IRepository<Employee>
{
    public bool Create(Employee entity)
    {
        try
        {
            DbContext.Employees.Add(entity);
            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(Employee entity)
    {
        try
        {
            DbContext.Employees.Remove(entity);
            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }

    public Employee Get(Predicate<Employee> filter)
    {
        return DbContext.Employees.Find(filter);
    }

    public List<Employee> GetAll(Predicate<Employee> filter = null)
    {
        return filter == null ? DbContext.Employees : DbContext.Employees.FindAll(filter);
    }

    public bool Update(Employee entity)
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
